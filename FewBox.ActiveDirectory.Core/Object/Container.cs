using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class Container : PackContainer
    {
        internal Container(DirectoryEntry directoryEntry) :
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
        /// Find one container object by common name.
        /// </summary>
        /// <param name="cn">The common name.</param>
        /// <returns>One container object.</returns>
        public static Container FindOneByCN(string cn)
        {
            return FindOne(new Is(AttributeNames.CN, cn));
        }

        /// <summary>
        /// Find all container objects.
        /// </summary>
        /// <param name="adOperator">The AD operator.</param>
        /// <returns>All container objects.</returns>
        public static IList<Container> FindAll()
        {
            return ActiveDirectoryQuery.FindAllByFilter<Container>(new IsContainer());
        }

        /// <summary>
        /// Find all user directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All user directory entry by filter.</returns>
        public static new IList<Container> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<Container>(new And(new IsContainer(), filter));
        }

        /// <summary>
        /// Find one user directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All user directory entry by filter.</returns>
        public static new Container FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<Container>(new And(new IsContainer(), filter));
        }
    }
}
