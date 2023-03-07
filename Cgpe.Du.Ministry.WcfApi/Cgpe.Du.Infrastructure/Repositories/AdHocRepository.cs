using Cgpe.Du.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Cgpe.Du.Domain.Entities;
using Cgpe.Du.Infrastructure.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query.Internal;
//using Remotion.Linq.Parsing.Structure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using MySqlConnector;
//using System.Data.SqlClient;

namespace Cgpe.Du.Infrastructure
{

    public class AdHocRepository : IAdHocRepository
    {

        private DuUnitOfWork uow;

        public AdHocRepository(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException("Unit Of Work");
            this.uow = uow as DuUnitOfWork;
            if (this.uow == null)
                throw new Exception("This type of Unit Of Work is not supported.");
        }


        private string GetAssociationTotalsCountQuery()
        {
            return @"EXEC spGetAssociationTotalsCount";
        }
        private string GetAssociationTotalsQuery()
        {
            return @"EXEC spGetAssociationTotals @minRow, @maxRow";
        }

        private string GetProcuratorsWithEmailsCountQuery()
        {
            return @"EXEC spGetProcuratorsWithEmailsCount";
        }
        private string GetProcuratorsWithEmailsQuery()
        {
            return @"EXEC spGetProcuratorsWithEmails @minRow, @maxRow";
        }


        private string GetProcuratorsPerSexAndPractiseCountQuery()
        {
            return @"EXEC spGetProcuratorsPerSexAndPractiseCount @sexId, @isPractising";
        }
        private string GetProcuratorsPerSexAndPractiseQuery()
        {
            return @"EXEC spGetProcuratorsPerSexAndPractise @sexId, @isPractising, @minRow, @maxRow";
        }

        private string GetProcuratorsWithMagAddressesCountQuery()
        {
            return @"EXEC [spGetProcuratorsWithMagAddressesCount]";
        }

        private string GetProcuratorsWithMagAddressesQuery()
        {
            return @"EXEC [spGetProcuratorsWithMagAddresses] @minRow, @maxRow";
        }

        public List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractisePage(int? sexId, bool? isPractising,
         int pageIndex, int pageSize, ref int totalRecords)
        {

            MySqlAttribute sexIdParam = sexId.HasValue ? new MySqlAttribute("sexId", sexId.Value) : new MySqlAttribute("sexId", DBNull.Value);
            MySqlAttribute isPractisingParam = isPractising.HasValue ? new MySqlAttribute("isPractising", isPractising.Value) : new MySqlAttribute("isPractising", DBNull.Value);

            int maxRow = pageSize * (pageIndex + 1);
            int minRow = maxRow - pageSize + 1;

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", minRow);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", maxRow);

            List<AdHocCountResultEntity> totalRecordsResult = uow.DbContext.AdHocCountResults.FromSqlRaw(this.GetProcuratorsPerSexAndPractiseCountQuery(), sexIdParam, isPractisingParam).ToList();

            totalRecords = totalRecordsResult[0].CountResult;

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults.FromSqlRaw(this.GetProcuratorsPerSexAndPractiseQuery(), sexIdParam, isPractisingParam, minRowParam, maxRowParam ).ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public List<AdHocProcuratorResult> GetProcuratorsPerSexAndPractise(int? sexId, bool? isPractising)
        {
            MySqlAttribute sexIdParam = sexId.HasValue ? new MySqlAttribute("sexId", sexId.Value) : new MySqlAttribute("sexId", DBNull.Value);
            MySqlAttribute isPractisingParam = isPractising.HasValue ? new MySqlAttribute("isPractising", isPractising.Value) : new MySqlAttribute("isPractising", DBNull.Value);


            MySqlAttribute minRowParam = new MySqlAttribute("minRow", DBNull.Value);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", DBNull.Value);

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults.FromSqlRaw(this.GetProcuratorsPerSexAndPractiseQuery(), sexIdParam, isPractisingParam, minRowParam, maxRowParam).ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }


        public List<AdHocProcuratorResult> GetProcuratorsWithEmailsPage(int pageIndex, int pageSize, ref int totalRecords)
        {


            int maxRow = pageSize * (pageIndex + 1);
            int minRow = maxRow - pageSize + 1;

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", minRow);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", maxRow);

