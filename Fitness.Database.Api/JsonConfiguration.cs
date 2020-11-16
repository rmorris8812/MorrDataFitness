// ***************************************************************
// Copyright 2020 MorrData LLC. All rights reserved.
// ***************************************************************

using Microsoft.Extensions.Configuration;
using System;

namespace Fitness.Database.Api
{
    public class JsonConfiguration : IConfigration
    {
        private readonly IConfigurationBuilder _builder;
        private readonly IConfigurationRoot _configuration;

        public JsonConfiguration()
        {
            _builder = new ConfigurationBuilder().AddJsonFile(@"app.settings.json", true, true);
            if (_builder == null)
                throw new NullReferenceException("Configuration builder can't be null");
            _configuration = _builder.Build();
            if (_configuration == null)
                throw new NullReferenceException("Connection configuration not found");
        }
        public string GetValue(string key)
        {
            return _configuration[key];
        }
    }
}
