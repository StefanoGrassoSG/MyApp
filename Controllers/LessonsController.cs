using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Controllers
{
    public class LessonsController : Controller
    {
        private readonly ILessonService service;
        private readonly ICachedLessonService cachedLessonService;

        public LessonsController(ILessonService service, ICachedLessonService cachedLessonService)
        {
            this.service = service;
            this.cachedLessonService = cachedLessonService;
        }
        public async Task<IActionResult> Detail(int id) 
        {
            LessonDetailViewModel viewModel = await cachedLessonService.GetLesson(id);
            ViewData["title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}