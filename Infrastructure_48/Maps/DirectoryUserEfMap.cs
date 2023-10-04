using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cgpe.Du.Infrastructure
{

    internal class DirectoryUserEfMap
    {

        public void Map(DirectoryUserEntity source, DirectoryUser target)
        {
            if (target.DirectoryRoles == null)
            {
                target.DirectoryRoles = new List<DirectoryRole>();
            }

            if (target.DirectoryUserCertificates == null)
            {
                target.DirectoryUserCertificates = new List<DirectoryUserCertificate>();
            }

            target.UserId = source.UserId;
            target.Active = source.Active;
            target.FirstName = source.FirstName;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;
            target.Nif = source.Nif;

            if (source.DirectoryUserCertificates != null && source.DirectoryUserCertificates.Count > 0)
            {
                DirectoryUserCertificate cert;
                for (int i = 0; i < source.DirectoryUserCertificates.Count; i++)
                {
                    cert = new DirectoryUserCertificate();
                    DirectoryUserCertificateEfMap mapper = new DirectoryUserCertificateEfMap();
                    mapper.Map(source.DirectoryUserCertificates[i], cert);
                    target.DirectoryUserCertificates.Add(cert);
                }
            }

            if (source.DirectoryRoles != null && source.DirectoryRoles.Count > 0)
            {
                DirectoryRole role;
                for (int i = 0; i < source.DirectoryRoles.Count; i++)
                {
                    role = new DirectoryRole();
                    DirectoryRoleEfMap mapper = new DirectoryRoleEfMap();
                    mapper.Map(source.DirectoryRoles[i].Role, role);
                    target.DirectoryRoles.Add(role);

                    if (source.DirectoryRoles[i].Role != null
                    && source.DirectoryRoles[i].Role.Claims != null)
                    {
                        foreach (var directoryRoleClaimEntity in source.DirectoryRoles[i].Role.Claims)
                        {
                            target.DirectoryRoles.Last().Claims.Add(new DirectoryClaim()
                            {
                                ClaimValue = directoryRoleClaimEntity.Claim.ClaimValue,
                                ClaimId = directoryRoleClaimEntity.Claim.ClaimId
                            });
                        }
                    }
                }
            }

            if (source.Association != null)
            {
                target.Association = new Association();
                AssociationEfMap mapper = new AssociationEfMap();
                mapper.Map(source.Association, target.Association);
            }
        }

        public void Map(DirectoryUser source, DirectoryUserEntity target)
        {
            if (target.DirectoryUserCertificates == null)
            {
                target.DirectoryUserCertificates = new List<DirectoryUserCertificateEntity>();
            }

            if (target.DirectoryRoles == null)
            {
                target.DirectoryRoles = new List<DirectoryUserRoleEntity>();
            }

            target.UserId = source.UserId;
            target.Active = source.Active;
            target.FirstName = source.FirstName;
            target.SecondName1 = source.SecondName1;
            target.SecondName2 = source.SecondName2;
            target.Nif = source.Nif;

            target.AssociationId = source.Association?.AssociationId;

        }

        internal List<DirectoryUser> Map(List<DirectoryUserEntity> users, List<DirectoryUser> list)
        {
            if (users == null) throw new ArgumentNullException(nameof(users));
            if (list == null) throw new ArgumentNullException(nameof(list));
            foreach (var directoryUserEntity in users)
            {
                var directoryUser = new DirectoryUser();
                Map(directoryUserEntity, directoryUser);
                list.Add(directoryUser);
            }

            return list;
        }
    }

}
