using AdsPlatformsAPI.Filters;

namespace AdsPlatformsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(
                options => options.Filters.Add<ExceptionFilter>());

            var app = builder.Build();
            app.MapControllers();

            app.Run();
        }
    }
}
