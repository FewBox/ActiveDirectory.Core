using System;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class DateTimeAdapter : Adapter<DateTime>
    {
        public DateTimeAdapter(DirectoryEntry directoryEntry, string attributeName) : 
            base(directoryEntry, attributeName)
        {
        }

        protected override DateTime GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return DateTime.MinValue;
            }
            if (this.ValueObject.ToString() == "System.__ComObject")
            {
                var dateTime = ((Int64[])this.ValueObject)[0];
                if (!dateTime.Equals(Int64.MaxValue))
                {
                    return DateTime.FromFileTimeUtc(dateTime);
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
            else
            {
                return (DateTime)this.ValueObject;
            }
        }
    }
}
