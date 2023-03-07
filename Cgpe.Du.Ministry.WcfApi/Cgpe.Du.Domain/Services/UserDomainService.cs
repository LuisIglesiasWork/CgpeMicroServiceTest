using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;

namespace Cgpe.Du.Domain.Services
{
    public class UserDomainService
    {
        private IDirectoryUserRepository directoryUserRepository;
        private IUnitOfWork uow;

        public UserDomainService(IUnitOfWork uow, IDirectoryUserRepository directoryUserRepository)
        {
            this.uow = uow;
            this.directoryUserRepository = directoryUserRepository;
        }
        public DirectoryUser Read(Guid id)
        {
            return directoryUserRepository.Read(id);
        }

        public DirectoryUser ReadByNif(string nif)
        {
            return directoryUserRepository.ReadByNif(nif);
        }

        public bool CheckIfUserIdDocumentExists(string nif, Guid? userId)
        {
            return directoryUserRepository.CheckIfUserIdDocumentExists(nif, userId);
        }

        public void Update(DirectoryUser value)
        {
            value.Validate();
            directoryUserRepository.Update(value, false);
        }
        public void Create(DirectoryUser value)
        {
            value.Validate();
            directoryUserRepository.Create(value);
        }

        public DirectoryUser GetUserByNif(string id)
        {
            return this.directoryUserRepository.ReadByNif(id);
        }
    }
}
