/*using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
{
    // Регистрируем AppDbContext в DI-контейнере с использованием PostgreSQL
    services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

    services.AddScoped<AppDbContext>();
    services.AddControllers();
    // Другие сервисы, если потребуется.

    // Добавьте настройку сервиса, если она отсутствует.
}

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        // Настраиваем маршруты для контроллера NotesController
        app.UseRouting();
        app.UseAuthentication();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseMvc(routes =>
            {
                routes.MapRoute(name: "defaultApi", template: "api/{controller}/{action}");
                routes.MapRoute(name: "default", template: "{controller}/{action=Index}/{id?}");
            }
            );
        // Другие middleware и настройки.
    }
    // Другие методы класса Startup.
}*/
