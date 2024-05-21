using WebAppCourse.Models.InputModels;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseListViewModel
    {
        public List<CourseViewModel> Courses {get;set;}
        public CourseListInputModel Input {get;set;}
    }
}