using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ICourseService 
    {
        Task<List<CourseViewModel>> GetCourses(string search,int page, string orderby, bool ascending);
        Task<CourseDetailViewModel> GetCourse(int id);
    }
}