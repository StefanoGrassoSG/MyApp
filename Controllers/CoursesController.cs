using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;

namespace WebAppCourse.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index() => View(); 

        public IActionResult Detail(string id) => View();
    }
}