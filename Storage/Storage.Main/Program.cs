using Microsoft.EntityFrameworkCore;
using Storage.Database;

namespace Storage.Main;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<StorageContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("Currency"));
        });

        builder.Services.AddDateOnlyTimeOnlyStringConverters();
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
