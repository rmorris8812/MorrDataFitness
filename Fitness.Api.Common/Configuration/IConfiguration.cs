// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection;

namespace Fitness.Api.Configuration
{
    public interface IConfiguration
    {
        /// <summary>
        /// Get a string value from the config.
        /// </summary>
        /// <param name="key">The value's key in the config.</param>
        /// <returns></returns>
        string GetValue(string key);
        /// <summary>
        /// Set the value for a key
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        void SetValue(string key, string value);
        /// <summary>
        /// Get a child section as nested arrays from the config.
        /// </summary>
        /// <param name="key">The root section's key, after that nested array keys must match the class properties</param>
        /// <returns>The child array.</returns>
        List<T> GetArrayValue<T>(string key);
        /// <summary>
        /// Get a class object from the configuration.
        /// </summary>
        /// <typeparam name="T">The class type</typeparam>
        /// <param name="key">The section key</param>
        /// <returns></returns>
        T GetClassValues<T>(string key);
        /// <summary>
        /// Get a child section from the config.
        /// </summary>
        /// <param name="key">The section's key</param>
        /// <returns>The child section for the key.</returns>
        IConfigurationSection GetSection(string key);
    }
}
