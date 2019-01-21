using FewBox.ActiveDirectory.Core.Attribute;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// Is password settings filter (Eg: (objectClass=msDs-PasswordSettings)).
    /// </summary>
    public class IsPasswordSettings : IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: (&amp;(cn=pangxiaoliang)(mail=pang*))).
        /// </summary>
        /// <returns></returns>
        public string BuildFilter()
        {
            IFilter filter = new Is(AttributeNames.ObjectClass, PasswordSettingsAttributeValues.MsDS_PasswordSettings);
            return filter.BuildFilter();
        }
    }
}
