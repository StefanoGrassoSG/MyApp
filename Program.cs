using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAppCourse.Models.Options;
using WebAppCourse.Models.Services.Application;
using WebAppCourse.Models.Services.Infrastructure;
using WebAppCourse.Models.Services.Middleware;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
        //builder.Services.AddTransient<ICourseService, AdoNetCourseService>();
        builder.Services.AddTransient<ICourseService, EfCoreCourseService>();
        builder.Services.AddTransient<IDatabase, SqlDatabaseAccessor>();
        builder.Configuration.AddJsonFile("appsettings.json");
        string connectionString = builder.Configuration.GetConnectionString("default")!;
        builder.Services.AddDbContextPool<WebAppDbContext>(optionsBuilder => {
            optionsBuilder.UseSqlServer(connectionString);
        });
        builder.Services.AddSingleton<RequestCounterService>();
        builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<CoursesOptions>(builder.Configuration.GetSection("Courses"));
        var app = builder.Build();
        app.UseMiddleware<RequestCountingMiddleware>();
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();

     

        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();

            app.Lifetime.ApplicationStarted.Register(() => 
            {
                string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                string logMessage = $"Applicazione avviata il: {DateTime.Now}\n";
                logMessage += $"Ambiente: {env.EnvironmentName}\n";
                logMessage += $"Nome macchina: {Environment.MachineName}\n";
                logMessage += "----------------------------------------------\n";
                

                File.AppendAllText(filePath, logMessage);
            });
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}