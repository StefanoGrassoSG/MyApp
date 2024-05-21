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

        public async Task<ListViewModel<CourseViewModel>> GetCourses(CourseListInputModel courseListInputModel)
        {
            IQueryable<Course> baseQuery = dbContext.Courses;

            switch(courseListInputModel.Orderby)
            {
                case "Title":
                    if(courseListInputModel.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.Title);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.Title);
                    }
                    break;
                case "Id":
                    if(courseListInputModel.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.Id);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.Id);
                    }
                    break;
                case "Rating":
                    if(courseListInputModel.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.Rating);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.Rating);
                    }
                    break;
                case "CurrentPrice":
                    if(courseListInputModel.Ascending)
                    {
                        baseQuery = baseQuery.OrderBy(obj => obj.CurrentPrice.Amount);
                    }
                    else
                    {
                        baseQuery = baseQuery.OrderByDescending(obj => obj.CurrentPrice.Amount);
                    }
                    break;
            }

            IQueryable<CourseViewModel> queryLinq =  baseQuery.Where(obj => obj.Title.Contains(courseListInputModel.Search)).AsNoTracking().Select(obj => new CourseViewModel {
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
            int count = await queryLinq.CountAsync();
            List<CourseViewModel> courses = await queryLinq.Skip(courseListInputModel.Offset).Take(courseListInputModel.Limit).ToListAsync();
            ListViewModel<CourseViewModel> results = new ListViewModel<CourseViewModel>
            {
                Results = courses,
                TotalCount = count
            };

            return results;
        }
        public async Task<List<CourseViewModel>> GetMostRecentCourses()
        {
            CourseListInputModel inputModel = new CourseListInputModel
            (
                search: "",
                page: 1,
                orderby: "Id",
                ascending: false,
                limit: courseOptions.CurrentValue.InHome,
                coursesOptions: courseOptions.CurrentValue
            );

            ListViewModel<CourseViewModel> result = await GetCourses(inputModel);
            return result.Results;
        }
        public async Task<List<CourseViewModel>> GetBestRatingCourses()
        {
              CourseListInputModel inputModel = new CourseListInputModel
            (
                search: "",
                page: 1,
                orderby: "Rating",
                ascending: false,
                limit: courseOptions.CurrentValue.InHome,
                coursesOptions: courseOptions.CurrentValue
            );

            ListViewModel<CourseViewModel> result = await GetCourses(inputModel);
            return result.Results;
        }
    }
}