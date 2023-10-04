using Cgpe.Du.Domain.Entities;
using System;

namespace Cgpe.Du.Domain
{

    public interface IContactRepository
    {

        void Create(Contact contact);

        Contact Read(string contactId);

        void Update(Contact contact);

        void Delete(string contactId);

    }

}
