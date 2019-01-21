using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class Computer : ActiveDirectoryObject
    {
        internal Computer(DirectoryEntry directoryEntry) : 
            base(directoryEntry)
        {
        }

        private string objectSid;
        private string operatingSystemName;
        private string operatingSystemVersion;
        private string operatingSystemServicePack;
        private string dnsName;
        private string siteName;
        private IList<string> memberOf;
        private string managedBy;
        private DateTime passwordLastSetTime;
        private DateTime lastLogonTime;
        private int logonCount;
        private bool isEnabled;
        private ActiveDirectoryObject managedByObject;

        /// <summary>
        /// The object sid.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.ObjectSid)]
        public string ObjectSid
        {
            get
            {
                if (String.IsNullOrEmpty(this.objectSid))
                {
                    this.objectSid = new SidAdapter(this.DirectoryEntry, ComputerAttributeNames.ObjectSid).Value;
                }
                return this.objectSid;
            }
        }

        /// <summary>
        /// The operating system name.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.OperatingSystem)]
        public string OperatingSystemName
        {
            get
            {
                if (String.IsNullOrEmpty(this.operatingSystemName))
                {
                    this.operatingSystemName = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.OperatingSystem).Value;
                }
                return this.operatingSystemName;
            }
        }

        /// <summary>
        /// The operating system version.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.OperatingSystemVersion)]
        public string OperatingSystemVersion
        {
            get
            {
                if (String.IsNullOrEmpty(this.operatingSystemVersion))
                {
                    this.operatingSystemVersion = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.OperatingSystemVersion).Value;
                }
                return this.operatingSystemVersion;
            }
        }

        /// <summary>
        /// The operating system service pack.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.OperatingSystemServicePack)]
        public string OperatingSystemServicePack
        {
            get
            {
                if (String.IsNullOrEmpty(this.operatingSystemServicePack))
                {
                    this.operatingSystemServicePack = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.OperatingSystemServicePack).Value;
                }
                return this.operatingSystemServicePack;
            }
        }

        /// <summary>
        /// The dns name.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.DNSHostName)]
        public string DnsName
        {
            get
            {
                if (String.IsNullOrEmpty(this.dnsName))
                {
                    this.dnsName = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.DNSHostName).Value;
                }
                return this.dnsName;
            }
        }

        /// <summary>
        /// The site.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.MsDS_SiteName)]
        public string SiteName
        {
            get
            {
                if (String.IsNullOrEmpty(this.siteName))
                {
                    this.siteName = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.MsDS_SiteName).Value;
                }
                return this.siteName;
            }
        }

        /// <summary>
        /// The member of groups.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.MemberOf)]
        public IList<string> MemberOf
        {
            get
            {
                if (this.memberOf == null)
                {
                    this.memberOf = new MultipleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.MemberOf).Value;
                }
                return this.memberOf;
            }
            set
            {
                SetMultipleAttributeValue(ComputerAttributeNames.MemberOf, value);
                this.memberOf = value;
            }
        }

        /// <summary>
        /// The managed by user, group or contact distinguish name.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.ManagedBy)]
        public string ManagedBy
        {
            get
            {
                if (String.IsNullOrEmpty(this.managedBy))
                {
                    this.managedBy = new SingleLineAdapter(this.DirectoryEntry, ComputerAttributeNames.ManagedBy).Value;
                }
                return this.managedBy;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[ComputerAttributeNames.ManagedBy].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[ComputerAttributeNames.ManagedBy].Value = value;
                }
                this.managedBy = value;
            }
        }

        [ADOriginalAttributeName(ComputerAttributeNames.PasswordLastSetTime)]
        public DateTime PasswordLastSetTime {
            get
            {
                if (this.passwordLastSetTime==DateTime.MinValue)
                {
                    this.passwordLastSetTime = new LargeIntegerAdapter(this.DirectoryEntry, ComputerAttributeNames.PasswordLastSetTime).Value;
                }
                return this.passwordLastSetTime;
            }
            set
            {
                this.DirectoryEntry.Properties[ComputerAttributeNames.PasswordLastSetTime].Value = value;
                this.passwordLastSetTime = value;
            }
        }

        [ADOriginalAttributeName(ComputerAttributeNames.LastLogonTime)]
        public DateTime LastLogonTime
        {
            get
            {
                if (this.lastLogonTime==DateTime.MinValue)
                {
                    this.lastLogonTime = new LargeIntegerAdapter(this.DirectoryEntry, ComputerAttributeNames.LastLogonTime).Value;
                }
                return this.lastLogonTime;
            }
            set
            {
                this.DirectoryEntry.Properties[ComputerAttributeNames.LastLogonTime].Value = value;
                this.lastLogonTime = value;
            }
        }

        [ADOriginalAttributeName(ComputerAttributeNames.LogonCount)]
        public int LogonCount
        {
            get
            {
                if (this.logonCount==0)
                {
                    this.logonCount = new IntegerAdapter(this.DirectoryEntry, ComputerAttributeNames.LogonCount).Value;
                }
                return this.logonCount;
            }
            set
            {
                this.DirectoryEntry.Properties[ComputerAttributeNames.LogonCount].Value = value;
                this.logonCount = value;
            }
        }

        /// <summary>
        /// Gets whether the computer is enabled.
        /// </summary>
        [ADOriginalAttributeName(ComputerAttributeNames.UserAccountControl)]
        public bool IsEnabled
        {
            get
            {
                int userAccountControlValue = new IntegerAdapter(this.DirectoryEntry, ComputerAttributeNames.UserAccountControl).Value;
                if (((UserAccountControlType)(userAccountControlValue) & UserAccountControlType.AccountDisabled) != UserAccountControlType.AccountDisabled)
                {
                    this.isEnabled = true;
                }
                return this.isEnabled;
            }
            set
            {
                int userAccountControlValue = new IntegerAdapter(this.DirectoryEntry, ComputerAttributeNames.UserAccountControl).Value;
                var userAccountControlType = (UserAccountControlType)userAccountControlValue;
                if (value)
                {
                    this.DirectoryEntry.Properties[ComputerAttributeNames.UserAccountControl].Value = userAccountControlType ^ UserAccountControlType.AccountDisabled;
                }
                else
                {
                    this.DirectoryEntry.Properties[ComputerAttributeNames.UserAccountControl].Value = userAccountControlType | UserAccountControlType.AccountDisabled;
                }
                this.isEnabled = value;
            }
        }

        /// <summary>
        /// The managed by user, group or contact AD object.
        /// </summary>
        public ActiveDirectoryObject ManagedByObject
        {
            get
            {
                if (this.managedByObject == null)
                {
                    this.managedByObject = FindOneByDN(this.ManagedBy);
                }
                return this.managedByObject;
            }
        }

        /// <summary>
        /// Fine one computer object by sid.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <returns>One computer object.</returns>
        public static Computer FindOneBySid(string sid)
        {
            return FindOne(new Is(ComputerAttributeNames.ObjectSid, sid));
        }

        /// <summary>
        /// Fine one compter object by common name.
        /// </summary>
        /// <param name="cn">The common name.</param>
        /// <returns>One computer object.</returns>
        public static Computer FindOneByCN(string cn)
        {
            return FindOne(new Is(AttributeNames.CN, cn));
        }

        /// <summary>
        /// Find all computer directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All computer directory entry by filter.</returns>
        public static new IList<Computer> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<Computer>(new And(new IsComputer(), filter));
        }

        /// <summary>
        /// Find one computer directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All computer directory entry by filter.</returns>
        public static new Computer FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<Computer>(new And(new IsComputer(), filter));
        }
    }
}
