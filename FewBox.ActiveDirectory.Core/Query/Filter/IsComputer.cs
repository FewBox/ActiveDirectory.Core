using FewBox.ActiveDirectory.Core.Attribute;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// Is computer filter (Eg: (objectClass=computer)).
    /// </summary>
    public class IsComputer : IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: (objectClass=computer)).
        /// </summary>
        /// <returns>The filter string.</returns>
        public string BuildFilter()
        {
            IFilter filter = new Is(AttributeNames.ObjectClass, ComputerAttributeValues.Computer);
            return filter.BuildFilter();
        }
    }
}
