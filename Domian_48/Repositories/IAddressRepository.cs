using Cgpe.Du.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Cgpe.Du.Domain
{

    public interface IAddressRepository
    {

        void Create(Address address);

        Address Read(string addressId);

        void Update(Address address);

        void Delete(string addressId);

        List<Address> GetAddressesWithExecutingSituationAndMagazine();
    }

}
