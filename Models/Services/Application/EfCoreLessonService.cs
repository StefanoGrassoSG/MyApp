using System.Data.SqlClient;
using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.Entities;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.Services.Infrastructure;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class EfCoreLessonService : ILessonService
    {
        private readonly WebAppDbContext dbContext;
        private readonly ILogger logger;
        public EfCoreLessonService(WebAppDbContext dbContext, ILogger<EfCoreCourseService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<LessonDetailViewModel> GetLesson(int id)
        {
             var lesson = await dbContext.Lessons.AsNoTracking()
                .Where(obj => obj.Id == id)
                .Select(obj => new LessonDetailViewModel
                {
                    Description = obj.Description,
                    Title = obj.Title,
                    Duration = TimeSpan.Parse(obj.Duration)
                }).SingleOrDefaultAsync();

            if (lesson == null)
            {
                throw new LessonNotFoundException(id);
            }

            return lesson;
        }
    }
}