using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Authentication;
using FewBox.ActiveDirectory.Core.Object;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace FewBox.ActiveDirectory.Core.Query
{
    public static class ActiveDirectoryQuery
    {
        /// <summary>
        /// Find one AD object by filter.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>One AD object.</returns>
        public static TADObject FindOneByFilter<TADObject>(IFilter filter, string[] propertiesToLoad = null, QueryScopeType queryScopeType = QueryScopeType.Subtree) where TADObject : ActiveDirectoryObject
        {
            TADObject adObject = null;
            SearchResult searchResult;
            using (DirectorySearcher directorySearcher = new DirectorySearcher(ClientContext.GetContext().RootDirectoryEntry, filter.BuildFilter(), propertiesToLoad, GetSearchScope(queryScopeType)))
            {
                searchResult = directorySearcher.FindOne();
                if (searchResult != null)
                {
                    adObject = GetActiveDirectoryObject(searchResult) as TADObject;
                }
            }
            return adObject;
        }

        /// <summary>
        /// Find all AD objects by filter.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>All AD objects.</returns>
        public static List<TADObject> FindAllByFilter<TADObject>(IFilter filter, string[] propertiesToLoad = null, QueryScopeType queryScopeType = QueryScopeType.Subtree)
        {
            List<TADObject> adObjects;
            using (var searchResultCollection = GetSearchResultCollection(filter.BuildFilter(), propertiesToLoad, GetSearchScope(queryScopeType)))
            {
                var objects = (from SearchResult searchResult in searchResultCollection
                               select GetActiveDirectoryObject(searchResult)).ToList();
                adObjects = objects.Cast<TADObject>().ToList();
            }
            return adObjects;
        }

        public static TADObject FindOneByGuid<TADObject>(Guid objectGuid) where TADObject : ActiveDirectoryObject
        {
            TADObject adObject;
            SearchResult searchResult;
            string path = String.Format(@"{0}/<GUID={1}>", ClientContext.GetContext().Path, objectGuid);
            try
            {
                using (DirectoryEntry directoryEntry = new DirectoryEntry(path, ClientContext.GetContext().Username, ClientContext.GetContext().Password))
                {
                    using (DirectorySearcher directorySearcher = new DirectorySearcher(directoryEntry))
                    {
                        searchResult = directorySearcher.FindOne();
                        adObject = GetActiveDirectoryObject(searchResult) as TADObject;
                    }
                }
            }
            catch{
                adObject = default(TADObject);
            }
            return adObject;
        }

        private static SearchResultCollection GetSearchResultCollection(string filterString, string[] propertiesToLoad, SearchScope searchSope)
        {
            SearchResultCollection searchResultCollection;
            using (DirectorySearcher directorySearcher = new DirectorySearcher(ClientContext.GetContext().RootDirectoryEntry, filterString, propertiesToLoad, searchSope))
            {
                directorySearcher.PageSize = Int32.MaxValue;
                directorySearcher.SizeLimit = Int32.MaxValue;
                searchResultCollection = directorySearcher.FindAll();
            }
            return searchResultCollection;
        }

        private static SearchScope GetSearchScope(QueryScopeType queryScopeType)
        {
            SearchScope searchScope = SearchScope.Base;
            if (queryScopeType == QueryScopeType.OneLevel)
            {
                searchScope = SearchScope.OneLevel;
            }
            else if (queryScopeType == QueryScopeType.Subtree)
            {
                searchScope = SearchScope.Subtree;
            }
            return searchScope;
        }

        private static ActiveDirectoryObject GetActiveDirectoryObject(SearchResult searchResult)
        {
            ActiveDirectoryObject activeDirectoryObject;
            ActiveDirectoryObjectType activeDirectoryObjectType = GetActiveDirectoryObjectType(searchResult);
            switch (activeDirectoryObjectType)
            {
                case ActiveDirectoryObjectType.User:
                    activeDirectoryObject = new User(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.Contact:
                    activeDirectoryObject = new Contact(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.Computer:
                    activeDirectoryObject = new Computer(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.Container:
                    activeDirectoryObject = new Container(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.Group:
                    activeDirectoryObject = new Group(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.InetOrgPerson:
                    activeDirectoryObject = new InetOrgPerson(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.MSMQQueueAlias:
                    activeDirectoryObject = new MSMQQueueAlias(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.MsImaging_PSPs:
                    activeDirectoryObject = new MsImaging_PSPs(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.OrganizationalUnit:
                    activeDirectoryObject = new OrganizationalUnit(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.Printer:
                    activeDirectoryObject = new Printer(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.SharedFolder:
                    activeDirectoryObject = new SharedFolder(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.DomainController:
                    activeDirectoryObject = new DomainController(searchResult.GetDirectoryEntry());
                    break;
                case ActiveDirectoryObjectType.PasswordSettings:
                    activeDirectoryObject = new PasswordSettings(searchResult.GetDirectoryEntry());
                    break;
                default:
                    activeDirectoryObject = new UnknownObject(searchResult.GetDirectoryEntry());
                    break;
            }
            return activeDirectoryObject;
        }

        internal static ActiveDirectoryObjectType GetActiveDirectoryObjectType(SearchResult searchResult)
        {
            ActiveDirectoryObjectType activeDirectoryObjectType = ActiveDirectoryObjectType.Unknow;
            if (searchResult != null)
            {
                var resultPropertyValueCollection = searchResult.Properties[AttributeNames.ObjectClass];
                for (int index = 0; index < resultPropertyValueCollection.Count; index++)
                {
                    switch (resultPropertyValueCollection[index].ToString())
                    {
                        case UserAttributeValues.User:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.User;
                            break;
                        case ContactAttributeValues.Contact:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.Contact;
                            break;
                        case ComputerAttributeValues.Computer:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.Computer;
                            break;
                        case ContainerAttributeValues.Container:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.Container;
                            break;
                        case GroupAttributeValues.Group:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.Group;
                            break;
                        case InetOrgPersonAttributeValues.InetOrgPerson:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.InetOrgPerson;
                            break;
                        case MSMQQueueAliasAttributeValues.MSMQQueueAlias:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.MSMQQueueAlias;
                            break;
                        case MsImaging_PSPsAttributeValues.MsImaging_PSPs:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.MsImaging_PSPs;
                            break;
                        case OrganizationalUnitAttributeValues.OrganizationalUnit:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.OrganizationalUnit;
                            break;
                        case PrinterAttributeValues.Printer:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.Printer;
                            break;
                        case SharedFolderAttributeValues.SharedFolder:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.SharedFolder;
                            break;
                        case DomainControllerAttributeValues.Domain:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.DomainController;
                            break;
                        case PasswordSettingsAttributeValues.MsDS_PasswordSettings:
                            activeDirectoryObjectType = ActiveDirectoryObjectType.PasswordSettings;
                            break;
                        default:
                            break;
                    }
                }
            }
            return activeDirectoryObjectType;
        }
    }
}
