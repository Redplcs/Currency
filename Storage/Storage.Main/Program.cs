using Microsoft.EntityFrameworkCore;
using Storage.Core;
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
        builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        builder.Services.AddScoped<ICurrencyExchangeRateRepository, CurrencyExchangeRateRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<StorageContext>();
            context.Database.Migrate();
        }

        app.Run();
    }
}
