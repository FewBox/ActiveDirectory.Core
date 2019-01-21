using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Security.Principal;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class SidsAdapter : Adapter<IList<string>>
    {
        public SidsAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override IList<string> GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return null;
            }
            IList<string> sidValues = new List<string>();
            foreach (object value in (object[])this.ValueObject)
            {
                sidValues.Add(new SecurityIdentifier(value as byte[], 0).ToString());
            }
            return sidValues;
        }
    }
}
