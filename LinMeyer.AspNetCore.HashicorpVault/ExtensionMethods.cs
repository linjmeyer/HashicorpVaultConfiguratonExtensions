using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public static class ExtensionMethods 
    {
        public static IConfigurationBuilder AddHashicorpVault(this IConfigurationBuilder builder, Action<VaultConfigurationOptions> options)
        {
            var source = new VaultConfigurationSource(options);
            builder.Add(source);

            return builder;
        }
    }
}