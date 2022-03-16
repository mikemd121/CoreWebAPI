using CoreWebAPI.Data;
using System;

namespace CoreWebAPI.Repository
{
    public  class UnitOfWork : IUnitOfWork
    {
        #region constructors
        private CoreAPIDbContext _context;
        public UnitOfWork(CoreAPIDbContext context)
        {
            this._context = context;
        }


        #endregion

        #region private-fields
        private bool disposed;

        #endregion

        #region Methods

        public IBaseRepository<T> GetRepository<T>() where T : class
        {
            var repositoryInstance = new BaseRepository<T>(_context);
            return repositoryInstance;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        #endregion

        #region Disposing

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
                if (disposing)
                    _context.Dispose();
            disposed = true;
        }
        #endregion
    }
}
