using AdsPlatformsAPI.Filters;

namespace AdsPlatformsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSwagger();

            builder.Services
                .AddRepositories()
                .AddServices();
            
            builder.Services.AddControllers(
                options => options.Filters.Add<ExceptionFilter>());

            var app = builder.Build();

            app.UseSwagger()
               .UseSwaggerUI(options =>
               {
                   options.SwaggerEndpoint("/swagger/AdsPlatforms/swagger.json", "AdsPlatforms WebApi");
                   options.RoutePrefix = string.Empty;
               });

            app.MapControllers();

            app.Run();
        }
    }
}
