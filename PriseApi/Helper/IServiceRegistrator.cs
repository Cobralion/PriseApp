using Microsoft.Extensions.DependencyInjection;

namespace PriseApi.Helper;

public interface IServiceRegistrator
{
    void RegisterServices(IServiceCollection serviceCollection);
}
