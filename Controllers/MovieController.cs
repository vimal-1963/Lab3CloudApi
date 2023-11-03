using Microsoft.AspNetCore.Mvc;

namespace MVCApplication.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/addMovie")]
        public IActionResult AddMovie()
        {
            return View();
        }
    }
}
