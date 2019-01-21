using FewBox.ActiveDirectory.Core.Attribute;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// Is user filter (Eg: (objectClass=user)).
    /// </summary>
    public class IsUser : IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: (objectClass=user)).
        /// </summary>
        /// <returns>The filter string.</returns>
        public string BuildFilter()
        {
            IFilter filter = new And(new Is(AttributeNames.ObjectClass, UserAttributeValues.User),
                new IsNot(AttributeNames.ObjectClass, ComputerAttributeValues.Computer),
                new IsNot(AttributeNames.ObjectClass, InetOrgPersonAttributeValues.InetOrgPerson));
            return filter.BuildFilter();
        }
    }
}
