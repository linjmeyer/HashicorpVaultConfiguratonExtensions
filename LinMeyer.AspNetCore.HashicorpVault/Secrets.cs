using System;

namespace LinMeyer.AspNetCore.HashicorpVault
{
    ////////////////////////////////////////////////////
    // KeyValueSecret
    ////////////////////////////////////////////////////
    public class KeyValueSecret 
    {
        public string Name { get; set; }
        public KeyValueVersion Version { get; set; }

        public KeyValueSecret(string name, KeyValueVersion version = KeyValueVersion.V2) 
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Version = version;
        }
    }

    public enum KeyValueVersion
    {
        V1,
        V2
    }

    ////////////////////////////////////////////////////
    // Database
    ////////////////////////////////////////////////////
    public class DatabaseSecret
    {
        public string Name { get; set; }
        public DatabaseSecret(string name)
        {
            Name = name;
        }
    }

    ////////////////////////////////////////////////////
    // Cubbyhole
    ////////////////////////////////////////////////////
    public class CubbyholeSecret
    {
        public string Path { get; set; }
        public CubbyholeSecret(string path)
        {
            Path = path;
        }
    }
}