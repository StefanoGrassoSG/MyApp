using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService service;
        public CoursesController(ICachedCourseService service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index(string search, int page, string orderby, bool ascending) 
        {
            List<CourseViewModel> courses = await service.GetCourses(search, page, orderby, ascending);
            ViewData["Title"] = "Catalogo dei corsi";
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await service.GetCourse(id);
            ViewData["title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}