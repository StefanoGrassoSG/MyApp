using System.Xml.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebAppCourse.Models.Entities;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.Services.Infrastructure;
using WebAppCourse.Models.ValueTypes;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly WebAppDbContext dbContext;
        private readonly ILogger logger;
        private readonly IOptionsMonitor<CoursesOptions> courseOptions;
        public EfCoreCourseService(WebAppDbContext dbContext, ILogger<EfCoreCourseService> logger, IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.courseOptions = coursesOptions;
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

        public async Task<List<CourseViewModel>> GetCourses(string search, int page, string orderby, bool ascending)
        {
            search = search ?? "";
            page = Math.Max(1, page);
            int limit = courseOptions.CurrentValue.PerPage;
            int offset = (page - 1) * limit;
            var orderOptions = courseOptions.CurrentValue.Order;
            if(!orderOptions.Allow.Contains(orderby)) 
            {
                orderby = orderOptions.By;
                ascending = orderOptions.Ascending;
            }

            IQueryable<Course> baseQuery = dbContext.Courses;

            switch(orderby)
            {
                case "Title":
                    if(ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.Title);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.Title);
                    }
                    break;
                case "Rating":
                    if(ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.Rating);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.Rating);
                    }
                    break;
                case "CurrentPrice":
                    if(ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.CurrentPrice.Amount);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.CurrentPrice.Amount);
                    }
                    break;
            }

            IQueryable<CourseViewModel> queryLinq =  baseQuery.Where(obj => obj.Title.Contains(search)).AsNoTracking().Select(obj => new CourseViewModel {
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
            }).Skip(offset).Take(limit);

            List<CourseViewModel> courses = await queryLinq.ToListAsync();

            return courses;
        }
    }
}