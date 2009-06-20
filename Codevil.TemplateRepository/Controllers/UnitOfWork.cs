using System.Data.Common;
using System.Data.Linq;

namespace Codevil.TemplateRepository.Controllers
{
    public class UnitOfWork
    {
        public DbTransaction Transaction { get; set; }
        public DataContext DataContext { get; set; }

        public UnitOfWork(DataContext dataContext)
        {
            this.DataContext = dataContext;

            if (this.DataContext.Connection.State == System.Data.ConnectionState.Closed)
            {
                this.DataContext.Connection.Open();
            }

            this.Transaction = this.DataContext.Connection.BeginTransaction();
            this.DataContext.Transaction = this.Transaction;
        }

        public void Commit()
        {
            this.Transaction.Commit();

            this.DataContext.Dispose();
        }

        public void Rollback()
        {
            this.Transaction.Rollback();

            this.DataContext.Dispose();
        }
    }
}
