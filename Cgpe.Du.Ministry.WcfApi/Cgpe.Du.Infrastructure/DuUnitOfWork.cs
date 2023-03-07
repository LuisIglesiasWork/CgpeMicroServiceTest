using Cgpe.Du.Domain;
using Cgpe.Du.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Cgpe.Du.Infrastructure
{

    public class DuUnitOfWork : IUnitOfWork
    {

        private DuDbContext dbContext;

        internal DuDbContext DbContext { get { return this.dbContext; } }

        public DuUnitOfWork()
        {
            this.dbContext = new DuDbContext();
        }

/*        public static void Main(string[] args)
        {
            var _context = new DuUnitOfWork().DbContext;
            System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "\\Entities.dgml",
                _context.AsDgml(), // https://github.com/ErikEJ/SqlCeToolbox/wiki/EF-Core-Power-Tools
                System.Text.Encoding.UTF8);

            //            IEnumerable<EdmError> errors;
            //
            //            //var writer = XmlWriter.Create(Console.Out, new XmlWriterSettings { Indent = true });
            //            var ctx = new DuUnitOfWork().DbContext.Model;
            //            using (var writer = new XmlTextWriter(@"c:\Model.edmx", Encoding.Default))
            //            {
            //                EdmxWriter.TryWriteEdmx(ctx, writer, EdmxTarget.EntityFramework, out errors);
            //            }
        }*/

        //        private static Stream GetCsdlStreamFromMetadata(IObjectContextAdapter context)
        //        {
        //            var metadata = new EntityConnectionStringBuilder(context.ObjectContext.Connection.ConnectionString).Metadata;
        //            return (from Match match in EntityConnectionMetadataPattern.Matches(metadata)
        //                from Capture capture in match.Groups["name"].Captures
        //                where capture.Value.EndsWith(CsdlFileExtension)
        //                let assembly = Assembly.GetAssembly(context.GetType())
        //                select assembly.GetManifestResourceStream(capture.Value)).Single();
        //        }

        public void Commit()
        {
            int intents = 3;
            do
            {
                try
                {
                    this.dbContext.SaveChanges();
                    return;
                }
                catch (DbUpdateException)
                {
                    if (intents <= 1)
                        throw;
                    else
                        Task.Delay(6000 / intents);
                    intents--;
                }
            }
            while (intents > 0);
        }

        public void Rollback()
        {
            List<EntityEntry> changes = this.dbContext.ChangeTracker.Entries().ToList();
            if (changes != null && changes.Count > 0)
            {
                foreach (EntityEntry change in changes)
                {
                    if (change.State == EntityState.Added)
                        change.State = EntityState.Detached;
                    else
                    {
                        change.State = EntityState.Unchanged;
                        change.CurrentValues.SetValues(change.OriginalValues);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (this.dbContext != null)
                this.dbContext.Dispose();
        }

    }

}
