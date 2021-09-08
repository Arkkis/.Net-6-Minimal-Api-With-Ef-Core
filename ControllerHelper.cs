namespace MinApi;
public static class ControllerHelper
{
    public static void RegisterControllers(this WebApplication app)
    {
        var commandType = typeof(IEndpointDefinition);
        var types = Assembly.GetExecutingAssembly().GetTypes().Where(type => commandType.IsAssignableFrom(type) && !type.IsInterface).ToList();

        foreach (var type in types)
        {
            var controllerInstance = Activator.CreateInstance(type, app.Services);

            if (controllerInstance is null)
            {
                throw new NullReferenceException($"Could not create controller {type.Name}");
            }

            var controller = (IEndpointDefinition)controllerInstance;

            app.MapGet($"/{type.Name}", async () => await controller.Action());
        }
    }
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient();
    }

    public static T GetScopedService<T>(this IServiceProvider services)
    {
        var scopedFactory = services.GetRequiredService<IServiceScopeFactory>();

        var scope = scopedFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<T>();
        return service;
    }
}
