using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class OrganizationalUnit : ActiveDirectoryObject
    {
        #region AD Attributes

        private string ou;
        private string street;
        private string city;
        private string stateOrProvince;
        private string co;
        private string c;
        private string managedBy;

        /// <summary>
        /// The ou.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.OU)]
        public string OU
        {
            get
            {
                if (String.IsNullOrEmpty(this.ou))
                {
                    this.ou = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.OU).Value;
                }
                return this.ou;
            }
        }

        /// <summary>
        /// The street.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.Street)]
        public string Street
        {
            get
            {
                if (String.IsNullOrEmpty(this.street))
                {
                    this.street = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.Street).Value;
                }
                return this.street;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.Street].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.Street].Value = value;
                }
                this.street = value;
            }
        }

        /// <summary>
        /// The city.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.L)]
        public string City
        {
            get
            {
                if (String.IsNullOrEmpty(this.city))
                {
                    this.city = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.L).Value;
                }
                return this.city;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.L].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.L].Value = value;
                }
                this.city = value;
            }
        }

        /// <summary>
        /// The state / province.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.ST)]
        public string StateOrProvince
        {
            get
            {
                if (String.IsNullOrEmpty(this.stateOrProvince))
                {
                    this.stateOrProvince = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.ST).Value;
                }
                return this.stateOrProvince;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.ST].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.ST].Value = value;
                }
                this.stateOrProvince = value;
            }
        }

        /// <summary>
        /// The country or region.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.CO)]
        public string CO
        {
            get
            {
                if (String.IsNullOrEmpty(this.co))
                {
                    this.co = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.CO).Value;
                }
                return this.co;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.CO].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.CO].Value = value;
                }
                this.co = value;
            }
        }

        /// <summary>
        /// The country or region abbreviation (eg: CN).
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.C)]
        public string C
        {
            get
            {
                if (String.IsNullOrEmpty(this.c))
                {
                    this.c = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.C).Value;
                }
                return this.c;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.C].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.C].Value = value;
                }
                this.c = value;
            }
        }

        /// <summary>
        /// The managed by user distinguish name.
        /// </summary>
        [ADOriginalAttributeName(OrganizationalUnitAttributeNames.ManagedBy)]
        public string ManagedBy
        {
            get
            {
                if (String.IsNullOrEmpty(this.managedBy))
                {
                    this.managedBy = new SingleLineAdapter(this.DirectoryEntry, OrganizationalUnitAttributeNames.ManagedBy).Value;
                }
                return this.managedBy;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.ManagedBy].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[OrganizationalUnitAttributeNames.ManagedBy].Value = value;
                }
                this.managedBy = value;
            }
        }

        #endregion

        internal OrganizationalUnit(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }

        /// <summary>
        /// Add OU.
        /// </summary>
        /// <param name="ouName">OU name.</param>
        /// <returns>OU object.</returns>
        public OrganizationalUnit AddOrganizationalUnit(string ouName)
        {
            var ouDirectoryEntry = this.DirectoryEntry.Children.Add(String.Format(@"{0}={1}", OrganizationalUnitAttributeNames.OU, ouName), OrganizationalUnitAttributeValues.OrganizationalUnit);
            ouDirectoryEntry.CommitChanges();
            return FindOneByDN(ouDirectoryEntry.Properties[AttributeNames.DistinguishedName].Value.ToString()) as OrganizationalUnit;
        }

        /// <summary>
        /// Add Group.
        /// </summary>
        /// <param name="groupName">Group name.</param>
        /// <returns>Group object.</returns>
        public Group AddGroup(string groupName)
        {
            var groupDirectoryEntry = this.DirectoryEntry.Children.Add(String.Format(@"{0}={1}", AttributeNames.CN, groupName), GroupAttributeValues.Group);
            groupDirectoryEntry.CommitChanges();
            return Group.FindOneByCN(groupName);
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <returns>User object.</returns>
        public User AddUser(string userName)
        {
            var userDirectoryEntry = this.DirectoryEntry.Children.Add(String.Format(@"{0}={1}", AttributeNames.CN, userName), UserAttributeValues.User);
            userDirectoryEntry.CommitChanges();
            return User.FindOneByCN(userName);
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="contactName">Contact name.</param>
        /// <returns>User object.</returns>
        public Contact AddContact(string contactName)
        {
            var userDirectoryEntry = this.DirectoryEntry.Children.Add(String.Format(@"{0}={1}", AttributeNames.CN, contactName), ContactAttributeValues.Contact);
            userDirectoryEntry.CommitChanges();
            return Contact.FindOneByCN(contactName);
        }

        /// <summary>
        /// Fine one ou object by ou name.
        /// </summary>
        /// <param name="ouName">The OU name.</param>
        /// <returns>One ou object.</returns>
        public static OrganizationalUnit FindOneByOU(string ouName)
        {
            return FindOne(new Is(OrganizationalUnitAttributeNames.OU, ouName));
        }

        /// <summary>
        /// Fine one ou object by distinguished name.
        /// </summary>
        /// <param name="distinguishedName">The distinguished name</param>
        /// <returns></returns>
        public static new OrganizationalUnit FindOneByDN(string distinguishedName)
        {
            return FindOne(new Is(AttributeNames.DistinguishedName, distinguishedName));
        }

        /// <summary>
        /// Find all ou objects.
        /// </summary>
        /// <param name="adOperator">The AD operator.</param>
        /// <returns>All ou objects.</returns>
        public static IList<OrganizationalUnit> FindAll()
        {
            return ActiveDirectoryQuery.FindAllByFilter<OrganizationalUnit>(new IsOrganizationalUnit());
        }

        /// <summary>
        /// Find all ou objects.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All ou objects by filter.</returns>
        public static new IList<OrganizationalUnit> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<OrganizationalUnit>(new And(new IsOrganizationalUnit(), filter));
        }

        /// <summary>
        /// Find one ou directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>One ou directory entry by filter.</returns>
        public static new OrganizationalUnit FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<OrganizationalUnit>(new And(new IsOrganizationalUnit(), filter));
        }
    }
}
