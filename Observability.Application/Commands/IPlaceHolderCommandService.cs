namespace Observability.Application.Commands;

using Dtos = DataTrasnferObjects;
using System.Threading.Tasks;

public interface IPlaceHolderCommandService
{
    Task<Dtos.PlaceHolder> CreateAsync( Dtos.PlaceHolder placeHolder );
}
