using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class SharedFolder : ActiveDirectoryObject
    {
        internal SharedFolder(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }
    }
}
