using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;

namespace Cgpe.Du.Domain
{

    public class LocationDomainService
    {

        #region Fields & Properties

        private IUnitOfWork uow;
        private IAddressRepository addressRepository;
        private IContactRepository contactRepository;

        #endregion

        #region Construction & Desctruction

        public LocationDomainService(IUnitOfWork uow, IAddressRepository addressRepository, IContactRepository contactRepository)
        {
            this.uow = uow;
            this.addressRepository = addressRepository;
            this.contactRepository = contactRepository;
        }

        #endregion

        public void CreateAddress(Address address)
        {
            address.Validate();
            this.addressRepository.Create(address);
        }

        public Address ReadAddress(Guid addressId)
        {
            return this.addressRepository.Read(addressId);
        }

        public void UpdateAddress(Address address)
        {
            address.Validate();
            this.addressRepository.Update(address);
        }

        public void DeleteAddress(Guid addressId)
        {
            this.addressRepository.Delete(addressId);
        }

        public void CreateContact(Contact contact)
        {
            contact.Validate();
            this.contactRepository.Create(contact);
        }

        public Contact ReadContact(Guid contactId)
        {
            return this.contactRepository.Read(contactId);
        }

        public void UpdateContact(Contact contact)
        {
            contact.Validate();
            this.contactRepository.Update(contact);
        }

        public void DeleteContact(Guid contactId)
        {
            this.contactRepository.Delete(contactId);
        }

        public List<Address> GetAddressesWithExecutingSituationAndMagazineAddresses()
        {
            return this.addressRepository.GetAddressesWithExecutingSituationAndMagazine();
        }

    }

}
