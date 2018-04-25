using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetAllUpcomingGigs();

            if (!string.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs
                    .Where(g => g.Artist.Name.Contains(query) ||
                                g.Genre.Name.Contains(query) ||
                                g.Venue.Contains(query));
            }

            var viewModel = new GigsViewModel
            {
                SearchTerm = query,
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                Attendances = _unitOfWork.Attendances
                    .GetFutureAttendances(User.Identity.GetUserId())
                    .ToLookup(a => a.GigId)
            };

            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}