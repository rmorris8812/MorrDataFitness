// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

namespace Fitness.Api.Configuration
{
    public class ConfigurationFactory
    {
        /// <summary>
        /// Get the configuration provider.
        /// </summary>
        /// <returns>A configuration provider that allows you to read/write json objects</returns>
        public static IConfiguration GetConfiguration()
        {
            return new JsonConfiguration("appsettings.json");
        }
        /// <summary>
        /// Get the configuration provider.
        /// </summary>
        /// <param name="fileName">The name of the json file that contains the app configuration</param>
        /// <returns>A configuration provider that allows you to read/write json objects</returns>
        public static IConfiguration GetConfiguration(string fileName)
        {
            return new JsonConfiguration(fileName);
        }
    }
}
