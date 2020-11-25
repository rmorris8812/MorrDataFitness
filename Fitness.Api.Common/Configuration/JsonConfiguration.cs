// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fitness.Api.Configuration
{
    public class JsonConfiguration : IConfiguration
    {
        private readonly IConfigurationBuilder _builder;
        private readonly IConfigurationRoot _configuration;
        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="fileName">The name of the json configuration file.</param>
        public JsonConfiguration(string fileName)
        {
            _builder = new ConfigurationBuilder().AddJsonFile(fileName, true, true);
            if (_builder == null)
                throw new NullReferenceException("Configuration builder can't be null");
            _configuration = _builder.Build();
            if (_configuration == null)
                throw new NullReferenceException("Connection configuration not found");
        }
        /// <summary>
        /// Get a string value from the config.
        /// </summary>
        /// <param name="key">The value's key in the config.</param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            return _configuration.GetValue<string>(key);
        }
        /// <summary>
        /// Set the value for a key
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void SetValue(string key, string value)
        {
            _configuration[key] = value;
        }
        /// <summary>
        /// Get a child section from the config.
        /// </summary>
        /// <param name="key">The section's key</param>
        /// <returns>The child section for the key.</returns>
        public IConfigurationSection GetSection(string key)
        {
            return _configuration.GetSection(key);
        }
        /// <summary>
        /// Get an array value.
        /// Note 1: This can and will be called recursively if a child is a list.
        /// Note 2: You can call this to access a nested list: "Key:Index:PropertyName:Index:PropertyName".
        /// Note 3. Only the top level name (root + 1) can follow the JSON key. 
        ///         Otherwise it must follow the name of the nested property.
        /// Note 4: Please see tests for examples.
        /// </summary>
        /// <typeparam name="T">The type of list</typeparam>
        /// <param name="key">The key to the nested array</param>
        /// <returns></returns>
        public List<T> GetArrayValue<T>(string key)
        {
            var result = new List<T>();
            var sections = GetSection(key);
            int index = 0;
            foreach (var section in sections.GetChildren())
            {
                var obj = Activator.CreateInstance<T>();
                var properties = obj.GetType().GetProperties();
                foreach (var property in properties)
                {
                    // If this is a list then recursive call
                    if (property.PropertyType.Namespace.Equals("System.Collections.Generic"))
                    {
                        // var nestedArray = GetArrayValue<T>(key + ":" + index + ":" + property.Name);
                        // property.SetValue(obj, nestedArray);
                    }
                    // If this is an Enum
                    else if (property.PropertyType.IsEnum)
                    {
                        property.SetValue(obj, GetEnumValue(section, property));
                    }
                    else if (property.PropertyType.IsClass && !property.PropertyType.Name.Equals("String"))
                    {
                        var childSection = GetSection(key + ":" + index + ":" + property.Name);
                        property.SetValue(obj, GetClassValues(childSection, property));
                    }
                    // Otherwise treat this as a primitive object
                    else if (property.PropertyType.IsPrimitive || property.PropertyType.Name.Equals("String"))
                    {
                        var childValue = section.GetValue(property.PropertyType, property.Name);
                        if (childValue != null)
                        {
                            property.SetValue(obj, childValue);
                        }
                    }
                }
                result.Add(obj);
                ++index;
            }
            return result;
        }
        public T GetClassValues<T>(string key)
        {
            var childValue = Activator.CreateInstance<T>();
            if (string.IsNullOrEmpty(key))
                return childValue;

            var section = GetSection(key);
            var properties = childValue.GetType().GetProperties();
            foreach (var childProperty in properties)
            {
                if (childProperty.PropertyType.IsEnum)
                {
                    childProperty.SetValue(childValue, GetEnumValue(section, childProperty));
                }
                else if (childProperty.PropertyType.IsClass && !childProperty.PropertyType.Name.Equals("String"))
                {
                    var childSection = GetSection(section.Key + ":" + childProperty.Name);
                    childProperty.SetValue(childValue, GetClassValues(childSection, childProperty));
                }
                else if (childProperty.PropertyType.IsPrimitive || childProperty.PropertyType.Name.Equals("String"))
                {
                    var value = section.GetValue(childProperty.PropertyType, childProperty.Name);
                    childProperty.SetValue(childValue, value);
                }
            }
            return childValue;
        }

        public object GetClassValues(IConfigurationSection section, PropertyInfo property)
        {
            if (section == null || property == null)
                return null;

            var childValue = Activator.CreateInstance(property.PropertyType);
            var properties = childValue.GetType().GetProperties();
            foreach (var childProperty in properties)
            {
                if (childProperty.PropertyType.IsEnum)
                {
                    childProperty.SetValue(childValue, GetEnumValue(section, childProperty));
                }
                else if (childProperty.PropertyType.IsClass && !childProperty.PropertyType.Name.Equals("String"))
                {
                    var childSection = GetSection(section.Key + ":" + childProperty.Name);
                    childProperty.SetValue(childValue, GetClassValues(childSection, childProperty));
                }
                else if (childProperty.PropertyType.IsPrimitive || childProperty.PropertyType.Name.Equals("String"))
                {
                    var value = section.GetValue(childProperty.PropertyType, childProperty.Name);
                    childProperty.SetValue(childValue, value);
                }
            }
            return childValue;
        }

        private Enum GetEnumValue(IConfigurationSection section, PropertyInfo property)
        {
            var intValue = 0;
            try
            {
                intValue = section.GetValue<int>(property.Name);
            } 
            catch (Exception)
            {
            }
            var underlyingValue = Convert.ChangeType(intValue, Enum.GetUnderlyingType(property.PropertyType)) as Enum;
            return underlyingValue;
        }
    }
}
