using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Cgpe.Du.Infrastructure
{

    public class DirectoryUserRepository : IDirectoryUserRepository
    {

        private DuUnitOfWork uow;

        public DirectoryUserRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }

        public void UpsertDirectoryUser(DirectoryUser newDirectoryUser, bool isNew, bool considerCertificates)
        {
            DirectoryUserEntity directoryUserEntity = null;

            if (isNew)
            {
                directoryUserEntity = new DirectoryUserEntity();
                newDirectoryUser.UserId = Guid.NewGuid();
            }
            else
            {
                directoryUserEntity = this.ReadDirectoryUserEntity(newDirectoryUser.UserId);
                if (directoryUserEntity == null)
                {
                    throw new Exception($"User with Id \"{newDirectoryUser.UserId}\" was not found.");
                }
            }

            new DirectoryUserEfMap().Map(newDirectoryUser, directoryUserEntity);
            newDirectoryUser.UserId = directoryUserEntity.UserId;

            CreateDeleteDirectoryRoles(newDirectoryUser.DirectoryRoles, directoryUserEntity);
            if (considerCertificates)
            {
                CreateClientCertificates(newDirectoryUser.DirectoryUserCertificates, directoryUserEntity);
            }

            if (isNew)
            {
                this.uow.DbContext.DirectoryUsers.Add(directoryUserEntity);
            }
        }

        private void CreateDeleteDirectoryRoles(List<DirectoryRole> newDirectoryRoles, DirectoryUserEntity directoryUserEntity)
        {
            foreach (DirectoryUserRoleEntity existingUserRole in directoryUserEntity.DirectoryRoles)
            {
                if (!newDirectoryRoles.Any(c => c.RoleId == existingUserRole.RoleId))
                {
                    this.uow.DbContext.Remove(existingUserRole);
                }
            }

            DirectoryUserRoleEfMap userRoleMapper = new DirectoryUserRoleEfMap();
            foreach (DirectoryRole role in newDirectoryRoles)
            {
                DirectoryUserRoleEntity existingUserRoleEntity = directoryUserEntity.DirectoryRoles
                    .Where(c => c.RoleId == role.RoleId)
                    .SingleOrDefault();

                if (existingUserRoleEntity == null)
                {
                    DirectoryUserRoleEntity userRoleEntity = new DirectoryUserRoleEntity();
                    userRoleEntity.UserId = directoryUserEntity.UserId;
                    userRoleEntity.RoleId = role.RoleId;
                    directoryUserEntity.DirectoryRoles.Add(userRoleEntity);
                }
            }

        }

        private void CreateClientCertificates(List<DirectoryUserCertificate> newCertificates, DirectoryUserEntity directoryUserEntity)
        {
            DirectoryUserCertificateEfMap certMapper = new DirectoryUserCertificateEfMap();
            foreach (DirectoryUserCertificate cert in newCertificates)
            {
                DirectoryUserCertificateEntity existingUserCertEntity = directoryUserEntity.DirectoryUserCertificates
                    .Where(c => c.CertificateId == cert.CertificateId || c.SerialNumber == cert.SerialNumber)
                    .SingleOrDefault();

                if (existingUserCertEntity == null)
                {
                    DirectoryUserCertificateEntity userCertEntity = new DirectoryUserCertificateEntity();
                    certMapper.Map(cert, userCertEntity, userCertEntity.UserId);
                    directoryUserEntity.DirectoryUserCertificates.Add(userCertEntity);
                }
            }

        }

        public void Create(DirectoryUser newDomainDirectoryUser)
        {
            this.UpsertDirectoryUser(newDomainDirectoryUser, true, false);
        }


        public DirectoryUserEntity ReadDirectoryUserEntity(Guid Id)
        {
            DirectoryUserEntity directoryUserEntity = uow.DbContext.DirectoryUsers
                    .Include(x => x.Association)
                    .Include(x => x.DirectoryRoles)
                        .ThenInclude(y => y.Role)
                    .Include(x => x.DirectoryUserCertificates)
                .Where(p => p.UserId == Id).FirstOrDefault();

            return directoryUserEntity;
        }

        public DirectoryUser Read(Guid id)
        {
            DirectoryUserEntity entity = this.ReadDirectoryUserEntity(id);

            if (entity == null)
                return null;
            DirectoryUser directoryUser = new DirectoryUser();
            new DirectoryUserEfMap().Map(entity, directoryUser);
            return directoryUser;
        }

        public void Update(DirectoryUser newDirectoryUser, bool considerCertificates)
        {
            this.UpsertDirectoryUser(newDirectoryUser, false, considerCertificates);
        }

        public List<DirectoryUser> GetUsersPage(Guid? associationId, int pageIndex, int pageSize, ref int totalRecords)
        {
            IQueryable<DirectoryUserEntity> query =
                uow.DbContext.DirectoryUsers.Include(x => x.Association)
                    .Include(x => x.DirectoryRoles
                    ).ThenInclude(y => y.Role).ThenInclude(z => z.Claims).ThenInclude(w => w.Claim)
                    .OrderBy(x => x.FirstName).ThenBy(x => x.SecondName1).ThenBy(x => x.SecondName2)
                    .AsQueryable<DirectoryUserEntity>();

            if (associationId != null && associationId.HasValue)
            {
                query = query.Where(p => p.AssociationId == associationId.Value);
            }

            totalRecords = query.Count();
            List<DirectoryUserEntity> entities = query.Skip((pageSize) * pageIndex).Take(pageSize).ToList();
            List<DirectoryUser> users = new List<DirectoryUser>();
            DirectoryUser user;
            foreach (DirectoryUserEntity entity in entities)
            {
                user = new DirectoryUser();
                new DirectoryUserEfMap().Map(entity, user);
                users.Add(user);
            }
            return users;

        }

        public bool CheckIfUserIdDocumentExists(string nif, Guid? userId)
        {
            DirectoryUserEntity userEntity =
                uow.DbContext.DirectoryUsers
                    .Where(u => u.Nif == nif && (userId == null || !userId.HasValue || userId.Value != u.UserId)).FirstOrDefault();

            return userEntity != null;
        }

        public DirectoryUser ReadByNif(string nif)
        {
            DirectoryUserEntity userEntity =
                uow.DbContext.DirectoryUsers.Include(x => x.Association)
                    .Include(x => x.DirectoryRoles
                    ).ThenInclude(y => y.Role).ThenInclude(z => z.Claims).ThenInclude(w => w.Claim)
                    .Include(x => x.DirectoryUserCertificates)
                    .Where(u => u.Nif == nif).FirstOrDefault();

            DirectoryUser user = null;

            if (userEntity != null)
            {
                user = new DirectoryUser();
                (new DirectoryUserEfMap()).Map(userEntity, user);
            }

            return user;
        }
    }

}
