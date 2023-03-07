using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Extensions;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace Cgpe.Du.Infrastructure.Data
{

    public class DuDbContext : DbContext
    {

        #region Core

        public DbSet<ProcuratorEntity> Procurators { get; set; }

        public DbSet<LanguageEntity> Languages { get; set; }

        public DbSet<ProcuratorLanguageEntity> ProcuratorLanguages { get; set; }

        public DbSet<AssociationEntity> Associations { get; set; }

        public DbSet<AssociationProcuratorEntity> AssociationProcurators { get; set; }

        public DbSet<ProcuratorSituationEntity> ProcuratorSituations { get; set; }

        public DbSet<ProcuratorPositionEntity> ProcuratorPositions { get; set; }

        public DbSet<SituationChangeEntity> ProcuratorSituationHistory { get; set; }

        public DbSet<SuspensionEntity> Suspensions { get; set; }

        public DbSet<CancellationEntity> Cancellations { get; set; }

        public DbSet<SexEntity> Sexes { get; set; }

        public DbSet<IdentificationDocumentFileEntity> IdentificationDocumentFiles { get; set; }

        public DbSet<CreationStateEntity> CreationStates { get; set; }

        #endregion

        #region Location

        public DbSet<WayTypeEntity> WayTypes { get; set; }

        public DbSet<ProvinceEntity> Provinces { get; set; }

        public DbSet<CityEntity> Cities { get; set; }

        public DbSet<ContactTypeEntity> ContactTypes { get; set; }

        public DbSet<ContactEntity> Contacts { get; set; }

        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<AddressTypeEntity> AddressTypes { get; set; }

        #endregion

        #region Position

        public DbSet<PositionEntity> Positions { get; set; }
        public DbSet<PositionTypeEntity> PositionTypes { get; set; }
        public DbSet<OrganEntity> Organs { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public DbSet<OrganizationTypeEntity> OrganizationTypes { get; set; }

        #endregion

        #region Agreements

        public DbSet<AgreementEntity> Agreements { get; set; }

        public DbSet<ProcuratorAdherenceEntity> ProcuratorAdherences { get; set; }

        #endregion

        #region Distinction

        public DbSet<HonourEntity> Honours { get; set; }

        public DbSet<HonourTypeEntity> HonourTypes { get; set; }

        #endregion

        #region Security

        public DbSet<DirectoryClaimEntity> DirectoryClaims { get; set; }

        public DbSet<DirectoryRoleEntity> DirectoryRoles { get; set; }

        public DbSet<DirectoryRoleClaimEntity> DirectoryRoleClaims { get; set; }

        public DbSet<DirectoryUserCertificateEntity> DirectoryUserCertificates { get; set; }

        public DbSet<DirectoryUserEntity> DirectoryUsers { get; set; }

        public DbSet<DirectoryUserRoleEntity> DirectoryUserRoles { get; set; }

        #endregion

        #region Integration

        public DbSet<IntegrationWorkflowEntity> IntegrationWorkflows { get; set; }

        public DbSet<ProcuratorSyncEntity> ProcuratorSyncs { get; set; }

        #endregion

        #region Audit

        public DbSet<AuditEntity> Audits { get; set; }

        public DbSet<OperationTypeEntity> OperationTypes { get; set; }

        #endregion


        #region Ah Hoc Queries

        public DbSet<AdHocQueryResultEntity> AdHocQueryResults { get; set; }
        public DbSet<AdHocProcuratorResultEntity> AdHocProcuratorResults { get; set; }
        public DbSet<AdHocCountResultEntity> AdHocCountResults { get; set; }
        public DbSet<AdHocAssociationTotalsResultEntity> AdHocAssociationTotalsResults { get; set; }

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
            }
            catch
            {
                // Para que funcione en WCF
                configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            }
            string connectionString = configuration.GetConnectionString("DuDataBase");
            optionsBuilder.UseLazyLoadingProxies(false);
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
