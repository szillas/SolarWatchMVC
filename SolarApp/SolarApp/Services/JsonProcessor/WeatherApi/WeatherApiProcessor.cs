using System.Text.Json;
using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor.WeatherApi;

public class WeatherApiProcessor
{
    public Task<City> ProcessWeatherApiCityStringToCity(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        if (json.RootElement.ValueKind == JsonValueKind.Array && json.RootElement.GetArrayLength() > 0)
        {
            JsonElement cityString = json.RootElement[0];

            string name = GetStringProperty(cityString, "name");
            double lat = GetDoubleProperty(cityString, "lat");
            double lon = GetDoubleProperty(cityString, "lon");
            string country = GetStringProperty(cityString, "country");
            
            string? state = cityString.TryGetProperty("state", out JsonElement stateElement) ? stateElement.GetString() : null;

            var city = new City
            {
                Name = name,
                Country = country,
                Latitude = lat,
                Longitude = lon,
                State = state
            };

            return Task.FromResult(city);
        }

        throw new JsonException($"Could not get coordinates. This city does not exist in the API.");
    }
}