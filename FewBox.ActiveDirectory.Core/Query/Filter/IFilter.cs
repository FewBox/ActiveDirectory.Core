namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// The AD filter interface.
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Build the AD filter string (Eg: <![CDATA[(&(cn=pangxiaoliang)(mail=pang*))]]>).
        /// </summary>
        /// <returns>The filter string.</returns>
        string BuildFilter();
    }
}
