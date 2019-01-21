using System;

namespace FewBox.ActiveDirectory.Core.Attribute
{
    /// <summary>
    /// The ADObject property original attriubte name custom attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ADOriginalAttributeNameAttribute : System.Attribute
    {
        /// <summary>
        /// The name of the original attribute.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Init the instance with original attribute name.
        /// </summary>
        /// <param name="name">The original attribute name.</param>
        public ADOriginalAttributeNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}
