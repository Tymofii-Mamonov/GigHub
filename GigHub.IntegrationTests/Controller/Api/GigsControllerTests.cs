using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.Persistance;
using GigHub.Tests.Extensions;
using NUnit.Framework;
using System;
using System.Linq;

namespace GigHub.IntegrationTests.Controller.Api
{
    [TestFixture]
    public class GigsControllerTests
    {
        private ApplicationDbContext _context;
        private GigsController _controller;

        [SetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Cancel_WhenCalled_ShouldMarkCurrentGigAsCancelled()
        {
            // Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUser(user.Id, user.Name);

            var genre = _context.Genres.Single(g => g.Id == 1);
            var gig = new Gig
            {
                Artist = user,
                DateTime = DateTime.Now.AddDays(1),
                Genre = genre,
                Venue = "-"
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            // Act
            _controller.Cancel(gig.Id);

            // Assert
            _context.Entry(gig).Reload();
            gig.IsCanceled.Should().Be(true);
        }
    }
}
