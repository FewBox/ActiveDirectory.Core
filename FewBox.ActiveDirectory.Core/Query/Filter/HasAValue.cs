namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// The AD has a value filter (Eg: ({0}=*)).
    /// </summary>
    public class HasAValue : AttributeKeyFilter
    {
        /// <summary>
        /// The constructure with attribute name param (Eg: ({0}=*)).
        /// </summary>
        /// <param name="attributeName">The attribute name which can be find in FewBox.ActiveDirectory.Entity.Attribute.Name namespace or custom set.</param>
        public HasAValue(string attributeName)
            : base(attributeName)
        {
        }

        /// <summary>
        /// Build the expression template.
        /// </summary>
        /// <returns>The expression template.</returns>
        protected override string BuildExpressionTemplate()
        {
            return ExpressionTemplates.HasAValueExpression;
        }
    }
}
