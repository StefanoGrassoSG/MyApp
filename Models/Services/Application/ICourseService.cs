using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ICourseService 
    {
        Task<List<CourseViewModel>> GetCourses(CourseListInputModel courseListInputModel);
        Task<CourseDetailViewModel> GetCourse(int id);
    }
}