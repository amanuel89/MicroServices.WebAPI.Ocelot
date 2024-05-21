using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsigneService.Application.Services.UnitOfWork
{ 
    public interface IUnitOfWork : IDisposable
    {      
        //IRepositoryBase<Employee> Employees { get; }
        int Complete();
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}