using System;
using System.Collections.Generic;
using VaultSharp;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public class VaultConfigurationOptions
    {
        public VaultClientSettings ClientSettings { get; set; }
        public IEnumerable<RetreivableSecret> Secrets { get; set; } = new List<RetreivableSecret>();

    }
    public class RetreivableSecret 
    {
        public string Name { get; set;}
        public SecretType Type { get; set; }
        public SecretVersion Version { get; set; }

        public RetreivableSecret(string name, SecretType type = SecretType.KeyValue, SecretVersion version = SecretVersion.V2) 
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
        }
    }

    public enum SecretType
    {
        KeyValue
    }

    public enum SecretVersion
    {
        V1,
        V2
    }
}