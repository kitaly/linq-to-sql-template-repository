using System.Data.Common;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Handlers
{
    public class Transaction
    {
        public DbTransaction DbTransaction { get; set; }
        public DataContext DataContext { get; set; }

        public Transaction(DataContext dataContext)
        {
            this.DataContext = dataContext;

            if (this.DataContext.Connection.State == System.Data.ConnectionState.Closed)
            {
                this.DataContext.Connection.Open();
            }

            this.DbTransaction = this.DataContext.Connection.BeginTransaction();
            this.DataContext.Transaction = this.DbTransaction;
        }

        public void Commit()
        {
            try
            {
                this.DbTransaction.Commit();
            }
            finally
            {
                this.DataContext.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                this.DbTransaction.Rollback();
            }
            finally
            {
                this.DataContext.Dispose();
            }
        }
    }
}
