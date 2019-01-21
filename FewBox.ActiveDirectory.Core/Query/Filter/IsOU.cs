using FewBox.ActiveDirectory.Core.Attribute;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// Is OU filter (Eg: (objectClass=organizationalUnit)).
    /// </summary>
    public class IsOrganizationalUnit : IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: (objectClass=organizationalUnit)).
        /// </summary>
        /// <returns>The filter string.</returns>
        public string BuildFilter()
        {
            IFilter filter = new Is(AttributeNames.ObjectClass, OrganizationalUnitAttributeValues.OrganizationalUnit);
            return filter.BuildFilter();
        }
    }
}
