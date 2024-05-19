using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using WebAppCourse.Models.Entities;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.Services.Infrastructure;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly WebAppDbContext dbContext;
        private readonly ILogger logger;
        public EfCoreCourseService(WebAppDbContext dbContext, ILogger<EfCoreCourseService> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        } 
        public async Task<CourseDetailViewModel> GetCourse(int id)
        {
                var course = await dbContext.Courses.AsNoTracking()
                .Where(obj => obj.Id == id)
                .Select(obj => new CourseDetailViewModel
                {
                    Id = obj.Id,
                    Title = obj.Title,
                    ImagePath = obj.ImagePath,
                    Author = obj.Author,
                    Rating = obj.Rating,
                    CurrentPrice = new Money
                    {
                        Amount = obj.CurrentPrice.Amount,
                        Currency = obj.CurrentPrice.Currency
                    },
                    FullPrice = new Money
                    {
                        Amount = obj.FullPrice.Amount,
                        Currency = obj.FullPrice.Currency
                    },
                    Description = obj.Description,
                    Lessons = obj.Lessons.Select(lesson => new LessonViewModel
                    {
                        Id = lesson.Id,
                        Title = lesson.Title,
                        Description = lesson.Description,
                        Duration = TimeSpan.Parse(lesson.Duration)
                    }).ToList()
                }).SingleOrDefaultAsync();

            if (course == null)
            {
                throw new CourseNotFoundException(id);
            }

            return course;
        }

        public async Task<List<CourseViewModel>> GetCourses()
        {
            IQueryable<CourseViewModel> queryLinq =  dbContext.Courses.AsNoTracking().Select(obj => new CourseViewModel {
                Id = obj.Id,
                Title = obj.Title,
                ImagePath = obj.ImagePath,
                Author = obj.Author,
                Rating = obj.Rating,
                CurrentPrice = new Money
                {
                    Amount = obj.CurrentPrice.Amount,
                    Currency = obj.CurrentPrice.Currency
                },
                FullPrice = new Money
                {
                    Amount = obj.FullPrice.Amount,
                    Currency = obj.FullPrice.Currency
                }
            });

            List<CourseViewModel> courses = await queryLinq.ToListAsync();

            return courses;
        }
    }
}