            List<AdHocCountResultEntity> totalRecordsResult = uow.DbContext.AdHocCountResults
                .FromSqlRaw(this.GetProcuratorsWithEmailsCountQuery()).ToList();

            totalRecords = totalRecordsResult[0].CountResult;

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults
                .FromSqlRaw(this.GetProcuratorsWithEmailsQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithEmails()
        {
   
            MySqlAttribute minRowParam = new MySqlAttribute("minRow", DBNull.Value);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", DBNull.Value);

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults
                .FromSqlRaw(this.GetProcuratorsWithEmailsQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithMagAddressPage(int pageIndex, int pageSize, ref int totalRecords)
        {


            int maxRow = pageSize * (pageIndex + 1);
            int minRow = maxRow - pageSize + 1;

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", minRow);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", maxRow);

            List<AdHocCountResultEntity> totalRecordsResult = uow.DbContext.AdHocCountResults
                .FromSqlRaw(this.GetProcuratorsWithMagAddressesCountQuery()).ToList();

            totalRecords = totalRecordsResult[0].CountResult;

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults
                .FromSqlRaw(this.GetProcuratorsWithMagAddressesQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }

        public List<AdHocProcuratorResult> GetProcuratorsWithMagAddress()
        {

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", DBNull.Value);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", DBNull.Value);

            List<AdHocProcuratorResultEntity> entities = uow.DbContext.AdHocProcuratorResults
                .FromSqlRaw(this.GetProcuratorsWithMagAddressesQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocProcuratorResult> procurators = new List<AdHocProcuratorResult>();
            AdHocProcuratorResult procurator;
            foreach (AdHocProcuratorResultEntity entity in entities)
            {
                procurator = new AdHocProcuratorResult();
                new AdHocProcuratorResultEfMap().Map(entity, procurator);
                procurators.Add(procurator);
            }
            return procurators;
        }

  
        public List<AdHocAssociationTotalsResult> GetAssociationsTotalsPage(int pageIndex, int pageSize, ref int totalRecords)
        {


            int maxRow = pageSize * (pageIndex + 1);
            int minRow = maxRow - pageSize + 1;

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", minRow);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", maxRow);

            List<AdHocCountResultEntity> totalRecordsResult = uow.DbContext.AdHocCountResults
                .FromSqlRaw(this.GetAssociationTotalsCountQuery()).ToList();

            totalRecords = totalRecordsResult[0].CountResult;

            List<AdHocAssociationTotalsResultEntity> entities = uow.DbContext.AdHocAssociationTotalsResults
                .FromSqlRaw(this.GetAssociationTotalsQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocAssociationTotalsResult> allAssoTotals = new List<AdHocAssociationTotalsResult>();
            AdHocAssociationTotalsResult oneAssoTotals;
            foreach (AdHocAssociationTotalsResultEntity entity in entities)
            {
                oneAssoTotals = new AdHocAssociationTotalsResult();
                new AdHocAssociationTotalsResultEfMap().Map(entity, oneAssoTotals);
                allAssoTotals.Add(oneAssoTotals);
            }
            return allAssoTotals;
        }

        public List<AdHocAssociationTotalsResult> GetAssociationsTotals()
        {

            MySqlAttribute minRowParam = new MySqlAttribute("minRow", DBNull.Value);
            MySqlAttribute maxRowParam = new MySqlAttribute("maxRow", DBNull.Value);

            List<AdHocAssociationTotalsResultEntity> entities = uow.DbContext.AdHocAssociationTotalsResults
                .FromSqlRaw(this.GetAssociationTotalsQuery(), minRowParam, maxRowParam)
                .ToList();

            List<AdHocAssociationTotalsResult> assoTotals = new List<AdHocAssociationTotalsResult>();
            AdHocAssociationTotalsResult oneAssoTotals;
            foreach (AdHocAssociationTotalsResultEntity entity in entities)
            {
                oneAssoTotals = new AdHocAssociationTotalsResult();
                new AdHocAssociationTotalsResultEfMap().Map(entity, oneAssoTotals);
                assoTotals.Add(oneAssoTotals);
            }
            return assoTotals;
        }


    }
}