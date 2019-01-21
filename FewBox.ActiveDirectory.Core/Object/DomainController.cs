using FewBox.ActiveDirectory.Core.Attribute;
using FewBox.ActiveDirectory.Core.Attribute.ValueAdapter;
using FewBox.ActiveDirectory.Core.Query;
using FewBox.ActiveDirectory.Core.Query.Filter;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace FewBox.ActiveDirectory.Core.Object
{
    public class DomainController : Pack
    {
        #region AD Attributes

        private int groupPolicyMinimumPasswordLength;
        private bool? isMustMeetComplexityRequirments;

        /// <summary>
        /// The group policy minimum password length.
        /// </summary>
        [ADOriginalAttributeName(DomainControllerAttributeNames.MinPwdLength)]
        public int GroupPolicyMinimumPasswordLength
        {
            get
            {
                if (this.groupPolicyMinimumPasswordLength == -1)
                {
                    this.groupPolicyMinimumPasswordLength = new IntegerAdapter(this.DirectoryEntry, DomainControllerAttributeNames.MinPwdLength).Value;
                }
                return this.groupPolicyMinimumPasswordLength;
            }
            set
            {
                this.DirectoryEntry.Properties[DomainControllerAttributeNames.MinPwdLength].Value = value;
                this.groupPolicyMinimumPasswordLength = value;
            }
        }

        /// <summary>
        /// Is must meet complexity requirements policy.
        /// </summary>
        [ADOriginalAttributeName(DomainControllerAttributeNames.PwdProperties)]
        public bool IsMustMeetComplexityRequirments
        {
            get
            {
                if (isMustMeetComplexityRequirments == null)
                {
                    isMustMeetComplexityRequirments = (new IntegerAdapter(this.DirectoryEntry, DomainControllerAttributeNames.PwdProperties).Value % 2 == 1);
                }
                return (bool)(isMustMeetComplexityRequirments);
            }
            set
            {
                this.DirectoryEntry.Properties[DomainControllerAttributeNames.PwdProperties].Value = value;
                this.isMustMeetComplexityRequirments = value;
            }
        }

        #endregion

        internal DomainController(DirectoryEntry directoryEntry) :
            base(directoryEntry)
        {
        }

        /// <summary>
        /// Find one domain object.
        /// </summary>
        /// <returns>One domain object.</returns>
        public static DomainController FindOne()
        {
            return ActiveDirectoryQuery.FindOneByFilter<DomainController>(new IsDomainController());
        }

        /// <summary>
        /// Find all domain controller directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All domain controller directory entry by filter.</returns>
        public static new IList<DomainController> FindAll(IFilter filter)
        {
            return ActiveDirectoryQuery.FindAllByFilter<DomainController>(new And(new IsDomainController(), filter));
        }

        /// <summary>
        /// Find one domain controller directory entry by filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>All domain controller directory entry by filter.</returns>
        public static new DomainController FindOne(IFilter filter)
        {
            return ActiveDirectoryQuery.FindOneByFilter<DomainController>(new And(new IsDomainController(), filter));
        }

        /// <summary>
        /// Get the current domain object with no password.
        /// </summary>
        /// <returns></returns>
        public static DomainController GetCurrent()
        {
            DomainController domainController;
            var directoryContext = new DirectoryContext(DirectoryContextType.Domain);
            using (Domain domain = Domain.GetDomain(directoryContext))
            {
                using (DirectoryEntry domainDirectoryEntry = domain.GetDirectoryEntry())
                {
                    domainController = new DomainController(domainDirectoryEntry);
                }
            }
            return domainController;
        }
    }
}
