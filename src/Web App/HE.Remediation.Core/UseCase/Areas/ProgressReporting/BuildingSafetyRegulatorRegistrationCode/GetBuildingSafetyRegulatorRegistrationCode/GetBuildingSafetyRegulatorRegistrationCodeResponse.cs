namespace HE.Remediation.Core.UseCase.Areas.ProgressReporting.BuildingSafetyRegulatorRegistrationCode.GetBuildingSafetyRegulatorRegistrationCode
{
    public class GetBuildingSafetyRegulatorRegistrationCodeResponse
    {
        public string ApplicationReferenceNumber { get; set; }
        public string BuildingName { get; set; }
        public string BuildingSafetyRegulatorRegistrationCode { get; set; }
        public int Version { get; set; }
        public bool HasVisitedCheckYourAnswers { get; set; }
    }
}
