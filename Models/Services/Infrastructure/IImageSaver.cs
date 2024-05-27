namespace WebAppCourse.Models.Services.Infrastructure
{
    public interface IImageSaver
    {
        Task<string> SaveImage(int id, IFormFile file);
    }
}