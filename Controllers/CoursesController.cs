using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICachedCourseService service;
        private readonly ICourseService courseService;
        public CoursesController(ICachedCourseService service, ICourseService courseService)
        {
            this.service = service;
            this.courseService = courseService;
        }
        public async Task<IActionResult> Index(CourseListInputModel model) 
        {
            ListViewModel<CourseViewModel> courses = await service.GetCourses(model);
            ViewData["Title"] = "Catalogo dei corsi";
            CourseListViewModel viewModel = new CourseListViewModel
            {
                Courses = courses,
                Input = model
            };
            return View(viewModel);
        }

        public async Task<IActionResult> Detail(int id)
        {
            CourseDetailViewModel viewModel = await service.GetCourse(id);
            ViewData["title"] = viewModel.Title;
            return View(viewModel);
        }

        public IActionResult Create() 
        {   
            ViewData["Title"] = "Nuovo Corso";
            var inputModel = new CourseCreateInputModel();
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInputModel model) 
        {   
            if(ModelState.IsValid)
            {
                try 
                {
                    CourseDetailViewModel course = await courseService.CreateCourseAsync(model);
                    TempData["ConfirmationMessage"] = "Bene! hai creato il corso, ora perchè non lo completi con gli altri dati?";
                    return RedirectToAction(nameof(Edit), new {id = course.Id});
                }
                catch(CourseTitleUnavailableException ex)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), ex.Message);
                }
            }
            ViewData["Title"] = "Nuovo Corso";
            return View(model);
        }

        public async Task<IActionResult> IsTitleAvailable(string title, int id = 0)
        {
            bool result = await courseService.IsTitleAvailableAsync(title, id);
            return Json(result);
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Modifica corso";
            CourseEditInputModel inputModel = await courseService.GetCourseEditAsync(id);
            return View(inputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseEditInputModel model)
        {
            if(ModelState.IsValid)
            {
                try 
                {
                    CourseDetailViewModel course = await courseService.EditCourseAsync(model);
                    TempData["ConfirmationMessage"] = "I dati sono stati salvati con successo";
                    return RedirectToAction(nameof(Detail), new { id = model.Id});
                }
                catch(OptimisticException ex)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), ex.Message);
                }
                catch(CourseTitleUnavailableException ex)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), ex.Message);
                }
                catch(CourseImageInvalidException ex)
                {
                    ModelState.AddModelError(nameof(CourseDetailViewModel.Title), ex.Message);
                }
            }
            ViewData["Title"] = "Modifica corso";
            return View(model);
        }
    }
}