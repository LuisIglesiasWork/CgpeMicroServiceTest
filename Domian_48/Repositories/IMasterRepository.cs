using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Domain
{

    public interface IMasterRepository
    {
        List<City> GetCities(string provinceId);
        List<Province> GetProvinces();
        List<ContactType> GetContactTypes();
        List<AddressType> GetAddressTypes();
        List<HonourType> GetHonourTypes();
        List<WayType> GetWayTypes();
        List<OrganizationType> GetOrganizationTypes();
        List<Organization> GetOrganizations(string organizationTypeId, bool includeInactive = false);
        List<Organ> GetOrgans(string organizationId, bool includeInactive = false);
        List<PositionType> GetPositionTypes();
        List<Language> GetLanguages();
        List<ProcuratorSituation> GetProcuratorSituations();
        List<Agreement> GetAgreements();
        List<Sex> GetSexes();
        List<DirectoryRole> GetRoles();
    }

}
