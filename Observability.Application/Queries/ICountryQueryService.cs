namespace Observability.Application.Queries;

using DataTrasnferObjects;
using System.Threading.Tasks;

public interface ICountryQueryService
{
    Task<IEnumerable<Country>> GetCountryByNameAsync( string name );
}
