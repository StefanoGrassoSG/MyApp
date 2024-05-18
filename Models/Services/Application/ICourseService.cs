using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ICourseService 
    {
        List<CourseViewModel> GetCourses();
        CourseDetailViewModel GetCourse(int id);
    }
}