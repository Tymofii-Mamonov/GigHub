using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Persistance.Repositories
{
    public class UserNotificationRespository : IUserNotifications
    {
        private readonly IApplicationDbContext _context;
        public UserNotificationRespository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist);
        }

        public IEnumerable<UserNotification> GetUnreadUserNotifications(string userId)
        {
            return _context.UserNotifications
                .Where(n => n.UserId == userId && !n.IsRead);
        }
    }
}