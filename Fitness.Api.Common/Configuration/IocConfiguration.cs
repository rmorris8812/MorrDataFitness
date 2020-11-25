using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Fitness.Api.Configuration
{
    public static class IocConfiguration
    {
        private static readonly IConfiguration _config = ConfigurationFactory.GetConfiguration();

        /// <summary>
        /// Get the services that are configured in appsettings.json
        /// </summary>
        /// <returns>A list of IOC Services</returns>
        private static List<IocService> GetServices()
        {
            return _config.GetArrayValue<IocService>("IocServices");
        }
        /// <summary>
        /// Get the class type by name and assembly
        /// </summary>
        /// <param name="service">Contains the class and assembly names</param>
        /// <returns>The type, otherwise it will throw an Exception</returns>
        private static Type GetServiceType(IocService service)
        {
            var typeName = service.ClassName + "," + service.AssemblyName;
            return Type.GetType(typeName, true);
        }
        /// <summary>
        /// Add the configured IocServices to the ServiceCollection.
        /// </summary>
        /// <param name="services">The MS Extensions Dependency Injection ServiceCollection</param>
        public static void AddServices(IServiceCollection services)
        {
            foreach (var service in GetServices())
            {
                var type = IocConfiguration.GetServiceType(service);
                // See if this has an interface.  Note: Only one interface is supported.
                if (service.InterfaceName != null)
                {
                    // It has an interface so we must add the interface type and the class type
                    switch (service.LifetimeType)
                    {
                        case 0:
                            services.AddTransient(type.GetInterface(service.InterfaceName), type);
                            break;
                        case 1:
                            services.AddScoped(type.GetInterface(service.InterfaceName), type);
                            break;
                        case 2:
                            services.AddSingleton(type.GetInterface(service.InterfaceName), type);
                            break;
                    }
                }
                else
                {
                    // Otherwise no interface is defined, so we only need to add it by type.
                    switch (service.LifetimeType)
                    {
                        case 0:
                            services.AddTransient(type);
                            break;
                        case 1:
                            services.AddScoped(type);
                            break;
                        case 2:
                            services.AddSingleton(type);
                            break;
                    }
                }
            }
        }
    }

    public class IocService
    {
        public int LifetimeType { get; set; }
        public string InterfaceName { get; set; }
        public string ClassName { get; set; }
        public string AssemblyName { get; set; }
    }
}
