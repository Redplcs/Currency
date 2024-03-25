using Microsoft.EntityFrameworkCore;

namespace Crawler.Database;

public class HangfireContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Timestamp> Timestamps => Set<Timestamp>();
}
