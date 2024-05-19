using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using WebAppCourse.Models.Exceptions;
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
            int TimeCached = cacheoptions.CurrentValue.Time;
            return memoryCache.GetOrCreateAsync($"Course{id}", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(TimeCached));
                try
                {
                    return courseService.GetCourse(id);
                }
                catch
                {
                    throw new CourseNotFoundException(id);
                }
                
            });
        }

        public Task<List<CourseViewModel>> GetCourses()
        {
            int TimeCached = cacheoptions.CurrentValue.Time;
            return memoryCache.GetOrCreateAsync($"Courses", cacheEntry =>
            {
                cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(TimeCached));
                return courseService.GetCourses();
            });
        }
    }
}