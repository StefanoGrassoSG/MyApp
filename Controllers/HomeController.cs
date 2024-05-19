using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Services.Application;

namespace WebAppCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly RequestCounterService _requestCounterService;

        public HomeController(RequestCounterService requestCounterService)
        {
            _requestCounterService = requestCounterService;
        }
        [ResponseCache(CacheProfileName = "Home")]
        public IActionResult Index() 
        {    
            ViewData["Title"] = "SkillMe";
            int requestCount = _requestCounterService.GetRequestCount();
            return View(requestCount);
        }
    }
}