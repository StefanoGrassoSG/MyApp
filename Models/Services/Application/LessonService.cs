using System;
using WebAppCourse.Models.Enums;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class LessonService : ILessonService
    {
        public Task<LessonDetailViewModel> GetLesson(int id)
        {
            throw new NotImplementedException();
        }
    }
}