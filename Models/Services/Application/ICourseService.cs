using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ICourseService 
    {
        Task<List<CourseViewModel>> GetCourses();
        Task<CourseDetailViewModel> GetCourse(int id);
    }
}