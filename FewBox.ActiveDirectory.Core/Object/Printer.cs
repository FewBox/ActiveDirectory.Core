using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class Printer : ActiveDirectoryObject
    {
        internal Printer(DirectoryEntry directoryEntry) : base(directoryEntry)
        {
        }
    }
}
