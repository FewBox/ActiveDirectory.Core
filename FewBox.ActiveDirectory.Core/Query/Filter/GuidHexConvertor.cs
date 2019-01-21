using System;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    static class GuidHexConvertor
    {
        public static string Convert(Guid guid)
        {
            byte[] guidBytes = guid.ToByteArray();
            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(guidBytes);
            }
            return String.Format(@"\{0}", BitConverter.ToString(guidBytes).Replace(@"-", @"\"));
        }
    }
}
