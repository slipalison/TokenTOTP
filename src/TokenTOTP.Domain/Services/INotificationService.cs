using System.Threading.Tasks;
using TokenTOTP.Domain.Services.DTOs;

namespace TokenTOTP.Domain.Services
{
    public interface INotificationService
    {
        Task SendAsync(NotificationRequest notificationRequest, string v);
    }

    public class NotificationService : INotificationService
    {
        public async Task SendAsync(NotificationRequest notificationRequest, string v)
        {
            await Task.Run(() => Task.CompletedTask);
        }
    }
}
