using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class AssociationEfMap
    {

        public void Map(AssociationEntity source, Association target)
        {
            if (target.AssociationProcurators == null)
                target.AssociationProcurators = new List<AssociationProcurator>();
            if (target.Contacts == null)
                target.Contacts = new List<Contact>();
            if (target.Users == null)
                target.Users = new List<DirectoryUser>();
            target.AssociationCode = source.AssociationCode;
            target.AssociationId = source.AssociationId;
            target.Cif = source.Cif;
            target.Name = source.Name;

            if(source.HeadquartersAddress != null)
            {
                target.HeadquartersAddress = new Address();
                new AddressEfMap().Map(source.HeadquartersAddress, target.HeadquartersAddress);
            }

            if(source.Contacts != null && source.Contacts.Count > 0)
            {
                Contact cont;
                foreach (ContactEntity contEntity in source.Contacts)
                {
                    cont = new Contact();
                    new ContactEfMap().Map(contEntity, cont);
                    target.Contacts.Add(cont);
                }
            }
        }

        public void Map(Association source, AssociationEntity target)
        {
            //if (target.Addresses == null)
            //    target.Addresses = new List<AddressEntity>();
            if (target.AssociationsProcurators == null)
                target.AssociationsProcurators = new List<AssociationProcuratorEntity>();
            if (target.Contacts == null)
                target.Contacts = new List<ContactEntity>();
            if (target.Users == null)
                target.Users = new List<DirectoryUserEntity>();
            target.AssociationCode = source.AssociationCode;
            //target.AssociationId = source.AssociationId;
            target.Cif = source.Cif;
            target.Name = source.Name;

            // Esto igual hay que hacerlo fuera.
            //if (source.HeadquartersAddress != null)
            //{
            //    target.HeadquartersAddress = new AddressEntity();
            //    new AddressEfMap().Map(source.HeadquartersAddress, target.HeadquartersAddress);
            //}
        }

    }

}
