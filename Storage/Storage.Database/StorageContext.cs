using Microsoft.EntityFrameworkCore;

namespace Storage.Database;

public class StorageContext(DbContextOptions<StorageContext> options) : DbContext(options)
{
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<CurrencyExchangeRate> ExchangeRates => Set<CurrencyExchangeRate>();
}
