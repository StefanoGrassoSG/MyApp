
using ImageMagick;

namespace WebAppCourse.Models.Services.Infrastructure
{
    public class MagickNetImageSaver : IImageSaver
    {
        private readonly IWebHostEnvironment env;
        private readonly SemaphoreSlim sem;
        public MagickNetImageSaver(IWebHostEnvironment env)
        {
            this.env = env;
            ResourceLimits.Width = 4000;
            ResourceLimits.Height = 4000;
            sem = new SemaphoreSlim(2);
        }
        public async Task<string> SaveImage(int id, IFormFile file)
        {
            string path = $"{id}.jpg";
            string physPath = Path.Combine(env.WebRootPath, $"{id}.jpg");
            
            using Stream inputStream = file.OpenReadStream();
            using var image = new MagickImage(inputStream);

            await sem.WaitAsync();
            int width = 300; int height = 300;// TODO metterli come configurazione, non cablarli nel codice

            try {
                MagickGeometry resize = new MagickGeometry(width, height)
                {
                    FillArea = true
                };
                image.Resize(resize);
                image.Crop(width, width, Gravity.Northwest);
                image.Quality = 70;
                image.Write(physPath, MagickFormat.Jpg);
            } finally {
                sem.Release();
            }
           
            return path;
        }
    }
}