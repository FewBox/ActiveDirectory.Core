using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class Group : ActiveDirectoryObject
    {
        #region AD Attributes

        private string managedBy;
        private string objectSid;
        private string sAMAccountName;
        private IList<string> groupSids;
        private string email;
        private string notes;
        private IList<string> members;
        private GroupType groupType;
        private GroupScopeType groupScopeType;

        /// <summary>
        /// The object sid.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.ObjectSid)]
        public string ObjectSid
        {
            get
            {
                if (String.IsNullOrEmpty(this.objectSid))
                {
                    this.objectSid = new SidAdapter(this.DirectoryEntry, GroupAttributeNames.ObjectSid).Value;
                }
                return this.objectSid;
            }
        }

        /// <summary>
        /// The pre Win2000 inditity name.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.SAMAccountName)]
        public string SAMAccountName
        {
            get
            {
                if (String.IsNullOrEmpty(this.sAMAccountName))
                {
                    this.sAMAccountName = new SingleLineAdapter(this.DirectoryEntry, GroupAttributeNames.SAMAccountName).Value;
                }
                return this.sAMAccountName;
            }
            set
            {
                this.DirectoryEntry.Properties[GroupAttributeNames.SAMAccountName].Value = value;
                this.sAMAccountName = value;
            }
        }

        /// <summary>
        /// The group sids.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.TokenGroups)]
        public IList<string> GroupSids
        {
            get
            {
                if (this.groupSids == null)
                {
                    this.groupSids = new SidsAdapter(this.DirectoryEntry, GroupAttributeNames.TokenGroups).Value;
                }
                return this.groupSids;
            }
        }

        /// <summary>
        /// The managed by user distinguish name.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.ManagedBy)]
        public string ManagedBy
        {
            get
            {
                if (String.IsNullOrEmpty(this.managedBy))
                {
                    this.managedBy = new SingleLineAdapter(this.DirectoryEntry, GroupAttributeNames.ManagedBy).Value;
                }
                return this.managedBy;
            }
            set
            {
                this.DirectoryEntry.Properties[GroupAttributeNames.ManagedBy].Value = value;
                this.managedBy = value;
            }
        }

        /// <summary>
        /// The email.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Mail)]
        public string Email
        {
            get
            {
                if (String.IsNullOrEmpty(this.email))
                {
                    this.email = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Mail).Value;
                }
                return this.email;
            }
            set
            {
                this.DirectoryEntry.Properties[PersonAttributeNames.Mail].Value = value;
                this.email = value;
            }
        }

        /// <summary>
        /// The notes.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Info)]
        public string Notes
        {
            get
            {
                if (String.IsNullOrEmpty(this.notes))
                {
                    this.notes = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Info).Value;
                }
                return this.notes;
            }
            set
            {
                this.DirectoryEntry.Properties[PersonAttributeNames.Info].Value = value;
                this.notes = value;
            }
        }

        /// <summary>
        /// The members.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.Member)]
        public IList<string> Members
        {
            get
            {
                if (this.members == null)
                {
                    this.members = new MultipleLineAdapter(this.DirectoryEntry, GroupAttributeNames.Member).Value;
                }
                return this.members;
            }
            set
            {
                SetMultipleAttributeValue(GroupAttributeNames.Member, value);
                this.members = value;
            }
        }

        /// <summary>
        /// The group type.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.GroupType)]
        public GroupType GroupType
        {
            get
            {
                if (this.groupType == GroupType.Unknow)
                {
                    int groupTypeValue = new IntegerAdapter(this.DirectoryEntry, GroupAttributeNames.GroupType).Value;
                    var buildInGroupType = (BuildInGroupType)groupTypeValue;
                    if ((buildInGroupType & BuildInGroupType.Security) != BuildInGroupType.None)
                    {
                        this.groupType = GroupType.Security;
                    }
                    else
                    {
                        this.groupType = GroupType.Distribution;
                    }
                }
                return this.groupType;
            }
            set
            {
                var buildInGroupType = BuildInGroupType.None;
                switch (this.GroupScopeType)
                {
                    case GroupScopeType.DomainLocal:
                        buildInGroupType |= BuildInGroupType.DomainLocal;
                        break;
                    case GroupScopeType.Global:
                        buildInGroupType |= BuildInGroupType.Global;
                        break;
                    case GroupScopeType.Universal:
                        buildInGroupType |= BuildInGroupType.Universal;
                        break;
                }
                if (value == GroupType.Security)
                {
                    this.DirectoryEntry.Properties[GroupAttributeNames.GroupType].Value = (buildInGroupType | BuildInGroupType.Security);
                }
                else
                {
                    this.DirectoryEntry.Properties[GroupAttributeNames.GroupType].Value = buildInGroupType;
                }
                this.groupType = value;
            }
        }

        /// <summary>
        /// The group scope type.
        /// </summary>
        [ADOriginalAttributeName(GroupAttributeNames.GroupType)]
        public GroupScopeType GroupScopeType
        {
            get
            {
                if (this.groupScopeType == GroupScopeType.Unknow)
                {
                    int groupTypeValue = new IntegerAdapter(this.DirectoryEntry, GroupAttributeNames.GroupType).Value;
                    var buildInGroupType = (BuildInGroupType)groupTypeValue;
                    if ((buildInGroupType & BuildInGroupType.DomainLocal) != BuildInGroupType.None)
                    {
                        this.groupScopeType = GroupScopeType.DomainLocal;
                    }
                    else if ((buildInGroupType & BuildInGroupType.Global) != BuildInGroupType.None)
                    {
                        this.groupScopeType = GroupScopeType.Global;
                    }
                    else if ((buildInGroupType & BuildInGroupType.Universal) != BuildInGroupType.None)
                    {
                        this.groupScopeType = GroupScopeType.Universal;
                    }
                }
                return this.groupScopeType;
            }
            set
            {
                var buildInGroupType = BuildInGroupType.None;
                switch (this.GroupType)
                {
                    case GroupType.Distribution:
                        break;
                    case GroupType.Security:
                        buildInGroupType |= BuildInGroupType.Security;
                        break;
                }
                if (value == GroupScopeType.DomainLocal)
                {
                    this.DirectoryEntry.Properties[GroupAttributeNames.GroupType].Value = 
                        (buildInGroupType | BuildInGroupType.DomainLocal);
                }
                else if (value == GroupScopeType.Global)
                {
                    this.DirectoryEntry.Properties[GroupAttributeNames.GroupType].Value =
                        (buildInGroupType | BuildInGroupType.Global);
                }
                else
                {
                    this.DirectoryEntry.Properties[GroupAttributeNames.GroupType].Value =
                        (buildInGroupType | BuildInGroupType.Universal);
                }
                this.groupScopeType = value;
            }
        }

        #endregion

        internal Group(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }

        /// <summary>
        /// Fine one user directory entry.
        /// </summary>
        /// <param name="sid">The sid.</param>
        /// <returns></returns>
        public static Group FindOneBySid(string sid)
        {
            return FindOne(new Is(GroupAttributeNames.ObjectSid, sid));
        }

        /// <summary>
        /// Fine one group object by common name.
        /// </summary>
        /// <param name="cn">The common name.</param>
        /// <returns>One group object.</returns>
        public static Group FindOneByCN(string cn)
        {
            return FindOne(new Is(AttributeNames.CN, cn));
        }

        /// <summary>
        /// Find all group objects.
        /// </summary>
        /// <param name="adOperator">The AD operator.</param>
        /// <returns>All group objects.</returns>
        public static IList<Group> FindAll()
        {
            return ActiveDirectoryQuery.FindAllByFilter<Group>(new IsGroup());
        }

        /// <summary>
        /// Find all group objects.
        /// </summary>
        /// <param name="sids">The sids.</param>
        /// <returns>All group objects.</returns>
        public static IList<Group> FindAllBySids(IList<string> sids)
        {
            List<IFilter> sidFilters = new List<IFilter>();
            foreach (string sid in sids)
            {
                sidFilters.Add(new Is(GroupAttributeNames.ObjectSid, sid));
            }
            IFilter filter = new Or(sidFilters.ToArray());
            return FindAll(filter);
        }

        /// <summary>
        /// Find one group object by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>One group object by filter.</returns>
        public static new Group FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<Group>(new And(new IsGroup(), filter));
        }

        /// <summary>
        /// Find all group objects by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All group objects by filter.</returns>
        public static new IList<Group> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<Group>(new And(new IsGroup(), filter));
        }
    }
}
