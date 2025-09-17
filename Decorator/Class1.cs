using System;
using System.Collections.Generic;
using System.Text;

namespace Estudos.Decorator
{
    internal class Class1
    {
        /*
         
         builder.Services.AddKeyedSingleton<IWeatherService, OpenWeatherMapService>("og");
builder.Services.AddSingleton<IWeatherService, ResilientWeatherService>();
         */

        /*
         
         public static IServiceCollection AddDecoratedSingleton<TService, TDec, TFinal>(
    this IServiceCollection services, object key)
    where TService : class
    where TFinal : class, TService
    where TDec : class, TService
{
    services.AddKeyedSingleton<TService, TFinal>(key);
    services.AddSingleton<TService, TDec>();
    return services;
}
         
         */
    }
}
