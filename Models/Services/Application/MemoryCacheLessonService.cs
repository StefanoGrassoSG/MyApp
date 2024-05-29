using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using WebAppCourse.Models.Exceptions;
using WebAppCourse.Models.InputModels;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.ViewModels;

namespace WebAppCourse.Models.Services.Application
{
    public class MemoryCacheLessonService : ICachedLessonService
    {
        private readonly ILessonService lessonService;
        private readonly IMemoryCache memoryCache;
        private readonly IOptionsMonitor<CacheTimeOptions> cacheoptions;

        public MemoryCacheLessonService(ILessonService lessonService, IMemoryCache memoryCache, IOptionsMonitor<CacheTimeOptions> cacheoptions)
        {
            this.lessonService = lessonService;
            this.memoryCache = memoryCache;
            this.cacheoptions = cacheoptions;
        }

        public Task<LessonDetailViewModel> GetLesson(int id)
        {
            try
            {   
                return memoryCache.GetOrCreateAsync($"Lesson{id}", cacheEntry =>
                {
                    cacheEntry.SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheoptions.CurrentValue.Time));
                    return lessonService.GetLesson(id);
                });
            }
            catch
            {
                throw new LessonNotFoundException(id);
            }
        }
    }
}