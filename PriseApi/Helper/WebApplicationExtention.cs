using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace PriseApi.Helper;

public static class WebApplicationExtention
{
    public static void RegisterServices<T>(this IServiceCollection serviceCollection) where T : IServiceRegistrator
    {
        Type type = typeof(T);
        var instance = new[] { type }.Select(Activator.CreateInstance).Cast<IServiceRegistrator>();

        instance.First().RegisterServices(serviceCollection);
    }
}
