using Cgpe.Du.Domain.Entities;
using System;

namespace Cgpe.Du.Domain
{

    public interface IContactRepository
    {

        void Create(Contact contact);

        Contact Read(Guid contactId);

        void Update(Contact contact);

        void Delete(Guid contactId);

    }

}
