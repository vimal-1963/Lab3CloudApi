using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCApplication.Models;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MVCApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WebApplicationBuilder _webApplicationBuilder;
       

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }
        

        /*public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }*/

        [HttpGet("/")]
        public IActionResult Signin()
        {
            return View();
        }
        [HttpGet("/signin")]
        public IActionResult Home()
        {
            string username = HttpContext.Session.GetString("username");

            if (username != null || username != "")
            {
                return View("LoginSuccess");
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost("/signin")]
        public async Task<IActionResult> Signin(IFormCollection form )
        {
            string email = form["email"]!;
            string password = form["password"]!;

            var builder = BuilderContainer.builder;

            string pwdFunc = await new DbOperations(builder).Login(email);
            if(pwdFunc != null)
            {
                
                if (password.Equals(pwdFunc))
                {
                    // Store username and password in session
                    HttpContext.Session.SetString("username", email);
                    HttpContext.Session.SetString("password", password);
                    return View("LoginSuccess");
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
            
        }

        [HttpGet("/signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost("/signup")]
        public async  Task<IActionResult> Signup(IFormCollection form)
        {
            string firstname = form["firstname"]!;
            string lastname = form["lastname"]!;
            string email = form["email"]!;
            string password = form["password"]!;

            var builder = BuilderContainer.builder;

            bool status = await new DbOperations(builder).Signup(firstname,lastname,email,password);
            if(status)
            {
                return Redirect("/");
            }
            else
            {
                return  Redirect("/signup");
            }
            
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}