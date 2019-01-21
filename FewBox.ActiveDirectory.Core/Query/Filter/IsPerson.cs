using FewBox.ActiveDirectory.Core.Attribute;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// Is person filter (Eg: (objectClass=person)).
    /// </summary>
    public class IsPerson : IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: (objectClass=person)).
        /// </summary>
        /// <returns>The filter string.</returns>
        public string BuildFilter()
        {
            IFilter filter = new And(new Is(AttributeNames.ObjectClass, PersonAttributeValues.Person),
                new IsNot(AttributeNames.ObjectClass, ComputerAttributeValues.Computer),
                new IsNot(AttributeNames.ObjectClass, InetOrgPersonAttributeValues.InetOrgPerson));
            return filter.BuildFilter();
        }
    }
}
