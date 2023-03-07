using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Cgpe.Du.Infrastructure
{

    public class MasterRepository : IMasterRepository
    {

        private DuUnitOfWork uow;

        public MasterRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public List<City> GetCities(Guid provinceId)
        {
            List<CityEntity> entities = uow.DbContext.Cities.Where(c => c.ProvinceId.Equals(provinceId)).ToList();
            List<City> items = new List<City>();
            City city;
            CityEfMap map = new CityEfMap();
            foreach (CityEntity entity in entities)
            {
                city = new City();
                map.Map(entity, city);
                items.Add(city);
            }
            return items.OrderBy(c => c.CityName).ToList();
        }

        public List<OrganizationType> GetOrganizationTypes()
        {
            List<OrganizationTypeEntity> entities = uow.DbContext.OrganizationTypes.ToList();
            List<OrganizationType> items = new List<OrganizationType>();
            OrganizationType organizationType;
            OrganizationTypeEfMap map = new OrganizationTypeEfMap();
            foreach (OrganizationTypeEntity entity in entities)
            {
                organizationType = new OrganizationType();
                map.Map(entity, organizationType);
                items.Add(organizationType);
            }
            return items.OrderBy(ot => ot.OrganizationTypeName).ToList();
        }

        public List<Organization> GetOrganizations(Guid? organizationTypeId, bool includeInactive = false)
        {
            List<OrganizationEntity> entities = uow.DbContext.Organizations
                .Where(ot => (organizationTypeId == null || ot.OrganizationTypeId == organizationTypeId) 
                && (includeInactive || ot.IsActive )).ToList();
            List<Organization> items = new List<Organization>();
            Organization organization;
            OrganizationEfMap map = new OrganizationEfMap();
            foreach (OrganizationEntity entity in entities)
            {
                organization = new Organization();
                map.Map(entity, organization);
                items.Add(organization);
            }
            return items.OrderBy(o => o.OrganizationName).ToList();
        }

        public List<Organ> GetOrgans(Guid? organizationId, bool includeInactive = false)
        {
            List<OrganEntity> entities = uow.DbContext.Organs
                .Where(o => (organizationId == null || o.OrganizationId == organizationId)
                && (includeInactive || o.IsActive)).ToList();
            List<Organ> items = new List<Organ>();
            Organ organ;
            OrganEfMap map = new OrganEfMap();
            foreach (OrganEntity entity in entities)
            {
                organ = new Organ();
                map.Map(entity, organ);
                items.Add(organ);
            }
            return items.OrderBy(o => o.OrganName).ToList();
        }

        public List<PositionType> GetPositionTypes()
        {
            List<PositionTypeEntity> entities = uow.DbContext.PositionTypes.ToList();
            List<PositionType> items = new List<PositionType>();
            PositionType positionType;
            PositionTypeEfMap map = new PositionTypeEfMap();
            foreach (PositionTypeEntity entity in entities)
            {
                positionType = new PositionType();
                map.Map(entity, positionType);
                items.Add(positionType);
            }
            return items.OrderBy(pt => pt.TypeName).ToList();
        }

        public List<Language> GetLanguages()
        {
            List<LanguageEntity> entities = uow.DbContext.Languages.ToList();
            List<Language> items = new List<Language>();
            Language language;
            LanguageEfMap map = new LanguageEfMap();
            foreach (LanguageEntity entity in entities)
            {
                language = new Language();
                map.Map(entity, language);
                items.Add(language);
            }
            return items.OrderBy(l => l.LanguageName).ToList();
        }

        public List<ProcuratorSituation> GetProcuratorSituations()
        {
            List<ProcuratorSituationEntity> entities = uow.DbContext.ProcuratorSituations.ToList();
            List<ProcuratorSituation> items = new List<ProcuratorSituation>();
            ProcuratorSituation situation;
            ProcuratorSituationEfMap map = new ProcuratorSituationEfMap();
            foreach (ProcuratorSituationEntity entity in entities)
            {
                situation = new ProcuratorSituation();
                map.Map(entity, situation);
                items.Add(situation);
            }
            return items.OrderBy(s => s.ProcuratorSituationName).ToList();
        }

        public List<Agreement> GetAgreements()
        {
            List<AgreementEntity> entities = uow.DbContext.Agreements.ToList();
            List<Agreement> items = new List<Agreement>();
            Agreement agreement;
            AgreementEfMap map = new AgreementEfMap();
            foreach (AgreementEntity entity in entities)
            {
                agreement = new Agreement();
                map.Map(entity, agreement);
                items.Add(agreement);
            }
            return items.OrderBy(a => a.AgreementName).ToList();
        }

        public List<Sex> GetSexes()
        {
            List<SexEntity> entities = uow.DbContext.Sexes.ToList();
            List<Sex> items = new List<Sex>();
            Sex sex;
            SexEfMap map = new SexEfMap();
            foreach (SexEntity entity in entities)
            {
                sex = new Sex();
                map.Map(entity, sex);
                items.Add(sex);
            }
            return items.OrderBy(s => s.SexName).ToList();
        }



        public List<ContactType> GetContactTypes()
        {
            List<ContactTypeEntity> entities = uow.DbContext.ContactTypes.ToList();
            List<ContactType> items = new List<ContactType>();
            ContactType contactType;
            ContactTypeEfMap map = new ContactTypeEfMap();
            foreach (ContactTypeEntity entity in entities)
            {
                contactType = new ContactType();
                map.Map(entity, contactType);
                items.Add(contactType);
            }
            return items.OrderBy(ct => ct.TypeName).ToList();
        }

        public List<AddressType> GetAddressTypes()
        {
            List<AddressTypeEntity> entities = uow.DbContext.AddressTypes.ToList();
            List<AddressType> items = new List<AddressType>();
            AddressType addressType;
            AddressTypeEfMap map = new AddressTypeEfMap();
            foreach (AddressTypeEntity entity in entities)
            {
                addressType = new AddressType();
                map.Map(entity, addressType);
                items.Add(addressType);
            }
            return items.OrderBy(at => at.TypeName).ToList();
        }

        public List<Province> GetProvinces()
        {
            List<ProvinceEntity> entities = uow.DbContext.Provinces.ToList();
            List<Province> items = new List<Province>();
            Province province;
            ProvinceEfMap map = new ProvinceEfMap();
            foreach (ProvinceEntity entity in entities)
            {
                province = new Province();
                map.Map(entity, province);
                items.Add(province);
            }
            return items.OrderBy(p => p.ProvinceName).ToList();
        }

        public List<HonourType> GetHonourTypes()
        {
            List<HonourTypeEntity> entities = uow.DbContext.HonourTypes.ToList();
            List<HonourType> items = new List<HonourType>();
            HonourType distinctionType;
            HonourTypeEfMap map = new HonourTypeEfMap();
            foreach (HonourTypeEntity entity in entities)
            {
                distinctionType = new HonourType();
                map.Map(entity, distinctionType);
                items.Add(distinctionType);
            }
            return items.OrderBy(ht => ht.TypeName).ToList();
        }

        public List<WayType> GetWayTypes()
        {
            List<WayTypeEntity> entities = uow.DbContext.WayTypes.ToList();
            List<WayType> items = new List<WayType>();
            WayType wayType;
            WayTypeEfMap map = new WayTypeEfMap();
            foreach (WayTypeEntity entity in entities)
            {
               wayType = new WayType();
                map.Map(entity, wayType);
                items.Add(wayType);
            }
            return items.OrderBy(wt => wt.TypeName).ToList();
        }

        public List<Association> GetAssociations()
        {
         
            List<AssociationEntity> entities = uow.DbContext.Associations.ToList();
            List<Association> items = new List<Association>();
            Association association;
            AssociationEfMap map = new AssociationEfMap();
            foreach (AssociationEntity entity in entities)
            {
                association = new Association();
                map.Map(entity, association);
                items.Add(association);
            }
            return items.OrderBy(a => a.Name).ToList();
        }

        public List<DirectoryRole> GetRoles()
        {
            // No incluyo las claims, no hacen falta.
            List<DirectoryRoleEntity> entities = uow.DbContext.DirectoryRoles.ToList();
            List<DirectoryRole> items = new List<DirectoryRole>();
            DirectoryRole role;
            DirectoryRoleEfMap map = new DirectoryRoleEfMap();
            foreach (DirectoryRoleEntity entity in entities)
            {
                role = new DirectoryRole();
                map.Map(entity, role);
                items.Add(role);
            }
            return items.OrderBy(a => a.RoleName).ToList();
        }



    }

}
