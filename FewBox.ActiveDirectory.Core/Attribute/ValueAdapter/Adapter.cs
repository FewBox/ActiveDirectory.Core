using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    abstract class Adapter<T>
    {
        protected object ValueObject { get; private set; }
        public T Value { get { return this.GetAttributeValue(); } }

        protected Adapter(DirectoryEntry directoryEntry, string attributeName)
        {
            directoryEntry.RefreshCache(new string[] { attributeName });
            if (directoryEntry.Properties[attributeName].Count == 0)
            {
                // N/A.
            }
            else
            {
                this.ValueObject = directoryEntry.Properties[attributeName].Value;
            }
        }

        protected abstract T GetAttributeValue();
    }
}
