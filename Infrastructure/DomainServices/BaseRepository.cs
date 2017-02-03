using Infrastructure.Providers; 
using System;
using System.Configuration;

using NI.Data;

namespace Infrastructure.DomainServices
{
    public class BaseRepository : IDisposable
    {
        protected DbDalc db = null;

        protected string connectionString;
        protected BaseRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
            db = new DbDalc(new CustomSqlServerProviderFactory(), connectionString);
            db.Connection.Open();
        }
       

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Connection.Close();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
