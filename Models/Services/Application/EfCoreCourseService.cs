using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using WebAppCourse.Models.Entities;
using WebAppCourse.Models.Services.Infrastructure;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly WebAppDbContext dbContext;
        public EfCoreCourseService(WebAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        } 
        public async Task<CourseDetailViewModel> GetCourse(int id)
        {
           CourseDetailViewModel course = await dbContext.Courses.AsNoTracking().Where(obj => obj.Id == id).Select(obj => new CourseDetailViewModel {
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
                Lessons = obj.Lessons.Select(obj => new LessonViewModel
                {
                    Id = obj.Id,
                    Title = obj.Title,
                    Description = obj.Description,
                    Duration = TimeSpan.Parse(obj.Duration)
                }).ToList()
           }).SingleAsync();

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