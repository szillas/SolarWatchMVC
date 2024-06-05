using System.Text.Json;

namespace SolarApp.Services.JsonProcessor;

public class JsonProcessorHelper : IJsonProcessorHelper
{
    public string GetStringProperty(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out JsonElement property))
        {
            return property.GetString();
        }
        throw new JsonException($"Missing required property '{propertyName}' in the city JSON data.");
    }

    public double GetDoubleProperty(JsonElement element, string propertyName)
    {
        if (element.TryGetProperty(propertyName, out JsonElement property))
        {
            return property.GetDouble();
        }
        throw new JsonException($"Missing required property '{propertyName}' in the city JSON data.");
    }
}