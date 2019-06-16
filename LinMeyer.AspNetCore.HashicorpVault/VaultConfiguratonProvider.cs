using Microsoft.Extensions.Configuration;
using VaultSharp;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LinMeyer.AspNetCore.HashicorpVault
{
    public class VaultConfigurationProvider : ConfigurationProvider
    {
        private VaultClientSettings _clientSettings;
        private IVaultClient _client;
        private IEnumerable<string> _keyValueSecrets;

        public VaultConfigurationProvider(VaultClientSettings clientSettings, IEnumerable<string> keyValueSecrets)
        {
            _clientSettings = clientSettings ?? throw new ArgumentNullException(nameof(clientSettings));
            _client = new VaultClient(_clientSettings);
            _keyValueSecrets = keyValueSecrets;
        }

        public override void Load() 
        {
            LoadAsync().Wait();
        }

        private async Task LoadAsync() 
        {
            await GetAllSecretsAsync();
        }

        private async Task GetAllSecretsAsync()
        {
            foreach(var secretName in _keyValueSecrets)
            {
                var secret = await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(secretName);
                foreach(var secretValue in secret.Data.Data)
                {
                    // Add to IConfigurationProvier data as SecretName:SecretValueName=Value
                    // E.g. something like loginsecret:username=asdfasdf, loginsecret:password=asdfasdf
                    Data.Add($"{secretName}:{secretValue.Key}", secretValue.Value.ToString());
                }
            }
        }
    }
}
