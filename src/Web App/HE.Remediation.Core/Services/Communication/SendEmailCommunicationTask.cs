namespace HE.Remediation.Core.Services.Communication
{
    public class SendEmailCommunicationTask : IDisposable
    {
        public SendEmailCommunicationTask(EmailCommunicationRequest emailCommunicationRequest)
       : this(emailCommunicationRequest, Guid.NewGuid())
        {
        }

        public SendEmailCommunicationTask(EmailCommunicationRequest emailCommunicationRequest, Guid traceId)
        {
            EmailCommunicationRequest = emailCommunicationRequest;
            TraceId = traceId;
        }

        public EmailCommunicationRequest EmailCommunicationRequest { get; }
        public Guid TraceId { get; }

        public void Dispose()
        {

        }
    }
}
