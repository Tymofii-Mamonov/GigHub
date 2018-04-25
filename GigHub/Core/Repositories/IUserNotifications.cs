using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IUserNotifications
    {
        IEnumerable<Notification> GetNotifications(string userId);
        IEnumerable<UserNotification> GetUnreadUserNotifications(string userId);
    }
}