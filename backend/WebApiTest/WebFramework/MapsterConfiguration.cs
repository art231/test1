using System.Reflection;
using Mapster;
using MobileStatisticsApp.Dtos;

namespace MobileStatisticsApp.WebFramework;

public static class MapsterConfiguration
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        Assembly applicationAssembly = typeof(BaseDto<,>).Assembly;
        typeAdapterConfig.Scan(applicationAssembly);
    }
}
