using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class UnknownObject : ActiveDirectoryObject
    {
        internal UnknownObject(DirectoryEntry directoryEntry) : 
            base(directoryEntry)
        {
        }
    }
}
