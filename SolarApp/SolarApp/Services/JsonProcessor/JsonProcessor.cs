﻿using System.Text.Json;
using SolarApp.Models;

namespace SolarApp.Services.JsonProcessor;

public class JsonProcessor : IJsonProcessor
{
    private readonly IJsonProcessorHelper _jsonProcessorHelper;

    public JsonProcessor(IJsonProcessorHelper jsonProcessorHelper)
    {
        _jsonProcessorHelper = jsonProcessorHelper;
    }
    public Task<City> ProcessWeatherApiCityStringToCity(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        if (json.RootElement.ValueKind == JsonValueKind.Array && json.RootElement.GetArrayLength() > 0)
        {
            JsonElement cityString = json.RootElement[0];

            string name = _jsonProcessorHelper.GetStringProperty(cityString, "name");
            double lat = _jsonProcessorHelper.GetDoubleProperty(cityString, "lat");
            double lon = _jsonProcessorHelper.GetDoubleProperty(cityString, "lon");
            string country = _jsonProcessorHelper.GetStringProperty(cityString, "country");
            
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
    
    public SunriseSunset ProcessSunriseSunsetApiStringToSunriseSunset(City city, DateTime date, string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        
        if (json.RootElement.ValueKind == JsonValueKind.Object)
        {
            JsonElement result = json.RootElement.GetProperty("results");

            string sunriseTo24HoursFrom12 = _jsonProcessorHelper
                .ConvertAmPmTimeTo24Hours(_jsonProcessorHelper.GetStringProperty(result, "sunrise"));
            string sunsetTo24HoursFrom12 = _jsonProcessorHelper
                .ConvertAmPmTimeTo24Hours(_jsonProcessorHelper.GetStringProperty(result, "sunset"));
            
            string? timeZone = json.RootElement.TryGetProperty("tzid", out JsonElement timeZoneJson)
                ? timeZoneJson.GetString()
                : null;

            return new SunriseSunset
            {
                Date = date,
                City = city,
                Sunrise = sunriseTo24HoursFrom12,
                Sunset = sunsetTo24HoursFrom12,
                TimeZone = timeZone
            };
        }
        throw new JsonException("Could not get sunrise/sunset information from API.");
    }

    public string? ProcessTimeApiResultStringForTimeZone(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        
        if (json.RootElement.ValueKind == JsonValueKind.Object)
        {
            string? timeZone = json.RootElement.TryGetProperty("timeZone", out JsonElement timeZoneJson)
                ? timeZoneJson.GetString()
                : null;

            return timeZone;
        }
        throw new JsonException("Could not get time zone from API.");
    }
}