using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public static class ExtensionMethods 
    {
        public static IConfigurationBuilder AddHashicorpVault(this IConfigurationBuilder builder, VaultClientSettings clientSettings, IEnumerable<string> keyValues)
        {
            var source = new VaultConfigurationSource(clientSettings, keyValues);
            builder.Add(source);

            return builder;
        }
    }
}