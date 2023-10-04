using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.Data.EntityFramework;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Cgpe.Du.Infrastructure.Data
{
    [DbContext(typeof(MySqlEFConfiguration))]
    public class DuDbContext : DbContext
    {

        #region Core

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorEntity> Procurators { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<LanguageEntity> Languages { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorLanguageEntity> ProcuratorLanguages { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<AssociationEntity> Associations { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<AssociationProcuratorEntity> AssociationProcurators { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorSituationEntity> ProcuratorSituations { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorPositionEntity> ProcuratorPositions { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<SituationChangeEntity> ProcuratorSituationHistory { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<SuspensionEntity> Suspensions { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<CancellationEntity> Cancellations { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<SexEntity> Sexes { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<IdentificationDocumentFileEntity> IdentificationDocumentFiles { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<CreationStateEntity> CreationStates { get; set; }

        #endregion

        #region Location

        public Microsoft.EntityFrameworkCore.DbSet<WayTypeEntity> WayTypes { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProvinceEntity> Provinces { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<CityEntity> Cities { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ContactTypeEntity> ContactTypes { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ContactEntity> Contacts { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<AddressEntity> Addresses { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<AddressTypeEntity> AddressTypes { get; set; }

        #endregion

        #region Position

        public Microsoft.EntityFrameworkCore.DbSet<PositionEntity> Positions { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<PositionTypeEntity> PositionTypes { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrganEntity> Organs { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrganizationEntity> Organizations { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<OrganizationTypeEntity> OrganizationTypes { get; set; }

        #endregion

        #region Agreements

        public Microsoft.EntityFrameworkCore.DbSet<AgreementEntity> Agreements { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorAdherenceEntity> ProcuratorAdherences { get; set; }

        #endregion

        #region Distinction

        public Microsoft.EntityFrameworkCore.DbSet<HonourEntity> Honours { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<HonourTypeEntity> HonourTypes { get; set; }

        #endregion

        #region Security

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryClaimEntity> DirectoryClaims { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryRoleEntity> DirectoryRoles { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryRoleClaimEntity> DirectoryRoleClaims { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryUserCertificateEntity> DirectoryUserCertificates { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryUserEntity> DirectoryUsers { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<DirectoryUserRoleEntity> DirectoryUserRoles { get; set; }

        #endregion

        #region Integration

        public Microsoft.EntityFrameworkCore.DbSet<IntegrationWorkflowEntity> IntegrationWorkflows { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<ProcuratorSyncEntity> ProcuratorSyncs { get; set; }

        #endregion

        #region Audit

        public Microsoft.EntityFrameworkCore.DbSet<AuditEntity> Audits { get; set; }

        public Microsoft.EntityFrameworkCore.DbSet<OperationTypeEntity> OperationTypes { get; set; }

        #endregion


        #region Ah Hoc Queries

        public Microsoft.EntityFrameworkCore.DbSet<AdHocQueryResultEntity> AdHocQueryResults { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AdHocProcuratorResultEntity> AdHocProcuratorResults { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AdHocCountResultEntity> AdHocCountResults { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<AdHocAssociationTotalsResultEntity> AdHocAssociationTotalsResults { get; set; }

        #endregion

        public static string ApplicationExeDirectory()
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var appRoot = Path.GetDirectoryName(location);

            return appRoot;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string applicationExeDirectory = ApplicationExeDirectory();

            IConfigurationRoot configuration;
            try
            {
                // Para que funcione en .NET Core
                configuration = new ConfigurationBuilder()
                .SetBasePath(applicationExeDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                // Para que funcione en WCF
                //configuration = new ConfigurationBuilder()
                //.AddJsonFile("appsettings.json")
                //.Build();
            }
            catch
            {

                configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            }
            string connectionString = configuration.GetConnectionString("DuDataBase");
            //optionsBuilder.UseLazyLoadingProxies(false);
            optionsBuilder.UseMySQL(connectionString);


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProcuratorEntity>().HasIndex(p => p.Nif).IsUnique();
            modelBuilder.Entity<ProcuratorEntity>().HasIndex(p => p.UniqueNumber).IsUnique();


            modelBuilder.Entity<ProcuratorPositionEntity>().HasKey(pp => new { pp.ProcuratorId, pp.PositionId });
            modelBuilder.Entity<DirectoryRoleClaimEntity>().HasKey(rc => new { rc.RoleId, rc.ClaimId });
            modelBuilder.Entity<DirectoryUserRoleEntity>().HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<ProcuratorLanguageEntity>().HasKey(pl => new { pl.ProcuratorId, pl.LanguageId });

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<CityEntity>().HasKey(c => c.CityId);
            //modelBuilder.Entity<CityEntity>().HasKey(c => c.CityId).ForSqlServerIsClustered(false);
        }

    }

}
