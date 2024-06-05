using Microsoft.EntityFrameworkCore;
using SolarApp.Models;

namespace SolarApp.Data;

public class SolarWatchDbContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunriseSunset> SunriseSunsets { get; set; }

    public SolarWatchDbContext(DbContextOptions<SolarWatchDbContext> options) : base(options)
    {
    }
}