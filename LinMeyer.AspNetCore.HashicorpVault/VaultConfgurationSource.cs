using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public class VaultConfigurationSource : IConfigurationSource
    {
        private VaultClientSettings _clientSettings;
        private IEnumerable<string> _keyValues;

        public VaultConfigurationSource(VaultClientSettings clientSettings, IEnumerable<string> keyValues)
        {
            _clientSettings = clientSettings;
            _keyValues = keyValues;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new VaultConfigurationProvider(_clientSettings, _keyValues);
        }
    }
}