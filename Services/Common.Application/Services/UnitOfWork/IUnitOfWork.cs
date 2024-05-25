using RideBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideBackend.Application.Services.UnitOfWork
{ 
    public interface IUnitOfWork : IDisposable
    {      
      


    

       // IRepositoryBase<Order> Order { get; }

        int Complete();
        void Commit();
        void Rollback();
        Task CommitAsync();
        Task RollbackAsync();
    }
}