using FluentAssertions;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Persistance.Repositories;
using GigHub.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;
using System.Linq;

namespace GigHub.Tests.Persistance.Repositories
{
    [TestClass]
    public class UserNotificationRepositoryTests
    {
        private Mock<DbSet<UserNotification>> _mockUNotifications;
        private UserNotificationRespository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUNotifications = new Mock<DbSet<UserNotification>>();

            var _context = new Mock<IApplicationDbContext>();
            _context.SetupGet(c => c.UserNotifications).Returns(_mockUNotifications.Object);

            _repository = new UserNotificationRespository(_context.Object);
        }

        [TestMethod]
        public void GetNotifications_NotificationIsRead_ShouldBeEmpty()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser {Id = "1"};
            var userNotification = new UserNotification(user, notification);
            userNotification.Read();

            _mockUNotifications.SetSource(new[] {userNotification});

            var notifications = _repository.GetNotifications(userNotification.UserId);

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNotifications_GettingNotificationsForDifferentUser_ShouldBeEmpty()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotifications(userNotification.UserId + "-");

            notifications.Should().BeEmpty();
        }

        [TestMethod]
        public void GetNotifications_GettingNotificationsForCurrentUser_ShouldReturnNotification()
        {
            var notification = Notification.GigCanceled(new Gig());
            var user = new ApplicationUser { Id = "1" };
            var userNotification = new UserNotification(user, notification);

            _mockUNotifications.SetSource(new[] { userNotification });

            var notifications = _repository.GetNotifications(userNotification.UserId);

            notifications.Should().HaveCount(1);
            notifications.First().Should().Be(notification);

        }
    }
}
