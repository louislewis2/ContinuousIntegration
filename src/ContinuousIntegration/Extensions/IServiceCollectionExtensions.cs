namespace Microsoft.Extensions.DependencyInjection
{
    using Extensions;

    public static class IServiceCollectionExtensions
    {
        public static void AddAssemblyScanner(this IServiceCollection serviceCollection)
        {
            var provider = serviceCollection.BuildServiceProvider();

            var runtime = ActivatorUtilities.CreateInstance<ContinuousIntegration.AssemblyScanner>(provider, serviceCollection);

            runtime.Configure();
            serviceCollection.TryAdd(ServiceDescriptor.Singleton(runtime));
        }
    }
}
