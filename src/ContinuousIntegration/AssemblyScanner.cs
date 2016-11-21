namespace ContinuousIntegration
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyModel;

    using Api;

    public class AssemblyScanner
    {
        #region Fields

        private readonly IServiceCollection services;
        private readonly ILogger logger;

        #endregion Fields

        #region Constructor

        public AssemblyScanner(IServiceCollection services, ILogger<AssemblyScanner> logger)
        {
            this.services = services;
            this.logger = logger;
        }

        #endregion Constructor

        #region Methods

        public void Configure()
        {
            this.RegisterHandlers();
        }

        #endregion Methods

        #region Private Methods

        private void RegisterHandlers()
        {
            var libraries = this.LoadReferencingLibraries();

            foreach (var assembly in libraries)
            {
                TypeInfo[] types = null;

                try
                {
                    var asm = Assembly.Load(new AssemblyName(assembly.FullName));
                    types = asm.DefinedTypes.ToArray();
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex.Message, ex);
                }

                if (types == null)
                {
                    continue;
                }

                foreach (var type in types.Where(x => x.ImplementedInterfaces.Any(y => y.GenericTypeArguments.Any())))
                {
                    var isTestInterface = type.AsType().IsGenericTypeOf(typeof(ITestInterface));

                    if (isTestInterface)
                    {
                        var commandHandlerInterfaces = type.ImplementedInterfaces.Where(x => x.IsGenericTypeOf(typeof(ITestInterface)));

                        foreach (var commandHandlerInterface in commandHandlerInterfaces)
                        {
                            this.services.AddTransient(commandHandlerInterface, type.AsType());
                        }
                    }
                }
            }
        }

        private Assembly[] LoadReferencingLibraries()
        {
            var assemblies = this.GetReferencingLibraries("ContinuousIntegration.Api");

            return assemblies.ToArray();
        }

        private IEnumerable<Assembly> GetReferencingLibraries(string assemblyName)
        {
            var assemblies = new List<Assembly>();

            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                if (IsCandidateLibrary(library, assemblyName))
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        private bool IsCandidateLibrary(RuntimeLibrary library, string assemblyName)
        {
            return library.Name == (assemblyName) || library.Dependencies.Any(d => d.Name.StartsWith(assemblyName));
        }

        #endregion Private Methods
    }
}
