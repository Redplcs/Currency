using Crawler.Core.CentralBank;

namespace Crawler.Main;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient<CbrClient>(client =>
        {
            client.BaseAddress = new Uri(@"http://www.cbr.ru/");
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
