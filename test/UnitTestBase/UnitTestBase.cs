namespace UnitTestBase
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.Extensions.DependencyInjection;

    public class UnitTestBase
    {
        #region Constructor

        public UnitTestBase()
        {
            HostingEnvironment env = new HostingEnvironment();
            env.ContentRootPath = Directory.GetCurrentDirectory();
            env.EnvironmentName = EnvironmentName.Development;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            Configuration = builder.Build();

            var services = new ServiceCollection();
            services.AddLogging();

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
