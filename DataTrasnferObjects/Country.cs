namespace DataTrasnferObjects;

using System.Text.Json.Serialization;

public class Country
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("independent")]
    public bool Independent { get; set; }

    [JsonPropertyName("capital")]
    public string Capital { get; set; }

    [JsonPropertyName("latlng")]
    public double[] Latlng { get; set; }

    [JsonPropertyName("area")]
    public double Area { get; set; }

    [JsonPropertyName("population")]
    public int Population { get; set; }

    [JsonPropertyName("startOfWeek")]
    public string StartOfWeek { get; set; }
}
