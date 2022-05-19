using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace API
{
    public interface IEndpointDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineEndpoints(WebApplication app);
    }
}