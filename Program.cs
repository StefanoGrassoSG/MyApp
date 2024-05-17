internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();
        builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
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

        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}