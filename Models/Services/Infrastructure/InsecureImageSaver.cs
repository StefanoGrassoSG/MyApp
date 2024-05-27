
namespace WebAppCourse.Models.Services.Infrastructure
{
    public class InsecureImageSaver : IImageSaver
    {
        private readonly IWebHostEnvironment env;
        public InsecureImageSaver(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public async Task<string> SaveImage(int id, IFormFile file)
        {
            string path = $"{id}.jpg";
            string physPath = Path.Combine(env.WebRootPath, $"{id}.jpg");
            using FileStream stream = File.OpenWrite(physPath);

            await file.CopyToAsync(stream);

            return path;
        }
    }
}