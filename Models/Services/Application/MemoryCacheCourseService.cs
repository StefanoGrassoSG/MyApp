using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class MemoryCacheCourseService : ICachedCourseService
    {
        private readonly ICourseService courseService;
        private readonly IMemoryCache memoryCache;
        private readonly IOptionsMonitor<CacheTimeOptions> cacheoptions;
        public MemoryCacheCourseService(ICourseService courseService, IMemoryCache memoryCache, IOptionsMonitor<CacheTimeOptions> cacheOptions)
        {
            this.courseService = courseService;
            this.memoryCache = memoryCache;
            this.cacheoptions = cacheOptions;
        }
        public Task<CourseDetailViewModel> GetCourse(int id)
        {
            try
            {   
                return courseService.GetCourse(id);
            }
            catch
            {
                throw new CourseNotFoundException(id);
            }
        }

        public Task<ListViewModel<CourseViewModel>> GetCourses(CourseListInputModel courseListInputModel)
        {
            bool canCache = courseListInputModel.Page <= 5 && string.IsNullOrEmpty(courseListInputModel.Search);
            if(canCache) 
            {
                return memoryCache.GetOrCreateAsync($"Courses{courseListInputModel.Search}+{courseListInputModel.Page}+{courseListInputModel.Orderby}+{courseListInputModel.Ascending}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheoptions.CurrentValue.Time));
                    return courseService.GetCourses(courseListInputModel);
                });
            }

            return courseService.GetCourses(courseListInputModel);
        }
        public Task<List<CourseViewModel>> GetMostRecentCourses()
        {
            return memoryCache.GetOrCreateAsync($"MostRecentCourses", cacheEntry => 
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheoptions.CurrentValue.Time));
                return courseService.GetMostRecentCourses();
            });
        }
        public Task<List<CourseViewModel>> GetBestRatingCourses()
        {
            return memoryCache.GetOrCreateAsync($"BestRatingCourses", cacheEntry => 
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheoptions.CurrentValue.Time));
                return courseService.GetBestRatingCourses();
            });
        }

        public Task<CourseDetailViewModel> CreateCourseAsync(CourseCreateInputModel model)
        {
            return courseService.CreateCourseAsync(model);
        }

        public Task<bool> IsTitleAvailableAsync(string title, int id = 0)
        {
            return courseService.IsTitleAvailableAsync(title, id);
        }

        public Task<CourseEditInputModel> GetCourseEditAsync(int id)
        {
            return courseService.GetCourseEditAsync(id);
        }

        public async Task<CourseDetailViewModel> EditCourseAsync(CourseEditInputModel model)
        {
           CourseDetailViewModel viewModel = await courseService.EditCourseAsync(model);
           memoryCache.Remove($"Course{model.Id}");
           return viewModel;
        }
    }
}