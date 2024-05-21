using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.InputModels;
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
        public async Task<IActionResult> Index(CourseListInputModel model) 
        {
            List<CourseViewModel> courses = await service.GetCourses(model);
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