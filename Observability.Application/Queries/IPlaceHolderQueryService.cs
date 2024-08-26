namespace Observability.Application.Queries;

using DataTrasnferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlaceHolderQueryService
{
    Task<IEnumerable<PlaceHolder>> GetAllAsync();
}
