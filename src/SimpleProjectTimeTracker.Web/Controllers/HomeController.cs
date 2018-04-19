using Microsoft.AspNetCore.Mvc;

namespace SimpleProjectTimeTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}