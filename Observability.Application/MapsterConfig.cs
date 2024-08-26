namespace Observability.Application;

using Mapster;
using Observability.Domain;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Country, DataTrasnferObjects.Country>
            .NewConfig()
            .Map( dest => dest.Name, src => src.Name )
            .Map( dest => dest.Capital, src => src.Capital )
            .Map( dest => dest.Population, src => src.Population )
            .Map( dest => dest.Area, src => src.Area )
            .Map( dest => dest.Independent, src => src.Independent )
            .Map( dest => dest.Latlng, src => src.Latlng )
            .Map( dest => dest.StartOfWeek, src => src.StartOfWeek );

        TypeAdapterConfig<PlaceHolder, DataTrasnferObjects.PlaceHolder>
            .NewConfig()
            .Map( dest => dest.Id, src => src.Id )
            .Map( dest => dest.UserId, src => src.UserId )
            .Map( dest => dest.Title, src => src.Title )
            .Map( dest => dest.Body, src => src.Body );
    }
}
