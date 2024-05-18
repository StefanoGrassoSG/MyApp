using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService service;
        public CoursesController(ICourseService service)
        {
            this.service = service;
        }
        public IActionResult Index() 
        {
            List<CourseViewModel> courses = service.GetCourses();
            ViewData["Title"] = "Catalogo dei corsi";
            return View(courses);
        }

        public IActionResult Detail(int id)
        {
            CourseDetailViewModel viewModel = service.GetCourse(id);
            ViewData["title"] = viewModel.Title;
            return View(viewModel);
        }
    }
}