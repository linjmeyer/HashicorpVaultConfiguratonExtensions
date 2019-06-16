using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public class VaultConfigurationSource : IConfigurationSource
    {
        private VaultConfigurationOptions _config;

        public VaultConfigurationSource(Action<VaultConfigurationOptions> config)
        {
            _config = new VaultConfigurationOptions();
            config.Invoke(_config);
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(_config);
        }
    }
}