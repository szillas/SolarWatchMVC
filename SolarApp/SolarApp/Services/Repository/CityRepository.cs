using SolarApp.Models;

namespace SolarApp.Services.Repository;

public class CityRepository : ICityRepository
{
    public Task<IEnumerable<City>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetByName(string city, string state)
    {
        throw new NotImplementedException();
    }

    public Task<City?> GetByNameAndCountry(string name, string country)
    {
        throw new NotImplementedException();
    }

    public Task Add(City city)
    {
        throw new NotImplementedException();
    }

    public Task Delete(City city)
    {
        throw new NotImplementedException();
    }

    public Task Update(City city)
    {
        throw new NotImplementedException();
    }
}