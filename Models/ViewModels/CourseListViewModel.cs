using WebAppCourse.Models.InputModels;

namespace WebAppCourse.Models.ViewModels
{
    public class CourseListViewModel : IPagination
    {
        public ListViewModel<CourseViewModel> Courses {get;set;}
        public CourseListInputModel Input {get;set;}

        int IPagination.currentPage => Input.Page;

        int IPagination.TotalResults => Courses.TotalCount;

        int IPagination.ResultPerPage => Input.Limit;

        string IPagination.Search => Input.Search;

        string IPagination.OrderBy => Input.Orderby;

        bool IPagination.Ascending => Input.Ascending;
    }
}