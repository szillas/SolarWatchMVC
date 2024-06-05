using System.Text.Json;

namespace SolarApp.Services.JsonProcessor;

public interface IJsonProcessorHelper
{
    string GetStringProperty(JsonElement element, string propertyName);
    double GetDoubleProperty(JsonElement element, string propertyName);
}