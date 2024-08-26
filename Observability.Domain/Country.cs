namespace Observability.Domain;

public class Country( string name, string capital, double[] latlng, double area, int population, string startOfWeek )
{
    public string Name { get; private set; } = name;
    public bool Independent { get; set; } = true;
    public string Capital { get; private set; } = capital;
    public double[] Latlng { get; private set; } = latlng;
    public double Area { get; private set; } = area;
    public int Population { get; private set; } = population;
    public string StartOfWeek { get; private set; } = startOfWeek;
}
