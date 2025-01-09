using Amazon.S3;
using Amazon.S3.Model;
using FileSignatures;
using HE.Remediation.Core.Exceptions;
using HE.Remediation.Core.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using VirusScanner.Client.Interfaces;

namespace HE.Remediation.Core.Services.FileService
{
    public class FileService : IFileService
    {
        private readonly VirusScanningSettings _virusScanningSettings;
        private readonly AwsS3Options _awsS3Options;
        private readonly ICustomFileFormatInspector _customFileFormatInspector;
        private readonly IVirusScannerClient _virusScannerClient;

        private readonly IAmazonS3 _s3Client;

        public FileService(IOptionsSnapshot<VirusScanningSettings> virusScanningSettings, ICustomFileFormatInspector customFileFormatInspector,
            IAmazonS3 s3Client, IVirusScannerClient virusScannerClient,
            IOptionsSnapshot<AwsS3Options> awsS3Options)
        {
            _virusScanningSettings = virusScanningSettings.Value;
            _customFileFormatInspector = customFileFormatInspector;
            _s3Client = s3Client;
            _virusScannerClient = virusScannerClient;
            _awsS3Options = awsS3Options.Value;
        }

        public async Task<ProcessFileResult> ProcessFile(IFormFile file, UploadSectionSettings settings)
        {
            var fileName = file.FileName;
            await ScanFile(file.OpenReadStream(), fileName);

            await using var fileStream = file.OpenReadStream();
            var result = VerifyFile(fileStream, fileName, settings);

            if (result == null)
                throw new InvalidFileException("File type not accepted");

            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Position = 0;
            var fileId = await UploadFile(fileStream, Path.GetExtension(fileName).Replace(".", ""));

            return new ProcessFileResult
            {
                FileId = fileId,
                MimeType = result.MediaType
            };
        }

        public async Task<ProcessFileResult> ProcessPdfFile(MemoryStream fileStream, string fileName)
        {
            using (MemoryStream scanFileStream = new MemoryStream())
            {
                fileStream.CopyTo(scanFileStream);
                await ScanFile(scanFileStream, fileName);
            }

            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.Position = 0;
            var fileId = await UploadFile(fileStream, "pdf");

            return new ProcessFileResult
            {
                FileId = fileId,
                MimeType = "application/pdf"
            };
        }

        public async Task DeleteFile(string fileName)
        {
            var request = new DeleteObjectRequest
            {
                BucketName = _awsS3Options.AWS_BUCKET_NAME,
                Key = fileName
            };

            await _s3Client.DeleteObjectAsync(request);
        }

        public async Task<GetFileResult> GetFile(Guid fileId, string extension)
        {
            var request = new GetObjectRequest
            {
                BucketName = _awsS3Options.AWS_BUCKET_NAME,
                Key = $"{fileId}{extension}"
            };

            using var result = await _s3Client.GetObjectAsync(request);

            using var memoryStream = new MemoryStream();
            await result.ResponseStream.CopyToAsync(memoryStream);

            return new GetFileResult
            {
                ContentType = result.Headers.ContentType,
                FileBytes = memoryStream.ToArray()
            };
        }

        private FileFormat VerifyFile(Stream file, string fileName, UploadSectionSettings settings)
        {
            var fileSizeByteLimit = settings.MaximumFileSizeMb * 1048576;

            if (file.Length > fileSizeByteLimit)
                throw new InvalidFileException($"The file must be smaller than {settings.MaximumFileSizeMb}MB");

            return VerifyFileFormat(file, fileName, settings);
        }

        private FileFormat VerifyFileFormat(Stream file, string fileName, UploadSectionSettings settings)
        {
            var extension = Path.GetExtension(fileName).Replace(".", "");

            if (!settings.AcceptedFileTypes.Contains(extension.ToLower()))
                return null;

            return _customFileFormatInspector.GetFileFormat(file, extension, settings);
        }

        private async Task ScanFile(Stream file, string fileName)
        {
            if (!_virusScanningSettings.VIRUS_SCANNING_ENABLED)
                return;

            var result = await _virusScannerClient.ScanAsync(file, fileName, CancellationToken.None);

            if (result.ScanResult != VirusScanner.Client.Enums.EVirusScanResult.Ok)
            {
                throw new InvalidFileException("Virus detected");
            }
        }

        private async Task<Guid> UploadFile(Stream file, string fileExtension)
        {
            var fileId = Guid.NewGuid();

            var request = new PutObjectRequest
            {
                BucketName = _awsS3Options.AWS_BUCKET_NAME,
                InputStream = file,
                Key = $"{fileId}.{fileExtension}"
            };

            await _s3Client.PutObjectAsync(request);

            return fileId;
        }
    }
}
