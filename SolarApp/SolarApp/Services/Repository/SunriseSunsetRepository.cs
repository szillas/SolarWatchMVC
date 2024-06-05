using Microsoft.EntityFrameworkCore;
using SolarApp.Data;
using SolarApp.Models;

namespace SolarApp.Services.Repository;

public class SunriseSunsetRepository : ISunriseSunsetRepository
{
    private readonly SolarWatchDbContext _dbContext;

    public SunriseSunsetRepository(SolarWatchDbContext context)
    {
        _dbContext = context;
    }

    public async Task<IEnumerable<SunriseSunset>> GetAll()
    {
        return await _dbContext.SunriseSunsets.ToListAsync();
    }
    
    public async Task<SunriseSunset?> GetById(int id)
    {
        return await _dbContext.SunriseSunsets.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<SunriseSunset?> GetByDateAndCity(string city, DateTime date)
    {
        var sunriseSunset = await _dbContext.SunriseSunsets
            .Include(s => s.City)
            .FirstOrDefaultAsync(s => s.City.Name == city && s.Date == date);

        return sunriseSunset;
    }
    
    public async Task<SunriseSunset?> GetByDateAndCity(string city, string? date)
    {
        var parsedDate = DateParser(date);
        var sunriseSunset = await _dbContext.SunriseSunsets
            .Include(ss => ss.City) 
            .FirstOrDefaultAsync(ss => ss.City.Name == city && ss.Date == parsedDate);

        return sunriseSunset;
    }

    public async Task Add(SunriseSunset sunriseSunset)
    {
        _dbContext.Add(sunriseSunset);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(SunriseSunset sunriseSunset)
    {
        _dbContext.Remove(sunriseSunset);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(SunriseSunset sunriseSunset)
    {  
        var sunriseSunsetInDb = await _dbContext.SunriseSunsets.FindAsync(sunriseSunset.Id);
        
        if (sunriseSunsetInDb == null)
        {
            throw new Exception($"City with ID {sunriseSunset.Id} not found.");
        }

        sunriseSunsetInDb.Date = sunriseSunset.Date;
        sunriseSunsetInDb.TimeZone = sunriseSunset.TimeZone;
        sunriseSunsetInDb.Sunrise = sunriseSunset.Sunrise;
        sunriseSunsetInDb.Sunset = sunriseSunset.Sunset;

        _dbContext.Update(sunriseSunsetInDb);
        await _dbContext.SaveChangesAsync();
    }
    
    private static DateTime DateParser(string? date)
    {
        if (string.IsNullOrEmpty(date))
        {
            return DateTime.Today;
        }
        else
        {
            if(!DateTime.TryParse(date, out var dateTime))
            {
                throw new FormatException("Date format is not correct.");
            }
            return dateTime;
        }
    }
}