using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Domain.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Cgpe.Du.Domain
{

    public class SecurityDomainService
    {

        private IUnitOfWork uow;
        private IDirectoryUserRepository directoryUserRepository;

        public SecurityDomainService(IUnitOfWork uow, IDirectoryUserRepository directoryUserRepository)
        {
            this.uow = uow;
            this.directoryUserRepository = directoryUserRepository;
        }

        public void ReadSecurityData(ref DirectoryUser user, X509Certificate2 clientCertificate)
        {
            if (user != null)
            {
                var existingUser = this.directoryUserRepository.ReadByNif(user.Nif);
                if (existingUser != null)
                {
                    user.UserId = existingUser.UserId;
                    user.FirstName = existingUser.FirstName;
                    user.Active = existingUser.Active;
                    user.SecondName1 = existingUser.SecondName1;
                    user.SecondName2 = existingUser.SecondName2;
                    user.Nif = existingUser.Nif;
                    user.DirectoryRoles = existingUser.DirectoryRoles;
                    user.Association = existingUser.Association;
                    user.IsInitialized = true;

                    DirectoryUserCertificate result = null;
                    if (clientCertificate != null)
                    {
                        result = (from c in existingUser.DirectoryUserCertificates
                                  where c.SerialNumber == clientCertificate.SerialNumber
                                  && this.IsValidCert(c.ActiveFrom, c.ActiveTo)
                                  select c).FirstOrDefault();
                    }

                    if (result == null)
                    {
                        result = new DirectoryUserCertificate()
                        {
                            CertificateId = Guid.NewGuid().ToString(),
                            Active = clientCertificate.NotAfter >= DateTime.Now && clientCertificate.NotBefore < DateTime.Now,
                            ActiveFrom = clientCertificate.NotBefore,
                            ActiveTo = clientCertificate.NotAfter,
                            CreationDate = DateTime.Now,
                            PublicKey = clientCertificate.GetPublicKeyString(),
                            SerialNumber = clientCertificate.SerialNumber
                        };
                        existingUser.DirectoryUserCertificates.Add(result);
                        this.directoryUserRepository.Update(existingUser, true);
                        this.uow.Commit();

                        if (!IsValidCert(result.ActiveFrom, result.ActiveTo))
                        {
                            throw new System.Security.Authentication.AuthenticationException(Resources.AuthenticationException);
                        }
                    }

                }
                else
                {
                    throw new System.Security.Authentication.AuthenticationException(Resources.AuthenticationException);
                }
            }
            else
            {
                throw new System.Security.Authentication.AuthenticationException(Resources.AuthenticationException);
            }
        }

        private bool IsValidCert(DateTime activeFrom, DateTime activeTo)
        {
            return activeTo >= DateTime.Now && activeFrom < DateTime.Now;
        }

        public List<DirectoryUser> GetPaginatedUsers(string associationId, int pageIndex, int pageSize, ref int totalRecords)
        {
            return this.directoryUserRepository.GetUsersPage(associationId, pageIndex, pageSize, ref totalRecords);
        }
    }

}
