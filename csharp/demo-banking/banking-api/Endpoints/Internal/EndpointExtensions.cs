namespace banking_api.Endpoints.Internal
{
    public static class EndpointExtensions
    {
        public static void AddEndpoints(this IServiceCollection services, Type typeMarker, IConfiguration configuration)
        {
            var endpointTypes = GetEndpointTypesFromAssemblyConmtaining(typeMarker);

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpoints.AddServices))!.Invoke(null, new object[] { services, configuration });
            }
        }
              
        public static void AddEndpoints<TMarker>(this IServiceCollection services, IConfiguration configuration)
        {
            AddEndpoints(services, typeof(TMarker), configuration);
        }

        public static void UseEndpoints(this IApplicationBuilder app, Type typeMarker)
        {
            var endpointTypes = GetEndpointTypesFromAssemblyConmtaining(typeMarker);

            foreach (var endpointType in endpointTypes)
            {
                endpointType.GetMethod(nameof(IEndpoints.DefineEndpoints))!.Invoke(null, new object[] { app });
            }
        }

        public static void UseEndpoints<TMarker>(this IApplicationBuilder app)
        {
            UseEndpoints(app, typeof(TMarker));
        }

        private static IEnumerable<Type> GetEndpointTypesFromAssemblyConmtaining(Type typeMarker)
        {
            return typeMarker.Assembly.GetTypes()
                            .Where(t => typeof(IEndpoints).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);
        }
    }
}
