using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.ViewModels;

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
        public async Task<IActionResult> Index([FromServices] ICachedCourseService courseService) 
        {    
            ViewData["Title"] = "SkillMe";
            int requestCount = _requestCounterService.GetRequestCount();
            List<CourseViewModel> bestRating = await courseService.GetBestRatingCourses();
            List<CourseViewModel> mostrecent = await courseService.GetMostRecentCourses();

            HomeViewModel viewModel = new HomeViewModel
            {
                BestRatingCourses = bestRating,
                MostRecentCourses = mostrecent
            };
            return View(viewModel);
        }
    }
}