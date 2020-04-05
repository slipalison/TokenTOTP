using System.Collections.Generic;

namespace TokenTOTP.API.Services.DTOs
{
    public class NotificationRequest
    {
        public List<Chennel> Channels { get; set; }
    }

    public class Chennel
    {
        public NotificationType Type { get; set; }
        public string To { get; set; }
    }
}