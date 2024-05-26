using WebAppCourse.Models.InputModels;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseListViewModel : IPagination
    {
        public ListViewModel<CourseViewModel> Courses {get;set;} = new ListViewModel<CourseViewModel>();
        public CourseListInputModel Input {get;set;} = new CourseListInputModel();

        int IPagination.currentPage => Input.Page;

        int IPagination.TotalResults => Courses.TotalCount;

        int IPagination.ResultPerPage => Input.Limit;

        string IPagination.Search => Input.Search ?? string.Empty;

        string IPagination.OrderBy => Input.Orderby ?? string.Empty;

        bool IPagination.Ascending => Input.Ascending;
    }
}