using System;

namespace LinMeyer.AspNetCore.HashicorpVault
{
    public class BaseSecret
    {
        public string MountPoint { get; set; }

        public BaseSecret(string mountPoint = null)
        {
            if(string.IsNullOrWhiteSpace(mountPoint))
            {
                throw new ArgumentNullException(nameof(mountPoint));
            }
            MountPoint = mountPoint;
        }

    }

    ////////////////////////////////////////////////////
    // KeyValueSecret
    ////////////////////////////////////////////////////
    public class KeyValueSecret : BaseSecret
    {
        public string Name { get; set; }
        public KeyValueVersion Version { get; set; }
        public int? V2Version { get; set; }

        public KeyValueSecret(string name, KeyValueVersion version = KeyValueVersion.V2, int? v2Version = null, string mountPoint = "kv") : base(mountPoint)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Name = name;
            Version = version;
            V2Version = v2Version;
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
    public class DatabaseSecret : BaseSecret
    {
        public string Name { get; set; }
        public DatabaseSecret(string name, string mountPoint = "database") : base(mountPoint)
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

    ////////////////////////////////////////////////////
    // SSH
    ////////////////////////////////////////////////////
    public class SshSecret : BaseSecret
    {
        public string Role { get; set; }
        public string IpAddress { get; set; }
        public string Username { get; set; }

        public SshSecret(string role, string ipAddress, string username = null, string mountPoint = "ssh") : base(mountPoint)
        {
            Role = role;
            IpAddress = ipAddress;
            Username = username;
        }
    }
}