namespace Observability.Domain;

using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlaceHolderProvider
{
    Task<IEnumerable<PlaceHolder>> GetAllAsync();
    Task<PlaceHolder> CreateAsync( Observability.Domain.PlaceHolder placeHolder );
}
