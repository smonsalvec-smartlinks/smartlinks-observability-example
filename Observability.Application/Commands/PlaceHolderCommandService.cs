namespace Observability.Application.Commands;

using MapsterMapper;
using Observability.Domain;
using System.Threading.Tasks;
using Dtos = DataTrasnferObjects;

public class PlaceHolderCommandService : IPlaceHolderCommandService
{
    private readonly IMapper mapper;
    private readonly IPlaceHolderProvider placeHolderProvider;

    public PlaceHolderCommandService( IMapper mapper, IPlaceHolderProvider placeHolderProvider )
    {
        this.mapper = mapper;
        this.placeHolderProvider = placeHolderProvider;
    }

    public async Task<Dtos.PlaceHolder> CreateAsync( Dtos.PlaceHolder placeHolder )
    {
        var domainPlaceHolder = this.mapper.Map<Observability.Domain.PlaceHolder>( placeHolder );
        var newPlaceholder = await this.placeHolderProvider.CreateAsync( domainPlaceHolder );
        return this.mapper.Map<Dtos.PlaceHolder>( newPlaceholder );
    }
}
