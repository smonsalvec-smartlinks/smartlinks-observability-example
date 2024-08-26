namespace APITest.Controllers;

using DataTrasnferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Observability.Application.Commands;
using Observability.Application.Queries;

[Route( "[controller]" )]
[ApiController]
public class PlaceHolderController : ControllerBase
{
    private readonly IPlaceHolderQueryService placeHolderQueryService;
    private readonly IPlaceHolderCommandService placeHolderCommandService;

    public PlaceHolderController(   IPlaceHolderQueryService placeHolderQueryService, 
                                    IPlaceHolderCommandService placeHolderCommandService )
    {
        this.placeHolderQueryService = placeHolderQueryService;
        this.placeHolderCommandService = placeHolderCommandService;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> Get()
    {
        try
        {
            IEnumerable<PlaceHolder> placeHolders = await this.placeHolderQueryService.GetAllAsync();
            return this.Ok( placeHolders );
        }
        catch( Exception ex )
        {
            return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
        }
    }

    [HttpPost( "posts" )]
    public async Task<IActionResult> Post( PlaceHolder placeHolder )
    {
        try
        {
            PlaceHolder createdPlaceHolder = await this.placeHolderCommandService.CreateAsync( placeHolder );
            return this.Ok( createdPlaceHolder );
        }
        catch( Exception ex )
        {
            return this.StatusCode( StatusCodes.Status500InternalServerError, ex.Message );
        }
    }
}
