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
        public IActionResult Index() 
        {    
            int requestCount = _requestCounterService.GetRequestCount();
            return View(requestCount);
        }
    }
}