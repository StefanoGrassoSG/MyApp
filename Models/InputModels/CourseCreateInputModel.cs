using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebAppCourse.Controllers;

namespace WebAppCourse.Models.InputModels
{
    public class CourseCreateInputModel
    {
        [Required(ErrorMessage = "Il titolo è obbligatorio"),
        MinLength(10, ErrorMessage = "il titolo dev'essere di almeno {1} caratteri"),
        MaxLength(100, ErrorMessage = "Il titolo dev'essere di al massimo {1} caratteri"),
        RegularExpression(@"^[\w\s\.]+$", ErrorMessage = "Titolo non valido"),
        Remote(action: nameof(CoursesController.IsTitleAvailable), controller:"Courses", ErrorMessage ="Il titolo esiste già")]
        public string Title {get;set;} = string.Empty;

        [Required]
        public int Id {get;set;}
    }
}