using Microsoft.AspNetCore.Mvc;

namespace WebAppCourse.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
    }
}