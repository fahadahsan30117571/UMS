using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

using UMS.DbContexts;
using UMS.Models;

namespace UMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProjectDbContext _context;

        public HomeController(ILogger<HomeController> logger, ProjectDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            SetUserId();
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVm vm)
        {
            var user = await _context.Users.FirstOrDefaultAsync(c => c.UserName == vm.UserName && c.Password == vm.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                SetUserId();
                return RedirectToAction("Index", "Student");
            }

            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SetUserId() => ViewBag.UserId = HttpContext.Session.GetString("UserId");
    }
}
