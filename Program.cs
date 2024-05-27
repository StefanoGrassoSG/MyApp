using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
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
        builder.Services.AddResponseCaching();
        builder.Services.AddMvc(options => 
        {   
            var homeProfile = new CacheProfile();
            homeProfile.Duration = builder.Configuration.GetValue<int>("ResponseCache:Home:Duration");
            homeProfile.Location = builder.Configuration.GetValue<ResponseCacheLocation>("ResponseCache:Home:Location");
            homeProfile.VaryByQueryKeys = builder.Configuration.GetValue<string[]>("ResponseCache:Home:VaryByQueryKeys");
            //homeProfile.VaryByQueryKeys = new string[] {"page"};
            options.CacheProfiles.Add("Home", homeProfile);
        });
       // builder.Services.AddControllersWithViews();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

        //Scegliere ADO.NET oppure Entity framework core
        var option = "Ef";
        switch(option)
        {
            case "Adonet":
                builder.Services.AddTransient<ICourseService, AdoNetCourseService>();
                builder.Services.AddTransient<IDatabase, SqlDatabaseAccessor>();
            break;

            case "Ef":
                builder.Services.AddTransient<ICourseService, EfCoreCourseService>();
                builder.Services.AddDbContextPool<WebAppDbContext>(optionsBuilder => {
                    string connectionStringWrong = builder.Configuration.GetConnectionString("default")!;
                    string connectionString = connectionStringWrong.Replace(@"\\", @"\");
                    optionsBuilder.UseSqlServer(connectionString);
        });
            break;
        }
     
        builder.Services.AddTransient<ICachedCourseService, MemoryCacheCourseService>();
        builder.Services.AddSingleton<IImageSaver, MagickNetImageSaver>();
        builder.Configuration.AddJsonFile("appsettings.json");
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddSingleton<RequestCounterService>();
        builder.Services.Configure<ConnectionStringsOptions>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<CoursesOptions>(builder.Configuration.GetSection("Courses"));
        builder.Services.Configure<CacheTimeOptions>(builder.Configuration.GetSection("Cache"));
        builder.Services.Configure<KestrelServerOptions>(builder.Configuration.GetSection("Kestrel"));
        var app = builder.Build();
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
        else {
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
        }
        app.UseMiddleware<RequestCountingMiddleware>();
        app.UseStaticFiles();
        app.UseResponseCaching();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}