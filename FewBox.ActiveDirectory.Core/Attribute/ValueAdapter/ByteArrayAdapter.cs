using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class ByteArrayAdapter : Adapter<byte[]>
    {
        public ByteArrayAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override byte[] GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return null;
            }
            return this.ValueObject as byte[];
        }
    }
}
