using Microsoft.AspNetCore.Mvc;

namespace MVCApplication.Controllers
{
    
    public class LoginController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
