namespace FewBox.ActiveDirectory.Core.Attribute
{
    public class DomainControllerAttributeNames: PSOAttributeNames
    {
        /// <summary>
        /// The custom password policy precedence.
        /// </summary>
        public const string MsDS_PasswordSettingsPrecedence = "msDS-PasswordSettingsPrecedence";

        /// <summary>
        /// The min length of the password.
        /// </summary>
        public const string MinPwdLength = "minPwdLength";

        /// <summary>
        /// The properties of the password.
        /// </summary>
        public const string PwdProperties = "pwdProperties";
    }
}
