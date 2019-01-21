using FewBox.ActiveDirectory.Core.Exception;
using System;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Authentication
{
    public class ClientContext: IDisposable
    {
        internal DirectoryEntry RootDirectoryEntry { get; private set; }
        internal string Path { get; private set; }
        internal string Username { get; private set; }
        internal string Password { get; private set; }
        private static ClientContext Instance { get; set; }
        private static object lockObject = new object();

        public ClientContext(string path, string username, string password)
        {
            this.Path = path;
            this.Username = username;
            this.Password = password;
            this.RootDirectoryEntry = new DirectoryEntry(path, username, password);
            Guid objectId = this.RootDirectoryEntry.Guid;
        }

        public static ClientContext GetContext() {
            if (Instance == null)
            {
                throw new ClientContextDoesNotInitialException();
            }
            return Instance;
        }

        public static void Init(string path, string username, string password)
        {
            if (Instance == null)
            {
                lock (lockObject)
                {
                    Instance = new ClientContext(path, username, password);
                }
            }
        }

        public static void Clear()
        {
            Instance = null;
        }

        public void Dispose()
        {
            this.RootDirectoryEntry.Dispose();
        }
    }
}