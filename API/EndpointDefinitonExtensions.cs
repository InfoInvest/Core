using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class EndpointDefinitonExtensions
    {
        public static void AddEndPointDefinitions(this IServiceCollection services, params Type[] types)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();

            foreach (var type in types)
            {
                endpointDefinitions.AddRange(GetEndPoints(type).Select(Activator.CreateInstance).Cast<IEndpointDefinition>());
            }

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions);
        }

        private static IEnumerable<Type> GetEndPoints(Type type)
        {
            return type.Assembly.ExportedTypes.Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface);
        }


        public static void UseEndpointDefinitions(this IApplicationBuilder app)
        {
            var definitions = app.ApplicationServices.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

            foreach (var endpointDefinition in definitions)
            {
                endpointDefinition.DefineEndpoints(app);
            }
        }
    }
}