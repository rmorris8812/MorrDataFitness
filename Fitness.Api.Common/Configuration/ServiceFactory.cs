// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

using System.Globalization;

namespace Fitness.Api.Configuration
{
    public static class ServiceFactory
    {
        public static T GetService<T>()
        {
            var service = IocComponents.GetService<T>();
            if (service == null)
                throw new System.Data.NoNullAllowedException(string.Format(CultureInfo.InvariantCulture,
                    "The service could not be created, did you forget to setup the appsettings?"));
            return service;
        }
    }
}
