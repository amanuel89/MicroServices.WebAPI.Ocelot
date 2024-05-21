using ConsigneService.Application.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsigneService.Application.Services.UnitOfWork
{

    public class UnitOfWork : IUnitOfWork
    {

        private readonly DbContext _dbContext;

        //public IRepositoryBase<Employee> Employees { get; }

        public UnitOfWork(ApplicationDbContext dbContext
            //,IRepositoryBase<Employee> employeeRepository
            )
        {
            this._dbContext = dbContext;

            //this.Employees = employeeRepository;
        }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }

        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
           => _dbContext.SaveChanges();


        public async Task CommitAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();

    }
}