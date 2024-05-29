using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Models.Exceptions;

namespace WebAppCourse.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index() 
        {
            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if(feature != null)
            {
                switch(feature.Error)
                {
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");
                case LessonNotFoundException exc:
                    ViewData["Title"] = "Lezione non trovata";
                    Response.StatusCode = 404;
                    return View("LessonNotFound");
                default:
                    ViewData["Title"] = "Errore";
                    return View();
                }
            }
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewData["Title"] = "Pagina non trovata";
                    return View("PageNotFound");
                default:
                    ViewData["Title"] = "Errore";
                    return View("Error");
            }
        }
    }
}