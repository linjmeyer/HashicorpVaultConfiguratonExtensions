using System.Collections.Generic;
using VaultSharp;

namespace LinMeyer.AspNetCore.HashicorpVault 
{
    public class VaultConfigurationOptions
    {
        public VaultClientSettings ClientSettings { get; set; }
        public List<KeyValueSecret> KeyValueSecrets { get; set; } = new List<KeyValueSecret>();
        public List<DatabaseSecret> DatabaseSecrets { get; set; } = new List<DatabaseSecret>();
        public List<CubbyholeSecret> CubbyholeSecrets { get; set; } = new List<CubbyholeSecret>();
        public List<SshSecret> SshSecrets { get; set; } = new List<SshSecret>();
    }

}