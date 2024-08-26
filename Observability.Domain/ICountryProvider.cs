namespace Observability.Domain;

public interface ICountryProvider
{
    Task<IEnumerable<Country>> GetCountryByNameAsync( string name );
}
