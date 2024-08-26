namespace Observability.Application.Queries;

using MapsterMapper;
using Observability.Domain;
using System;
using System.Threading.Tasks;
using Dtos = DataTrasnferObjects;

public class CountryQueryService : ICountryQueryService
{
    private readonly IMapper mapper;
    private readonly ICountryProvider countryProvider;

    public CountryQueryService( IMapper mapper, ICountryProvider countryProvider )
    {
        this.mapper = mapper;
        this.countryProvider = countryProvider;
    }

    public async Task<IEnumerable<Dtos.Country>> GetCountryByNameAsync( string name )
    {
        IEnumerable<Country> countries = await this.countryProvider.GetCountryByNameAsync( name );
        return this.mapper.Map<IEnumerable<Dtos.Country>>( countries );
    }
}
