using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class PackContainer : ActiveDirectoryObject
    {
        #region AD Attributes

        private IList<User> users;
        private IList<Contact> contacts;
        private IList<Computer> computers;

        /// <summary>
        /// The child users.
        /// </summary>
        public IList<User> Users
        {
            get
            {
                if (this.users == null)
                {
                    this.users = new List<User>();
                    foreach (DirectoryEntry child in this.DirectoryEntry.Children)
                    {
                        using (child)
                        {
                            var user = FindOne(new And(
                                    new Is(AttributeNames.DistinguishedName, child.Properties[AttributeNames.DistinguishedName].Value.ToString()),
                                    new IsUser()
                                    )) as User;
                            if (user != null)
                            {
                                this.users.Add(user);
                            }
                        }
                    }
                }
                return this.users;
            }
        }

        /// <summary>
        /// The child contacts.
        /// </summary>
        public IList<Contact> Contacts
        {
            get
            {
                if (this.contacts == null)
                {
                    this.contacts = new List<Contact>();
                    foreach (DirectoryEntry child in this.DirectoryEntry.Children)
                    {
                        using (child)
                        {
                            var contact = FindOne(new And(
                                    new Is(AttributeNames.DistinguishedName, child.Properties[AttributeNames.DistinguishedName].Value.ToString()),
                                    new IsContact()
                                    )) as Contact;
                            if (contact != null)
                            {
                                this.contacts.Add(contact);
                            }
                        }
                    }
                }
                return this.contacts;
            }
        }

        /// <summary>
        /// The child computers.
        /// </summary>
        public IList<Computer> Computers
        {
            get
            {
                if (this.computers == null)
                {
                    this.computers = new List<Computer>();
                    foreach (DirectoryEntry child in this.DirectoryEntry.Children)
                    {
                        using (child)
                        {
                            var computer = FindOne(new And(
                                    new Is(AttributeNames.DistinguishedName, child.Properties[AttributeNames.DistinguishedName].Value.ToString()),
                                    new IsComputer())) as Computer;
                            if (computer != null)
                            {
                                this.computers.Add(computer);
                            }
                        }
                    }
                }
                return this.computers;
            }
        }

        #endregion

        internal PackContainer(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }
    }
}
