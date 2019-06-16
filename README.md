# HashicorpVaultConfiguratonExtensions
This repository contains a provider for Microsoft.Extensions.Configuration that retrieves secrets stored in a Hashicorp Vault instance.  

## Examples

### With ASP.NET Core

```csharp
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(c => {
            // Choose your auth method
            IAuthMethodInfo authMethod = new TokenAuthMethodInfo(".....");
            // Initialize settings. You can also set proxies, custom delegates etc. here.
            var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", authMethod);
            c.AddHashicorpVault(o => {
                // Pass in your client settings used when connecting to Vault
                o.ClientSettings = vaultClientSettings;
                // Add the secrets you need to retreive
                o.KeyValueSecrets.Add(new KeyValueSecret("creds"));
                o.CubbyholeSecrets.Add(new CubbyholeSecret("creds"));
                o.DatabaseSecrets.Add(new DatabaseSecret("test"));
            });
        })
        .UseStartup<Startup>();
```

### Non-ASP Project types

```csharp
static void Main(string[] args)
{
    IAuthMethodInfo authMethod = new TokenAuthMethodInfo(".....");
    // Initialize settings. You can also set proxies, custom delegates etc. here.
    var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", authMethod);
    
    var builder = new ConfigurationBuilder();
    builder.AddHashicorpVault(o => {
        // Pass in your client settings used when connecting to Vault
        o.ClientSettings = vaultClientSettings;
        // Add the secrets you need to retreive
        o.KeyValueSecrets.Add(new KeyValueSecret("creds"));
        o.CubbyholeSecrets.Add(new CubbyholeSecret("creds"));
        o.DatabaseSecrets.Add(new DatabaseSecret("test"));
    });

    var configuration = builder.Build();
    Console.WriteLine("Waddup World!");
}
```

This project uses [VaultSharp](https://github.com/rajanadar/VaultSharp), a Vault client using C#/.NET
