using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ICourseService 
    {
        Task<ListViewModel<CourseViewModel>> GetCourses(CourseListInputModel courseListInputModel);
        Task<CourseDetailViewModel> GetCourse(int id);
        Task<List<CourseViewModel>> GetBestRatingCourses();
        Task<List<CourseViewModel>> GetMostRecentCourses();
        Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model);
    }
}