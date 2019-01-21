using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace FewBox.ActiveDirectory.Core.Object
{
    public abstract class PersonObject : ActiveDirectoryObject
    {
        #region AD Attributes

        private byte[] thumbnailPhoto;
        private byte[] thumbnailLogo;
        private string email;
        private string co;
        private string c;
        private string company;
        private int countryCode;
        private string department;
        private string fax;
        private IList<string> otherFaxes;
        private string givenName;
        private string homePhone;
        private IList<string> otherHomePhones;
        private string notes;
        private string initials;
        private string ipPhone;
        private IList<string> otherIpPhones;
        private string city;
        private string manager;
        private IList<string> memberOf;
        private string mobile;
        private IList<string> otherMobiles;
        private string pager;
        private IList<string> otherPagers;
        private string telephone;
        private IList<string> otherTelephones;
        private string lastName;
        private string stateOrProvince;
        private string streetAddress;
        private string jobTitle;
        private User user;

        /// <summary>
        /// The thumbnailPhoto.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.ThumbnailPhoto)]
        public byte[] ThumbnailPhoto
        {
            get
            {
                if (this.thumbnailPhoto == null)
                {
                    this.thumbnailPhoto =
                        new ByteArrayAdapter(this.DirectoryEntry, PersonAttributeNames.ThumbnailPhoto).Value;
                }
                return this.thumbnailPhoto;
            }
            set
            {
                if (value==null)
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ThumbnailPhoto].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ThumbnailPhoto].Value = value;
                }
                this.thumbnailPhoto = value;
            }
        }

        /// <summary>
        /// The thumbnailLogo.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.ThumbnailLogo)]
        public byte[] ThumbnailLogo
        {
            get
            {
                if (this.thumbnailLogo == null)
                {
                    this.thumbnailLogo =
                        new ByteArrayAdapter(this.DirectoryEntry, PersonAttributeNames.ThumbnailLogo).Value;
                }
                return this.thumbnailLogo;
            }
            set
            {
                if (value == null)
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ThumbnailLogo].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ThumbnailLogo].Value = value;
                }
                this.thumbnailLogo = value;
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
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Mail].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Mail].Value = value;
                }
                this.email = value;
            }
        }

        /// <summary>
        /// The country or region.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.CO)]
        public string CO
        {
            get
            {
                if (String.IsNullOrEmpty(this.co))
                {
                    this.co = new SingleLineAdapter(this.DirectoryEntry,PersonAttributeNames.CO).Value;
                }
                return this.co;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.CO].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.CO].Value = value;
                }
                this.co = value;
            }
        }

        /// <summary>
        /// The country or region abbreviation (eg: CN).
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.C)]
        public string C
        {
            get
            {
                if (String.IsNullOrEmpty(this.c))
                {
                    this.c = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.C).Value;
                }
                return this.c;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.C].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.C].Value = value;
                }
                this.c = value;
            }
        }

        /// <summary>
        /// The company.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Company)]
        public string Company
        {
            get
            {
                if (String.IsNullOrEmpty(this.company))
                {
                    this.company = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Company).Value;
                }
                return this.company;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Company].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Company].Value = value;
                }
                this.company = value;
            }
        }

        /// <summary>
        /// The country code;
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.CountryCode)]
        public int CountryCode
        {
            get
            {
                if (this.countryCode != -1)
                {
                    this.countryCode = new IntegerAdapter(this.DirectoryEntry, PersonAttributeNames.CountryCode).Value;
                }
                return this.countryCode;
            }
            set
            {
                this.DirectoryEntry.Properties[PersonAttributeNames.CountryCode].Value = value;
                this.countryCode = value;
            }
        }

        /// <summary>
        /// The department.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Department)]
        public string Department
        {
            get
            {
                if (String.IsNullOrEmpty(this.department))
                {
                    this.department =
                        new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Department).Value;
                }
                return this.department;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Department].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Department].Value = value;
                }
                this.department = value;
            }
        }

        /// <summary>
        /// The fax number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.FacsimileTelephoneNumber)]
        public string Fax
        {
            get
            {
                if (String.IsNullOrEmpty(this.fax))
                {
                    this.fax = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.FacsimileTelephoneNumber).Value;
                }
                return this.fax;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.FacsimileTelephoneNumber].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.FacsimileTelephoneNumber].Value = value;
                }
                this.fax = value;
            }
        }

        /// <summary>
        /// Other fax numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherFacsimileTelephoneNumber)]
        public IList<string> OtherFaxes
        {
            get
            {
                if (this.otherFaxes == null)
                {
                    this.otherFaxes = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherFacsimileTelephoneNumber).Value;
                }
                return this.otherFaxes;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherFacsimileTelephoneNumber, value);
                this.otherFaxes = value;
            }
        }

        /// <summary>
        /// The given name.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.GivenName)]
        public string GivenName
        {
            get
            {
                if (String.IsNullOrEmpty(this.givenName))
                {
                    this.givenName = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.GivenName).Value;
                }
                return this.givenName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.GivenName].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.GivenName].Value = value;
                }
                this.givenName = value;
            }
        }

        /// <summary>
        /// The home phone number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.HomePhone)]
        public string HomePhone
        {
            get
            {
                if (String.IsNullOrEmpty(this.homePhone))
                {
                    this.homePhone = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.HomePhone).Value;
                }
                return this.homePhone;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.HomePhone].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.HomePhone].Value = value;
                }
                this.homePhone = value;
            }
        }

        /// <summary>
        /// The other home phone numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherHomePhone)]
        public IList<string> OtherHomePhones
        {
            get
            {
                if (this.otherHomePhones == null)
                {
                    this.otherHomePhones = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherHomePhone).Value;
                }
                return this.otherHomePhones;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherHomePhone, value);
                this.otherHomePhones = value;
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
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Info].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Info].Value = value;
                }
                this.notes = value;
            }
        }

        /// <summary>
        /// The initals.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Initials)]
        public string Initials
        {
            get
            {
                if (String.IsNullOrEmpty(this.initials))
                {
                    this.initials = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Initials).Value;
                }
                return this.initials;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Initials].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Initials].Value = value;
                }
                this.initials = value;
            }
        }

        /// <summary>
        /// The IP phone number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.IpPhone)]
        public string IpPhone
        {
            get
            {
                if (String.IsNullOrEmpty(this.ipPhone))
                {
                    this.ipPhone = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.IpPhone).Value;
                }
                return this.ipPhone;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.IpPhone].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.IpPhone].Value = value;
                }
                this.ipPhone = value;
            }
        }

        /// <summary>
        /// The other IP phone numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherIpPhone)]
        public IList<string> OtherIpPhones
        {
            get
            {
                if (this.otherIpPhones == null)
                {
                    this.otherIpPhones = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherIpPhone).Value;
                }
                return this.otherIpPhones;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherIpPhone, value);
                this.otherIpPhones = value;
            }
        }

        /// <summary>
        /// The city.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.L)]
        public string City
        {
            get
            {
                if (String.IsNullOrEmpty(this.city))
                {
                    this.city = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.L).Value;
                }
                return this.city;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.L].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.L].Value = value;
                }
                this.city = value;
            }
        }

        /// <summary>
        /// The manager.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Manager)]
        public string Manager
        {
            get
            {
                if (String.IsNullOrEmpty(this.manager))
                {
                    this.manager = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Manager).Value;
                }
                return this.manager;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Manager].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Manager].Value = value;
                }
                this.manager = value;
            }
        }

        /// <summary>
        /// The manager user object.
        /// </summary>
        public User ManagerUser
        {
            get
            {
                if (this.user == null)
                {
                    this.user = FindOneByDN(this.Manager) as User;
                }
                return this.user;
            }
            set
            {
                this.DirectoryEntry.Properties[PersonAttributeNames.Manager].Value = value.DistinguishedName;
                this.user = value;
            }
        }

        /// <summary>
        /// The member of groups.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.MemberOf)]
        public IList<string> MemberOf
        {
            get
            {
                if (this.memberOf == null)
                {
                    this.memberOf = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.MemberOf).Value;
                }
                return this.memberOf;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.MemberOf, value);
                this.memberOf = value;
            }
        }

        /// <summary>
        /// The mobile number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Mobile)]
        public string Mobile
        {
            get
            {
                if (String.IsNullOrEmpty(this.mobile))
                {
                    this.mobile = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Mobile).Value;
                }
                return this.mobile;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Mobile].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Mobile].Value = value;
                }
                this.mobile = value;
            }
        }

        /// <summary>
        /// Other mobile numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherMobile)]
        public IList<string> OtherMobiles
        {
            get
            {
                if (this.otherMobiles == null)
                {
                    this.otherMobiles = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherMobile).Value;
                }
                return this.otherMobiles;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherMobile, value);
                this.otherMobiles = value;
            }
        }

        /// <summary>
        /// The pager number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Pager)]
        public string Pager
        {
            get
            {
                if (String.IsNullOrEmpty(this.pager))
                {
                    this.pager = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Pager).Value;
                }
                return this.pager;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Pager].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Pager].Value = value;
                }
                this.pager = value;
            }
        }

        /// <summary>
        /// The other pager numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherPager)]
        public IList<string> OtherPagers
        {
            get
            {
                if (this.otherPagers == null)
                {
                    this.otherPagers = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherPager).Value;
                }
                return this.otherPagers;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherPager, value);
                this.otherPagers = value;
            }
        }

        /// <summary>
        /// The telephone number.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.TelephoneNumber)]
        public string Telephone
        {
            get
            {
                if (String.IsNullOrEmpty(this.telephone))
                {
                    this.telephone = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.TelephoneNumber).Value;
                }
                return this.telephone;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.TelephoneNumber].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.TelephoneNumber].Value = value;
                }
                this.telephone = value;
            }
        }

        /// <summary>
        /// The other telephone numbers.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.OtherTelephone)]
        public IList<string> OtherTelephones
        {
            get
            {
                if (this.otherTelephones == null)
                {
                    this.otherTelephones = new MultipleLineAdapter(this.DirectoryEntry, PersonAttributeNames.OtherTelephone).Value;
                }
                return this.otherTelephones;
            }
            set
            {
                SetMultipleAttributeValue(PersonAttributeNames.OtherTelephone, value);
                this.otherTelephones = value;
            }
        }

        /// <summary>
        /// The last name.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.SN)]
        public string LastName
        {
            get
            {
                if (String.IsNullOrEmpty(this.lastName))
                {
                    this.lastName = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.SN).Value;
                }
                return this.lastName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.SN].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.SN].Value = value;
                }
                this.lastName = value;
            }
        }

        /// <summary>
        /// The state / province.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.ST)]
        public string StateOrProvince
        {
            get
            {
                if (String.IsNullOrEmpty(this.stateOrProvince))
                {
                    this.stateOrProvince = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.ST).Value;
                }
                return this.stateOrProvince;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ST].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.ST].Value = value;
                }
                this.stateOrProvince = value;
            }
        }

        /// <summary>
        /// The address of street.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.StreetAddress)]
        public string StreetAddress
        {
            get
            {
                if (String.IsNullOrEmpty(this.streetAddress))
                {
                    this.streetAddress = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.StreetAddress).Value;
                }
                return this.streetAddress;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.StreetAddress].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.StreetAddress].Value = value;
                }
                this.streetAddress = value;
            }
        }

        /// <summary>
        /// The job title.
        /// </summary>
        [ADOriginalAttributeName(PersonAttributeNames.Title)]
        public string JobTitle
        {
            get
            {
                if (String.IsNullOrEmpty(this.jobTitle))
                {
                    this.jobTitle = new SingleLineAdapter(this.DirectoryEntry, PersonAttributeNames.Title).Value;
                }
                return this.jobTitle;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Title].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[PersonAttributeNames.Title].Value = value;
                }
                this.jobTitle = value;
            }
        }

        #endregion

        internal PersonObject(DirectoryEntry directoryEntry) : 
            base(directoryEntry)
        {
        }
    }
}
