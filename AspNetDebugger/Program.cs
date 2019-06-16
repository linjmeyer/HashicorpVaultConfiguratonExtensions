using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using LinMeyer.AspNetCore.HashicorpVault;
using VaultSharp.V1.AuthMethods;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace AspNetDebugger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c => {
                    // Choose your auth method
                    IAuthMethodInfo authMethod = new TokenAuthMethodInfo("s.AaFey8lIgEMoZNqq5bHldwBP");
                    // Initialize settings. You can also set proxies, custom delegates etc. here.
                    var vaultClientSettings = new VaultClientSettings("http://127.0.0.1:8200", authMethod);
                    c.AddHashicorpVault(o => {
                        o.ClientSettings = vaultClientSettings;
                        o.Secrets.Append(new RetreivableSecret("creds"));
                    });
                })
                .UseStartup<Startup>();
    }
}
