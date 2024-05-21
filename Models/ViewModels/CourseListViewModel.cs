using WebAppCourse.Models.InputModels;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseListViewModel
    {
        public ListViewModel<CourseViewModel> Courses {get;set;}
        public CourseListInputModel Input {get;set;}
    }
}