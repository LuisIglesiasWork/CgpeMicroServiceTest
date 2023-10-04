using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class MasterDomainService
    {

        #region Fields & Properties

        private IUnitOfWork uow;
        private IMasterRepository masterRepository;

        #endregion

        #region Construction & Destruction

        public MasterDomainService(IUnitOfWork uow, IMasterRepository masterRepository)
        {
            this.uow = uow;
            this.masterRepository = masterRepository;
        }

        #endregion

        #region MasterData

        public List<City> GetCities(string provinceId)
        {
            string provinceIdGuid = Guid.Parse(provinceId).ToString();
            return this.masterRepository.GetCities(provinceIdGuid);
        }

        public List<ContactType> GetContactTypes()
        {
            return this.masterRepository.GetContactTypes();
        }

        public List<AddressType> GetAddressTypes()
        {
            return this.masterRepository.GetAddressTypes();
        }

        public List<Province> GetProvinces()
        {
            return this.masterRepository.GetProvinces();
        }

        public List<HonourType> GetHonourTypes()
        {
            return this.masterRepository.GetHonourTypes();
        }

        public List<WayType> GetWayTypes()
        {
            return this.masterRepository.GetWayTypes();
        }

        public List<OrganizationType> GetOrganizationTypes()
        {
            return this.masterRepository.GetOrganizationTypes();
        }

        public List<Organization> GetOrganizations(string organizationTypeId)
        {
            string id = null;
            if (!string.IsNullOrWhiteSpace(organizationTypeId))
            {
                id = Guid.Parse(organizationTypeId).ToString();
            }
            return this.masterRepository.GetOrganizations(id);
        }

        public List<Organ> GetOrgans(string organizationId)
        {
            string id = null;
            if (!string.IsNullOrWhiteSpace(organizationId))
            {
                id = Guid.Parse(organizationId).ToString();
            }
            return this.masterRepository.GetOrgans(id);
        }

        public List<PositionType> GetPositionTypes()
        {
            return this.masterRepository.GetPositionTypes();
        }

        public List<Language> GetLanguages()
        {
            return this.masterRepository.GetLanguages();
        }

        public List<ProcuratorSituation> GetProcuratorSituations()
        {
            return this.masterRepository.GetProcuratorSituations();
        }

        public List<Sex> GetSexes()
        {
            return this.masterRepository.GetSexes();
        }

        public List<Agreement> GetAgreements()
        {
            return this.masterRepository.GetAgreements();
        }

        public List<DirectoryRole> GetRoles()
        {
            return this.masterRepository.GetRoles();
        }

        #endregion

    }

}
