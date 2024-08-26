namespace Observability.Application.Queries;

using MapsterMapper;
using Observability.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dtos = DataTrasnferObjects;

public class PlaceHolderQueryService : IPlaceHolderQueryService
{
    private readonly IMapper mapper;
    private readonly IPlaceHolderProvider placeHolderProvider;

    public PlaceHolderQueryService( IMapper mapper, IPlaceHolderProvider placeHolderProvider )
    {
        this.mapper = mapper;
        this.placeHolderProvider = placeHolderProvider;
    }

    public async Task<IEnumerable<Dtos.PlaceHolder>> GetAllAsync()
    {
        IEnumerable<PlaceHolder> placeHolders = await this.placeHolderProvider.GetAllAsync();
        return this.mapper.Map<IEnumerable<Dtos.PlaceHolder>>( placeHolders );
    }
}
