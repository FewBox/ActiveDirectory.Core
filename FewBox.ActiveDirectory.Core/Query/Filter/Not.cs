using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FewBox.ActiveDirectory.Core.Query.Filter
{
    /// <summary>
    /// The AD and filter (Eg: (!{0})).
    /// </summary>
    public class Not : Decorator
    {
        /// <summary>
        /// The constructure with filter collection param(Eg: (!{0})).
        /// The filter must inherit from IFilter interface.
        /// </summary>
        /// <param name="filters">The filter collection.</param>
        public Not(params IFilter[] filters)
            : base(filters)
        {
        }

        /// <summary>
        /// Build the filter expression.
        /// </summary>
        /// <param name="childrenFilterString">The children filter string.</param>
        /// <returns>The filter expression.</returns>
        protected override string BuildExpression(string childrenFilterString)
        {
            return String.Format(ExpressionTemplates.Not, childrenFilterString);
        }
    }
}
