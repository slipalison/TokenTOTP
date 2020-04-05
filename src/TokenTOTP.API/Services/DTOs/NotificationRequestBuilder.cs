namespace TokenTOTP.API.Services.DTOs
{
    public class NotificationRequestBuilder
    {
        internal NotificationRequestBuilder UseEmailChannel(string email)
        {
            return this;
        }

        internal NotificationRequestBuilder UseTemplate(string templateId)
        {
            return this;
        }

        internal NotificationRequestBuilder AddParameter(string v, string name)
        {
            return this;
        }

        internal NotificationRequest Build()
        {
            return new NotificationRequest();
        }
    }
}