using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    public class AddressEfMap
    {

        public void Map(AddressEntity source, Address target)
        {
            if (target.Contacts == null)
            {
                target.Contacts = new List<Contact>();
            }


            target.AddressId = source.AddressId;
            target.FullAddress = source.FullAddress;
            target.IsPublic = source.IsPublic;
            target.IsReceivingMagazine = source.IsReceivingMagazine;
            target.MailBox = source.MailBox;
            target.WayName = source.WayName;
            target.Door = source.Door;
            target.Floor = source.Floor;
            target.BuildingName = source.BuildingName;
            target.Stairway = source.Stairway;
            target.WayNumber = source.WayNumber;
            target.ZipCode = source.ZipCode;


            if (source.AddressType != null)
            {
                AddressTypeEfMap addressTypeMapper = new AddressTypeEfMap();
                target.AddressType = new AddressType();
                addressTypeMapper.Map(source.AddressType, target.AddressType);
            }
            if (source.Province != null)
            {
                ProvinceEfMap provinceMapper = new ProvinceEfMap();
                target.Province = new Province();
                provinceMapper.Map(source.Province, target.Province);
            }
            if (source.City != null)
            {
                CityEfMap cityMapper = new CityEfMap();
                target.City = new City();
                cityMapper.Map(source.City, target.City);
            }
            if (source.WayType != null)
            {
                WayTypeEfMap wayTypeMapper = new WayTypeEfMap();
                target.WayType = new WayType();
                wayTypeMapper.Map(source.WayType, target.WayType);
            }
            if (source.Contacts != null && source.Contacts.Count > 0)
            {
                ContactEfMap contactMapper = new ContactEfMap();
                Contact cont;
                foreach (ContactEntity contEntity in source.Contacts)
                {
                    cont = new Contact();
                    contactMapper.Map(contEntity, cont);
                    target.Contacts.Add(cont);
                }
            }
        }

        public void Map(Address source, AddressEntity target, Guid? associationProcuratorId, bool isNew = false)
        {
            if (target.Contacts == null)
            {
                // Las colecciones las mapeo desde el repositorio.
                target.Contacts = new List<ContactEntity>();
            }

            if (isNew)
            {
                source.AddressId = Guid.NewGuid();
            }

            target.AddressId = source.AddressId;

            target.AssociationProcuratorId = associationProcuratorId;

            target.FullAddress = source.FullAddress;
            target.IsPublic = source.IsPublic;
            target.IsReceivingMagazine = source.IsReceivingMagazine;
            target.MailBox = source.MailBox;
            target.WayName = source.WayName;
            target.Door = source.Door;
            target.BuildingName = source.BuildingName;
            target.Floor = source.Floor;
            target.Stairway = source.Stairway;
            target.WayNumber = source.WayNumber;
            target.ZipCode = source.ZipCode;
            if (source.AddressType != null)
            {
                target.AddressTypeId = source.AddressType.TypeId;
            }
            if (source.Province != null)
            {
                target.ProvinceId = source.Province.ProvinceId;
            }
            if (source.City != null)
            {
                target.CityId = source.City.CityId;
            }
            if (source.WayType != null)
            {
                target.WayTypeId = source.WayType.TypeId;
            }
        }

    }
}
