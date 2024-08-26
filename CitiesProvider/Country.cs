namespace CountriesProvider;

using System.Text.Json.Serialization;

internal class Country
{
    [JsonPropertyName( "name" )]
    public CountryName Name { get; set; }

    [JsonPropertyName( "independent" )]
    public bool Independent { get; set; }

    [JsonPropertyName( "capital" )]
    public string[] Capital { get; set; }

    [JsonPropertyName( "latlng" )]
    public double[] Latlng { get; set; }

    [JsonPropertyName( "borders" )]
    public string[] Borders { get; set; }

    [JsonPropertyName( "area" )]
    public double Area { get; set; }

    [JsonPropertyName( "maps" )]
    public Maps Maps { get; set; }

    [JsonPropertyName( "population" )]
    public int Population { get; set; }

    [JsonPropertyName( "timezones" )]
    public string[] Timezones { get; set; }

    [JsonPropertyName( "startOfWeek" )]
    public string StartOfWeek { get; set; }
}
