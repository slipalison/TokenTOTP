using System.Threading.Tasks;
using TokenTOTP.API.Services.DTOs;

namespace TokenTOTP.API.Services
{
    public interface INotificationService
    {
        Task SendAsync(NotificationRequest notificationRequest, string v);
    }

    public class NotificationService : INotificationService
    {
        public async Task SendAsync(NotificationRequest notificationRequest, string v)
        {
        }
    }
}