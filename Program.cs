internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        
        var env = app.Services.GetRequiredService<IWebHostEnvironment>();

        if(env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        }
        app.UseStaticFiles();
        app.MapGet("/", () => "Hello from development!");

        app.Run();
    }
}