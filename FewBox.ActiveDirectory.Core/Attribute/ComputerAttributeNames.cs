namespace FewBox.ActiveDirectory.Core.Attribute
{
    /// <summary>
    /// The names of Computer AD object common attribute.
    /// </summary>
    public class ComputerAttributeNames : AttributeNames
    {
        /// <summary>
        /// The sid.
        /// </summary>
        public const string ObjectSid = "objectSid";

        /// <summary>
        /// The manged by user.
        /// </summary>
        public const string ManagedBy = "managedBy";

        /// <summary>
        /// The operating system name.
        /// </summary>
        public const string OperatingSystem = "operatingSystem";

        /// <summary>
        /// The operating system version.
        /// </summary>
        public const string OperatingSystemVersion = "operatingSystemVersion";

        /// <summary>
        /// The operating system service pack.
        /// </summary>
        public const string OperatingSystemServicePack = "operatingSystemServicePack";

        /// <summary>
        /// The dns host name.
        /// </summary>
        public const string DNSHostName = "dNSHostName";

        /// <summary>
        /// The site name.
        /// </summary>
        public const string MsDS_SiteName = "msDS-SiteName";

        /// <summary>
        /// The member of group.
        /// </summary>
        public const string MemberOf = "memberOf";

        /// <summary>
        /// Password last set time.
        /// </summary>
        public const string PasswordLastSetTime = "pwdLastSet";

        /// <summary>
        /// Last logon time.
        /// </summary>
        public const string LastLogonTime = "lastLogon";

        /// <summary>
        /// Logon count.
        /// </summary>
        public const string LogonCount = "logonCount";

        /// <summary>
        /// The user account control (eg: enabled.).
        /// </summary>
        public const string UserAccountControl = "userAccountControl";
    }
}
