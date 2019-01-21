using System;
using System.DirectoryServices;
using System.Security.Principal;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class SidAdapter : Adapter<string>
    {
        public SidAdapter(DirectoryEntry directoryEntry, string attributeName) : 
            base(directoryEntry, attributeName)
        {
        }

        protected override string GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return String.Empty;
            }
            return new SecurityIdentifier(this.ValueObject as byte[], 0).ToString();
        }
    }
}
