using System;

namespace FewBox.ActiveDirectory.Core.Object
{
    [Flags]
    enum BuildInGroupType
    {
        None = 0,
        /// <summary>
        /// Type: Security
        /// </summary>
        Security = -2147483648,
        /// <summary>
        /// Scope: Global
        /// </summary>
        Global = 2,
        /// <summary>
        /// Scope: Domian Local
        /// </summary>
        DomainLocal = 4,
        /// <summary>
        /// Scope: Universal
        /// </summary>
        Universal = 8,
    }
}
