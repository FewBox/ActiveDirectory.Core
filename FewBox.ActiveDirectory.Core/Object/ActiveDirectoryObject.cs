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
    public abstract class ActiveDirectoryObject : IDisposable
    {
        protected DirectoryEntry DirectoryEntry { get; set; }

        #region AD Attributes

        private string cn;
        private Guid objectGuid;
        private string distinguishedName;
        private string name;
        private string canonicalName;
        private DateTime createTime;
        private DateTime modifyTime;
        private string description;
        private IList<string> directReports;
        private string displayName;
        private string msDS_PrincipalName;
        private string office;
        private string zipOrPostalCode;
        private IList<string> postOfficeBoxs;
        private string webPage;
        private IList<string> otherWebPages;
        private IList<ActiveDirectoryObject> directReportObjects;

        /// <summary>
        /// The cn attribute.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.CN)]
        public string CN
        {
            get
            {
                if (String.IsNullOrEmpty(this.cn))
                {
                    this.cn = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.CN).Value;
                }
                return this.cn;
            }
            set
            {
                this.DirectoryEntry.Rename(String.Format(@"{0}={1}", AttributeNames.CN, value));
            }
        }

        /// <summary>
        /// The object guid attribute.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.ObjectGuid)]
        public Guid ObjectGuid
        {
            get
            {
                if (this.objectGuid == Guid.Empty)
                {
                    this.objectGuid = new GuidAdapter(this.DirectoryEntry, AttributeNames.ObjectGuid).Value;
                }
                return this.objectGuid;
            }
        }

        /// <summary>
        /// The distinguished name attribute.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.DistinguishedName)]
        public string DistinguishedName
        {
            get
            {
                if (String.IsNullOrEmpty(this.distinguishedName))
                {
                    this.distinguishedName =
                        new SingleLineAdapter(this.DirectoryEntry, AttributeNames.DistinguishedName).Value;
                }
                return this.distinguishedName;
            }
        }

        /// <summary>
        /// The full name.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.Name)]
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(this.name))
                {
                    this.name = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.Name).Value;
                }
                return this.name;
            }
        }

        /// <summary>
        /// The canonical name.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.CanonicalName)]
        public string CanonicalName
        {
            get
            {
                if (String.IsNullOrEmpty(this.canonicalName))
                {
                    this.canonicalName = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.CanonicalName).Value;
                }
                return this.canonicalName;
            }
        }

        /// <summary>
        /// The create time.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.CreateTimeStamp)]
        public DateTime CreateTime
        {
            get
            {
                if (this.createTime == DateTime.MinValue)
                {
                    this.createTime = new DateTimeAdapter(this.DirectoryEntry, AttributeNames.CreateTimeStamp).Value;
                }
                return this.createTime;
            }
        }

        /// <summary>
        /// The modify time.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.ModifyTimeStamp)]
        public DateTime ModifyTime
        {
            get
            {
                if (this.modifyTime == DateTime.MinValue)
                {
                    this.modifyTime = new DateTimeAdapter(this.DirectoryEntry, AttributeNames.ModifyTimeStamp).Value;
                }
                return this.modifyTime;
            }
        }

        /// <summary>
        /// The description.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.Description)]
        public string Description
        {
            get
            {
                if (String.IsNullOrEmpty(this.description))
                {
                    this.description =
                        new SingleLineAdapter(this.DirectoryEntry, AttributeNames.Description).Value;
                }
                return this.description;
            }
            set
            {
                this.DirectoryEntry.Properties[AttributeNames.Description].Clear();
                if (!String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[AttributeNames.Description].Add(value);
                }
                this.description = value;
            }
        }

        /// <summary>
        /// The direct reports.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.DirectReports)]
        public IList<string> DirectReports
        {
            get
            {
                if (this.directReports == null)
                {
                    this.directReports = new MultipleLineAdapter(this.DirectoryEntry, AttributeNames.DirectReports).Value;
                }
                return this.directReports;
            }
        }

        /// <summary>
        /// The display name.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.DisplayName)]
        public string DisplayName
        {
            get
            {
                if (String.IsNullOrEmpty(this.displayName))
                {
                    this.displayName = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.DisplayName).Value;
                }
                return this.displayName;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[AttributeNames.DisplayName].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[AttributeNames.DisplayName].Value = value;
                }
                this.displayName = value;
            }
        }

        /// <summary>
        /// The Logon name for 2000(eg: [DomainName]\[UserName]).
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.MsDS_PrincipalName)]
        public string MsDS_PrincipalName
        {
            get
            {
                if (String.IsNullOrEmpty(this.msDS_PrincipalName))
                {
                    this.msDS_PrincipalName = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.MsDS_PrincipalName).Value;
                }
                return this.msDS_PrincipalName;
            }
        }

        /// <summary>
        /// The office.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.PhysicalDeliveryOfficeName)]
        public string Office
        {
            get
            {
                if (String.IsNullOrEmpty(this.office))
                {
                    this.office = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.PhysicalDeliveryOfficeName).Value;
                }
                return this.office;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[AttributeNames.PhysicalDeliveryOfficeName].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[AttributeNames.PhysicalDeliveryOfficeName].Value = value;
                }
                this.office = value;
            }
        }

        /// <summary>
        /// The zip or postal code.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.PostalCode)]
        public string ZipOrPostalCode
        {
            get
            {
                if (String.IsNullOrEmpty(this.zipOrPostalCode))
                {
                    this.zipOrPostalCode = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.PostalCode).Value;
                }
                return this.zipOrPostalCode;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[AttributeNames.PostalCode].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[AttributeNames.PostalCode].Value = value;
                }
                this.zipOrPostalCode = value;
            }
        }

        /// <summary>
        /// The P.O.Box.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.PostOfficeBox)]
        public IList<string> PostOfficeBoxs
        {
            get
            {
                if (this.postOfficeBoxs == null)
                {
                    this.postOfficeBoxs = new MultipleLineAdapter(this.DirectoryEntry, AttributeNames.PostOfficeBox).Value;
                }
                return this.postOfficeBoxs;
            }
            set
            {
                SetMultipleAttributeValue(AttributeNames.PostOfficeBox, value);
                this.postOfficeBoxs = value;
            }
        }

        /// <summary>
        /// The web page.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.WWWHomePage)]
        public string WebPage
        {
            get
            {
                if (String.IsNullOrEmpty(this.webPage))
                {
                    this.webPage = new SingleLineAdapter(this.DirectoryEntry, AttributeNames.WWWHomePage).Value;
                }
                return this.webPage;
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    this.DirectoryEntry.Properties[AttributeNames.WWWHomePage].Clear();
                }
                else
                {
                    this.DirectoryEntry.Properties[AttributeNames.WWWHomePage].Value = value;
                }
                this.webPage = value;
            }
        }

        /// <summary>
        /// The other web pages.
        /// </summary>
        [ADOriginalAttributeName(AttributeNames.Url)]
        public IList<string> OtherWebPages
        {
            get
            {
                if (this.otherWebPages == null)
                {
                    this.otherWebPages = new MultipleLineAdapter(this.DirectoryEntry, AttributeNames.Url).Value;
                }
                return this.otherWebPages;
            }
            set
            {
                SetMultipleAttributeValue(AttributeNames.Url, value);
                this.otherWebPages = value;
            }
        }

        #endregion

        protected ActiveDirectoryObject(DirectoryEntry directoryEntry)
        {
            this.DirectoryEntry = directoryEntry;
        }

        /// <summary>
        /// Set the attribute value.
        /// </summary>
        /// <typeparam name="TAttributeValue">The attribute value generic type.</typeparam>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="attributeValue">The attribute value.</param>
        protected void SetMultipleAttributeValue<TAttributeValue>(string attributeName, TAttributeValue attributeValue)
        {
            if (typeof(TAttributeValue) == typeof(IList<string>) || typeof(TAttributeValue) == typeof(List<string>))
            {
                var attributeValueItems = attributeValue as IList<string>;
                if (attributeValueItems != null)
                {
                    this.DirectoryEntry.Properties[attributeName].Clear();
                    foreach (var attributeValueItem in attributeValueItems)
                    {
                        this.DirectoryEntry.Properties[attributeName].Add(attributeValueItem);
                    }
                }
            }
            else
            {
                this.DirectoryEntry.Properties[attributeName].Value = attributeValue;
            }
        }

        /// <summary>
        /// Save the directory entry.
        /// </summary>
        public void Save()
        {
            this.DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// Delete the directory entry.
        /// </summary>
        public void Delete()
        {
            this.DirectoryEntry.DeleteTree();
        }

        /// <summary>
        /// Clear the attribute value.
        /// </summary>
        /// <param name="attributename">The attribute name.</param>
        public void ClearAttributeValue(string attributename)
        {
            this.DirectoryEntry.Properties[attributename].Clear();
        }

        /// <summary>
        /// Move to new OU.
        /// </summary>
        /// <param name="orgnizationalUnit">The new OU.</param>
        public void MoveTo(OrganizationalUnit orgnizationalUnit)
        {
            this.DirectoryEntry.MoveTo(orgnizationalUnit.DirectoryEntry);
            orgnizationalUnit.DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// Copy to new Container.
        /// </summary>
        /// <param name="orgnizationalUnit">The new OU.</param>
        public void CopyTo(Container container)
        {
            this.DirectoryEntry.CopyTo(container.DirectoryEntry);
            container.DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// Copy to new OU.
        /// </summary>
        /// <param name="orgnizationalUnit">The new OU.</param>
        public void CopyTo(OrganizationalUnit orgnizationalUnit)
        {
            this.DirectoryEntry.CopyTo(orgnizationalUnit.DirectoryEntry);
            orgnizationalUnit.DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// Move to new Container.
        /// </summary>
        /// <param name="orgnizationalUnit">The new OU.</param>
        public void MoveTo(Container container)
        {
            this.DirectoryEntry.MoveTo(container.DirectoryEntry);
            container.DirectoryEntry.CommitChanges();
        }

        /// <summary>
        /// Find one AD object by objectGUID.
        /// </summary>
        /// <param name="objectGuid">The objectGUID.</param>
        /// <returns>One AD object.</returns>
        public static ActiveDirectoryObject FindOneByObjectGUID(Guid objectGuid)
        {
            return ActiveDirectoryQuery.FindOneByGuid<ActiveDirectoryObject>(objectGuid);
        }

        /// <summary>
        /// Fine one AD object by distinguished name.
        /// </summary>
        /// <param name="distinguishedName">The distinguished name.</param>
        /// <returns>One AD object.</returns>
        public static ActiveDirectoryObject FindOneByDN(string distinguishedName)
        {
            return FindOne(new Is(AttributeNames.DistinguishedName, distinguishedName));
        }

        /// <summary>
        /// Find all ad directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All ad directory entry by filter.</returns>
        public static IList<ActiveDirectoryObject> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<ActiveDirectoryObject>(filter);
        }

        /// <summary>
        /// Find one ad directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All ad directory entry by filter.</returns>
        public static ActiveDirectoryObject FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<ActiveDirectoryObject>(filter);
        }

        public void Dispose()
        {
            this.DirectoryEntry.Dispose();
        }
    }
}
