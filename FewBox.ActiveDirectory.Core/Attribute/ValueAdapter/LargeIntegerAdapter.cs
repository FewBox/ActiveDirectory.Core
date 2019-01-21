using System;
using System.DirectoryServices;
using System.Reflection;

namespace FewBox.ActiveDirectory.Core.Attribute.ValueAdapter
{
    class LargeIntegerAdapter : Adapter<DateTime>
    {
        public LargeIntegerAdapter(DirectoryEntry directoryEntry, string attributeName) :
            base(directoryEntry, attributeName)
        {
        }

        protected override DateTime GetAttributeValue()
        {
            if (this.ValueObject == null)
            {
                return DateTime.MinValue;
            }
            else
            {
                return LongFromLargeInteger(this.ValueObject);
            }
        }

        private DateTime LongFromLargeInteger(object largeInteger)
        {
            Type type = largeInteger.GetType();
            int highPart = (int)type.InvokeMember("HighPart", BindingFlags.GetProperty, null, largeInteger, null);
            int lowPart = (int)type.InvokeMember("LowPart", BindingFlags.GetProperty, null, largeInteger, null);
            long fileTime = (long)(((long)highPart << 32) + (uint)lowPart);
            DateTime datetime;
            if (fileTime == 9223372036854775807)
            {
                datetime = DateTime.MaxValue;
            }
            else
            {
                datetime = DateTime.FromFileTimeUtc(fileTime);
            }
            return datetime;
        }
    }
}
