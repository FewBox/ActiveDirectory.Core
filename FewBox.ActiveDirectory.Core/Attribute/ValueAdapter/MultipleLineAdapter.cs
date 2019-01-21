using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class MultipleLineAdapter : Adapter<IList<string>>
    {
        public MultipleLineAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override IList<string> GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return null;
            }
            IList<string> mulitpleLineValues = new List<string>();
            if (this.ValueObject is String)
            {
                mulitpleLineValues.Add(this.ValueObject.ToString());
            }
            else
            {
                foreach (object value in (object[])this.ValueObject)
                {
                    mulitpleLineValues.Add(value.ToString());
                }
            }
            return mulitpleLineValues;
        }
    }
}
