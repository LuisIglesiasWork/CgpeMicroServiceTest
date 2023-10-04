using System;

namespace Cgpe.Du.Domain
{

    public interface IUnitOfWork : IDisposable
    {

        #region Common

        void Commit();

        void Rollback();

        #endregion

    }

}
