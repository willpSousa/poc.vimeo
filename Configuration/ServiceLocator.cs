namespace Poc.Vimeo.Configuration;

public static class ServiceLocator
{
    public static IServiceProvider ServiceProvider;
    private static object Get(Type service) =>
        ServiceProvider.GetService(service);

    public static T? Get<T>() where T : class => (T)Get(typeof(T));

}
