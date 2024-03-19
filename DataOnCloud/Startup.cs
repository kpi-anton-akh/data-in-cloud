using Autofac;
using DataInCloud.Dal;
using DataInCloud.Dal.Car;
using DataInCloud.Model.Car;
using DataInCloud.Orchestrators;
using DataInCloud.Orchestrators.Car;
using Microsoft.EntityFrameworkCore;

namespace DataOnCloud.Api;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(
        IConfiguration configuration,
        IWebHostEnvironment env)
    {
        _configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile($"appsettings.json", false, true)
            .Build();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cars API v1");
        });

        app.UseRouting();
        app.UseEndpoints(endpoint => endpoint.MapControllers());
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();
        services.AddAutoMapper(config =>
        {
            config.AddProfile(typeof(DaoMapper));
            config.AddProfile(typeof(OrchestratorMapper));
        });

        ConfigureDb(services);
    }

    protected virtual void ConfigureDb(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(c => c.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
    }

    public void ConfigureContainer(ContainerBuilder builder)
    {
        builder.RegisterType<CarOrchestrator>().As<ICarOrchestrator>();
        builder.RegisterType<CarRepository>().As<ICarRepository>();
    }
}