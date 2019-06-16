using Microsoft.Extensions.Configuration;
using VaultSharp;
using System;
using System.Threading.Tasks;
using VaultSharp.V1.Commons;
using System.Collections.Generic;
using VaultSharp.V1.SecretsEngines;

namespace LinMeyer.AspNetCore.HashicorpVault
{
    public class VaultConfigurationProvider : ConfigurationProvider
    {
        private IVaultClient _client;
        private VaultConfigurationOptions _config;

        public VaultConfigurationProvider(VaultConfigurationOptions options)
        {
            _config = options;

            if(_config.ClientSettings == null) 
            {
                throw new ArgumentNullException(nameof(_config.ClientSettings));
            }

            _client = new VaultClient(_config.ClientSettings);
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
            await GetKeyValueSecretsAsync();
            await GetDatabaseSecretsAsync();
        }

        private async Task GetKeyValueSecretsAsync()
        {
            foreach(var retreivableSecret in _config.KeyValueSecrets)
            {

                Dictionary<string,object> secretData; // To store secret data which will be retreived from v1 or v2 api
                if(retreivableSecret.Version == KeyValueVersion.V1) 
                {
                    var secretV1 = await _client.V1.Secrets.KeyValue.V1.ReadSecretAsync(retreivableSecret.Name);
                    secretData = secretV1.Data;
                } else {
                    var secretV2 = await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(retreivableSecret.Name);
                    secretData = secretV2.Data.Data;
                }

                foreach(var secretValue in secretData)
                {
                    // Add to IConfigurationProvier data as SecretName:SecretValueName=Value
                    // E.g. something like loginsecret:username=asdfasdf, loginsecret:password=asdfasdf
                    Data.Add($"{retreivableSecret.Name}:{secretValue.Key}", secretValue.Value.ToString());
                }
            }
        }

        private async Task GetDatabaseSecretsAsync()
        {
            foreach(var retreivableSecret in _config.DatabaseSecrets)
            {
                var secret = await _client.V1.Secrets.Database.GetCredentialsAsync(retreivableSecret.Name);
                Data.Add($"{retreivableSecret.Name}:username", secret.Data.Username);
                Data.Add($"{retreivableSecret.Name}:password", secret.Data.Password);
            }
        }
    }
}
