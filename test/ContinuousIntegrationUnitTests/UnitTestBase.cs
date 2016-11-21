namespace ContinuousIntegrationUnitTests
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using ContinuousIntegration;

    public class UnitTestBase
    {
        #region Constructor

        public UnitTestBase()
        {
            var assembly = typeof(UnitTestBase).GetTypeInfo().Assembly;
            string assemblyFolder = Path.GetDirectoryName(assembly.Location);

            HostingEnvironment env = new HostingEnvironment();
            env.ContentRootPath = assemblyFolder;
            env.EnvironmentName = EnvironmentName.Development;

            var builder = new ConfigurationBuilder()
                .AddEmbeddedJsonFile(assembly, "config.json");

            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddLogging();
            services.AddOptions();

            services.Configure<Settings>(Configuration.GetSection("Settings"));

            this.serviceProvider = services.BuildServiceProvider();

            var loggerFactory = this.serviceProvider.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddDebug(LogLevel.Debug);
        }

        #endregion Constructor

        #region Properties

        public IConfiguration Configuration { get; private set; }
        public IServiceProvider serviceProvider { get; private set; }

        #endregion Properties

        #region Methods

        public TSource GetService<TSource>()
        {
            return this.serviceProvider.GetRequiredService<TSource>();
        }

        #endregion Methods
    }
}
