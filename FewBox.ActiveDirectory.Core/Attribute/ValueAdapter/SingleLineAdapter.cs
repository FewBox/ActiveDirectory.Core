using System;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class SingleLineAdapter : Adapter<string>
    {
        public SingleLineAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override string GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return String.Empty;
            }
            return this.ValueObject.ToString();
        }
    }
}
