namespace APITest.Controllers;

using Microsoft.AspNetCore.Mvc;
using Observability.Application.Queries;
using Dtos = DataTrasnferObjects;

[ApiController]
[Route( "[controller]" )]
public class CountriesController : ControllerBase
{
    private readonly ICountryQueryService countryQueryService;

    public CountriesController( ICountryQueryService countryQueryService )
    {
        this.countryQueryService = countryQueryService;
    }

    [HttpGet( "{name}" )]
    public async Task<OkObjectResult> Get( string name )
    {
        IEnumerable<Dtos.Country> result = await this.countryQueryService.GetCountryByNameAsync( name );
        return this.Ok( result );
    }
}
