using System;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class IntegerAdapter : Adapter<Int32>
    {
        public IntegerAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override int GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return -1;
            }
            else
            {
                return Int32.Parse(this.ValueObject.ToString());
            }
        }
    }
}
