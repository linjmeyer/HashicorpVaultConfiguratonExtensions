using Microsoft.Extensions.Configuration;
using VaultSharp;
using System;
using System.Threading.Tasks;
using VaultSharp.V1.Commons;

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
            foreach(var retreivableSecret in _config.Secrets)
            {
                var name = retreivableSecret.Name;
                var secret = await GetSecret(retreivableSecret);
                foreach(var secretValue in secret.Data.Data)
                {
                    // Add to IConfigurationProvier data as SecretName:SecretValueName=Value
                    // E.g. something like loginsecret:username=asdfasdf, loginsecret:password=asdfasdf
                    Data.Add($"{name}:{secretValue.Key}", secretValue.Value.ToString());
                }
            }
        }

        private async Task<Secret<SecretData>> GetSecret(RetreivableSecret secret)
        {
            switch (secret.Type)
            {
                case SecretType.KeyValue:
                    return await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(secret.Name);
                default:
                    throw new InvalidOperationException($"Cannot load secret of type: {secret.Type}");
            }
        }
    }
}
