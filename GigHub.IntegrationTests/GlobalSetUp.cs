using GigHub.Core.Models;
using GigHub.Persistance;
using NUnit.Framework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GigHub.IntegrationTests
{
    [SetUpFixture]
    public class GlobalSetUp
    {
        [SetUp]
        public void SetUp()
        {
            MigrateDbToLastestVersion();
            Seed();
        }

        private static void MigrateDbToLastestVersion()
        {
            var configuration = new GigHub.Migrations.Configuration();
            var migratior = new DbMigrator(configuration);
            migratior.Update();
        }

        public void Seed()
        {
            var context = new ApplicationDbContext();

            if (context.Users.Any())
                return;

            context.Users.Add(new ApplicationUser{UserName = "user1", Name = "user1", Email = "-", PasswordHash = "-"});
            context.Users.Add(new ApplicationUser{UserName = "user2", Name = "user2", Email = "-", PasswordHash = "-"});
            context.SaveChanges();
        }
    }
}
