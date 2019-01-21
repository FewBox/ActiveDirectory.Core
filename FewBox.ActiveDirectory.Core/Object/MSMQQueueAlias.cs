using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class MSMQQueueAlias : ActiveDirectoryObject
    {
        internal MSMQQueueAlias(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }
    }
}
