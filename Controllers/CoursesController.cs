using Microsoft.AspNetCore.Mvc;

namespace WebAppCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index() 
        {
            return Content("ciao dall'index dei corsi");
        }

        public IActionResult Detail(string id) 
        {
            return Content($"sono detail, ho ricevuto l'id: {id}");
        }
    }
}