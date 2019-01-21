using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class MsImaging_PSPs : ActiveDirectoryObject
    {
        internal MsImaging_PSPs(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }
    }
}
