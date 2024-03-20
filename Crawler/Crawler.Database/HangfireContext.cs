using Microsoft.EntityFrameworkCore;

namespace Crawler.Database;

public class HangfireContext(DbContextOptions options) : DbContext(options)
{
}
