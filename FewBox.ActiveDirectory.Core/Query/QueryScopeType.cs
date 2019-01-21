namespace FewBox.ActiveDirectory.Core.Query
{
    /// <summary>
    /// The query scope type.
    /// </summary>
    public enum QueryScopeType
    {
        /// <summary>
        /// Limits the search to the base object. The result contains a maximum of one object.
        /// </summary>
        Base = 0,
        /// <summary>
        /// Contains the current AD object and the child AD objects of the current AD object.
        /// </summary>
        OneLevel = 1,
        /// <summary>
        /// Contains the current AD object and child AD objects of the whole subtree.
        /// </summary>
        Subtree = 2
    }
}
