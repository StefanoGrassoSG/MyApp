using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public interface ILessonService 
    {
        Task<LessonDetailViewModel> GetLesson(int id);
    }
}