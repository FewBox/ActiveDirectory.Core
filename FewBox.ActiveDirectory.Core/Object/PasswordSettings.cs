using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class PasswordSettings : ActiveDirectoryObject
    {
        #region AD Attributes

        private int customPolicyMinimumPasswordLength;
        private bool? isMustMeetComplexityRequirments;

        /// <summary>
        /// The group policy minimum password length.
        /// </summary>
        [ADOriginalAttributeName(PasswordSettingsAttributeNames.MsDS_MinimumPasswordLength)]
        public int CustomPolicyMinimumPasswordLength
        {
            get
            {
                if (this.customPolicyMinimumPasswordLength == -1)
                {
                    this.customPolicyMinimumPasswordLength = new IntegerAdapter(this.DirectoryEntry, PasswordSettingsAttributeNames.MsDS_MinimumPasswordLength).Value;
                }
                return this.customPolicyMinimumPasswordLength;
            }
        }

        /// <summary>
        /// Is must meet complexity requirements policy.
        /// </summary>
        [ADOriginalAttributeName(PasswordSettingsAttributeNames.MsDS_PasswordComplexityEnabled)]
        public bool IsMustMeetComplexityRequirments
        {
            get
            {
                if (isMustMeetComplexityRequirments == null)
                {
                    isMustMeetComplexityRequirments = new BooleanAdapter(this.DirectoryEntry, PasswordSettingsAttributeNames.MsDS_PasswordComplexityEnabled).Value;
                }
                return (bool)(isMustMeetComplexityRequirments);
            }
        }

        #endregion

        internal PasswordSettings(DirectoryEntry directoryEntry) : 
            base(directoryEntry)
        {
        }

        public static IList<PasswordSettings> FindAll(string userCn)
        {
            var userObject = User.FindOneByCN(userCn);
            return FindAll(new And(
                    new IsPasswordSettings(),
                    new Or(
                        new Is(PSOAttributeNames.MsDS_PSOAppliesTo, userObject.DistinguishedName),
                        new Is(PSOAttributeNames.MsDS_PSOAppliesTo, userObject.ObjectSid)))) as List<PasswordSettings>;
        }
    }
}
