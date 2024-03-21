using System.Text;
using Crawler.Core.CentralBank;
using Crawler.Database;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Main;

public class Program
{
    public static void Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var builder = WebApplication.CreateBuilder(args);

        string? hangfireConnectionString = builder.Configuration.GetConnectionString("Hangfire");

        builder.Services.AddHttpClient<CbrClient>(client =>
        {
            client.BaseAddress = new Uri(@"http://www.cbr.ru/");
        });

        builder.Services.AddDbContext<HangfireContext>(options =>
        {
            options.UseNpgsql(hangfireConnectionString);
        });

        builder.Services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();

            config.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(hangfireConnectionString);
            });
        });

        builder.Services.AddEntityFrameworkNpgsql();
        builder.Services.AddHangfireServer();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
