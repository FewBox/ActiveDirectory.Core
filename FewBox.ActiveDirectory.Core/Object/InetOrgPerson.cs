using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class InetOrgPerson : ActiveDirectoryObject
    {
        internal InetOrgPerson(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }
    }
}
