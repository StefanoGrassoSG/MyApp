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
            switch(feature.Error)
            {
                case CourseNotFoundException exc:
                    ViewData["Title"] = "Corso non trovato";
                    Response.StatusCode = 404;
                    return View("CourseNotFound");
                default:
                    ViewData["Title"] = "Errore";
                    return View();
            }
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