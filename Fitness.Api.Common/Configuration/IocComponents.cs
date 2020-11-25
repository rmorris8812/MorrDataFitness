// ***************************************************************
// Copyright 2020 MOrrData LLC. All rights reserved.
// ***************************************************************
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Fitness.Api.Configuration
{
    public static class IocComponents
    {
        private static IServiceProvider _serviceProvider;
        private static bool _servicesRegistered = false;

        public static T GetService<T>()
        {
            if (_servicesRegistered != true)
                RegisterServices();

            using (var scope = _serviceProvider.CreateScope()) 
            {
                return scope.ServiceProvider.GetRequiredService<T>();
            }
        }

        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            Configuration.IocConfiguration.AddServices(services);
            _serviceProvider = services.BuildServiceProvider(true);
            _servicesRegistered = true;
        }

        public static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
