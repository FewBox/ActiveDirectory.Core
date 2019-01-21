using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class User : PersonObject
    {
        #region AD Attributes

        private string objectSid;
        private string sAMAccountName;
        private string principalName;
        private IList<string> groupSids;
        private UserAccountControlType accountControlType;
        private bool isMustChangePwdNextLogon;
        private bool isEnabled;
        private bool isLocked;
        private DateTime accountExpiresTime;

        /// <summary>
        /// The object sid.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.ObjectSid)]
        public string ObjectSid
        {
            get
            {
                if (String.IsNullOrEmpty(this.objectSid))
                {
                    this.objectSid = new SidAdapter(this.DirectoryEntry, UserAttributeNames.ObjectSid).Value;
                }
                return this.objectSid;
            }
        }

        /// <summary>
        /// The pre Win2000 inditity name.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.SAMAccountName)]
        public string SAMAccountName
        {
            get
            {
                if (String.IsNullOrEmpty(this.sAMAccountName))
                {
                    this.sAMAccountName = new SingleLineAdapter(this.DirectoryEntry, UserAttributeNames.SAMAccountName).Value;
                }
                return this.sAMAccountName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.SAMAccountName].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.SAMAccountName].Value = value;
                }
                this.sAMAccountName = value;
            }
        }

        /// <summary>
        /// The user logon name(eg: [UserName]@[DomainName]).
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.UserPrincipalName)]
        public string PrincipalName
        {
            get
            {
                if (String.IsNullOrEmpty(this.principalName))
                {
                    this.principalName = new SingleLineAdapter(this.DirectoryEntry, UserAttributeNames.UserPrincipalName).Value;
                }
                return this.principalName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.UserPrincipalName].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.UserPrincipalName].Value = value;
                }
                this.principalName = value;
            }
        }

        /// <summary>
        /// The group sids.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.TokenGroups)]
        public IList<string> GroupSids
        {
            get
            {
                if (this.groupSids == null)
                {
                    this.groupSids = new SidsAdapter(this.DirectoryEntry, UserAttributeNames.TokenGroups).Value;
                }
                return this.groupSids;
            }
        }

        /// <summary>
        /// The user account control type.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.UserAccountControl)]
        public UserAccountControlType AccountControlType
        {
            get
            {
                if (this.accountControlType == UserAccountControlType.Unknow)
                {
                    this.accountControlType = (UserAccountControlType)(new IntegerAdapter(this.DirectoryEntry, UserAttributeNames.UserAccountControl).Value);
                }
                return this.accountControlType;
            }
            set
            {
                this.DirectoryEntry.Properties[UserAttributeNames.UserAccountControl].Value = value;
                this.accountControlType = value;
            }
        }

        /// <summary>
        /// Gets whether is domain admin.
        /// </summary>
        public bool IsDomainAdmin
        {
            get
            {
                return (from groupSid in this.GroupSids
                        where groupSid.EndsWith("-512")
                        select groupSid).Any();
            }
            set
            {
                Group domainAdminGroup = Group.FindOneByCN("Domain Admins");
                if (domainAdminGroup.Members != null && domainAdminGroup.Members.Count != 0)
                {
                    if (value)
                    {
                        if (!domainAdminGroup.Members.Contains(this.DistinguishedName))
                        {
                            List<string> members = new List<string>(domainAdminGroup.Members);
                            members.Add(this.DistinguishedName);
                            domainAdminGroup.Members = members;
                        }
                    }
                    else
                    {
                        if (domainAdminGroup.Members.Contains(this.DistinguishedName))
                        {
                            List<string> members = new List<string>(domainAdminGroup.Members);
                            members.Remove(this.DistinguishedName);
                            domainAdminGroup.Members = members;
                        }
                    }
                }
                else
                {
                    domainAdminGroup.Members = new List<string> { this.DistinguishedName };
                }
                domainAdminGroup.Save();
            }
        }

        /// <summary>
        /// Gets whether is account operator.
        /// </summary>
        public bool IsAccountOperator
        {
            get
            {
                return (from groupSid in this.GroupSids
                        where groupSid.EndsWith("-548")
                        select groupSid).Any();
            }
            set
            {
                Group accountOperatorGroup = Group.FindOneByCN("Account Operators");
                accountOperatorGroup.Members.Add(this.DistinguishedName);
                accountOperatorGroup.Save();
            }
        }

        /// <summary>
        /// Gets or sets whether the user must reset the password when next logon.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.PwdLastSet)]
        public bool IsMustChangePwdNextLogon
        {
            get
            {
                DateTime largeInteger = new LargeIntegerAdapter(this.DirectoryEntry, UserAttributeNames.PwdLastSet).Value;
                if (largeInteger == new DateTime(1601, 1, 1, 0, 0, 0))
                {
                    this.isMustChangePwdNextLogon = true;
                }
                else
                {
                    this.isMustChangePwdNextLogon = false;
                }
                return this.isMustChangePwdNextLogon;
            }
            set
            {
                this.isMustChangePwdNextLogon = value;
                if (value)
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.PwdLastSet].Value = 0;
                }
                else
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.PwdLastSet].Value = -1;
                }
            }
        }

        /// <summary>
        /// Gets whether the user is enabled.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.UserAccountControl)]
        public bool IsEnabled
        {
            get
            {
                int userAccountControlValue = new IntegerAdapter(this.DirectoryEntry, UserAttributeNames.UserAccountControl).Value;
                if (((UserAccountControlType)(userAccountControlValue) & UserAccountControlType.AccountDisabled) != UserAccountControlType.AccountDisabled)
                {
                    this.isEnabled = true;
                }
                return this.isEnabled;
            }
            set
            {
                int userAccountControlValue = new IntegerAdapter(this.DirectoryEntry, UserAttributeNames.UserAccountControl).Value;
                var userAccountControlType = (UserAccountControlType)userAccountControlValue;
                if (value)
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.UserAccountControl].Value = userAccountControlType ^ UserAccountControlType.AccountDisabled;
                }
                else
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.UserAccountControl].Value = userAccountControlType | UserAccountControlType.AccountDisabled;
                }
                this.isEnabled = value;
            }
        }

        /// <summary>
        /// Gets or sets whether the user is locked.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.LockoutTime)]
        public bool IsLocked
        {
            get
            {
                DateTime largeInteger = new LargeIntegerAdapter(this.DirectoryEntry, UserAttributeNames.LockoutTime).Value;
                if (largeInteger == new DateTime(1601, 1, 1, 0, 0, 0))
                {
                    this.isLocked = false;
                }
                else
                {
                    this.isLocked = true;
                }
                return this.isLocked;
            }
            set
            {
                this.isLocked = value;
                if (value)
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.LockoutTime].Value = DateTime.Now.ToFileTimeUtc();
                }
                else
                {
                    this.DirectoryEntry.Properties[UserAttributeNames.LockoutTime].Value = 0;
                }
            }
        }

        /// <summary>
        /// Gets or sets the account expires time (UTC) of user.
        /// </summary>
        [ADOriginalAttributeName(UserAttributeNames.AccountExpires)]
        public DateTime AccountExpiresTime
        {
            get
            {
                if (this.accountExpiresTime == DateTime.MinValue)
                {
                    this.accountExpiresTime = new LargeIntegerAdapter(this.DirectoryEntry, UserAttributeNames.AccountExpires).Value;
                }
                return this.accountExpiresTime;
            }
            set
            {
                this.accountExpiresTime = value;
                this.DirectoryEntry.Properties[UserAttributeNames.AccountExpires].Value = value.ToFileTimeUtc().ToString();
            }
        }

        #endregion

        internal User(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        } 

        /// <summary>
        /// Fine one user object by sid.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <returns>One user object.</returns>
        public static User FindOneBySid(string sid)
        {
            return FindOne(new Is(UserAttributeNames.ObjectSid, sid));
        }

        /// <summary>
        /// Find one user object by sAMAccountName.
        /// </summary>
        /// <param name="sAMAccountName">The sAMAccountName.</param>
        /// <returns>One user object.</returns>
        public static User FindOneBySAMAccountName(string sAMAccountName)
        {
            return FindOne(new Is(UserAttributeNames.SAMAccountName, sAMAccountName));
        }

        /// <summary>
        /// Fine one user object by common name.
        /// </summary>
        /// <param name="cn">The common name.</param>
        /// <returns>One user object.</returns>
        public static User FindOneByCN(string cn)
        {
            return FindOne(new Is(AttributeNames.CN, cn));
        }

        /// <summary>
        /// Find all user directory entry.
        /// </summary>
        /// <returns></returns>
        public static IList<User> FindAll()
        {
            return ActiveDirectoryQuery.FindAllByFilter<User>(new IsUser());
        }

        /// <summary>
        /// Find all user directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All user directory entry by filter.</returns>
        public static new IList<User> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<User>(new And(new IsUser(), filter));
        }

        /// <summary>
        /// Find one user directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>One user directory entry by filter.</returns>
        public static new User FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<User>(new And(new IsUser(), filter));
        }

        public static bool IsPasswordValid(string domain, string username, string password)
        {
            bool isPasswordValid = false;
            try
            {
                using (DirectoryEntry rootDirectoryEntry = new DirectoryEntry(String.Format("LDAP://{0}", domain), username, password))
                {
                    Guid objectId = rootDirectoryEntry.Guid;
                    isPasswordValid = true;
                }
            }
            catch (System.Exception exception)
            {
                if (exception.Message.Contains("Logon failure: unknown user name or bad password.") ||
                    exception.Message.Contains("The user name or password is incorrect."))
                {
                }
                else
                {
                    throw;
                }
            }
            return isPasswordValid;
        }

        public static int CountExpired(int days = 0) {
            return FindAllExpired(days).Count();
        }

        public static IList<User> FindAllExpired(int days = 0) {
            DateTime now = DateTime.UtcNow.AddDays(days).Date;
            DateTime never = new DateTime(1601, 1, 1);
            return FindAll(new And(
                new LessThanOrEqualTo(
                    UserAttributeNames.AccountExpires,
                    now.ToFileTimeUtc().ToString()),
                new IsNot(
                    UserAttributeNames.AccountExpires,
                    never.ToFileTimeUtc().ToString())));
        }

        public static IList<User> FindAllDisabled() {
            return FindAll(new Custom("userAccountControl:1.2.840.113556.1.4.803:=2"));
        }

        public static IList<User> FindAllLocked()
        {
            DateTime never = new DateTime(1601, 1, 1);
            return FindAll(new IsNot(UserAttributeNames.LockoutTime, never.ToFileTimeUtc().ToString()));
        }

        /// <summary>
        /// Reset the password.
        /// </summary>
        /// <param name="password">The password</param>
        public void ResetPassword(string password)
        {
            this.DirectoryEntry.Invoke("SetPassword", new object[] { password });
            this.DirectoryEntry.Properties[UserAttributeNames.LockoutTime].Value = 0;
            this.DirectoryEntry.CommitChanges();
        }
    }
}
