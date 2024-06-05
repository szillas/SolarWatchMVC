using SolarApp.Models;

namespace SolarApp.Services.Repository;

public interface ISunriseSunsetRepository
{
    Task<IEnumerable<SunriseSunset>> GetAll();
    Task<SunriseSunset?> GetById(int id);
    Task<SunriseSunset?> GetByDateAndCity(string city, DateTime date);
    Task<SunriseSunset?> GetByDateAndCity(string city, string? date);

    Task Add(SunriseSunset sunriseSunset);
    Task Delete(SunriseSunset sunriseSunset);
    Task Update(SunriseSunset sunriseSunset);
}