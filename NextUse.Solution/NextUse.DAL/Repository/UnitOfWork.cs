using Microsoft.EntityFrameworkCore.Storage;
using NextUse.DAL.Extensions;
using NextUse.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextUse.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        private IDbContextTransaction? _currentTransaction;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task BeginTransactionAsync()
        {
            // Start a new transaction
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
                _currentTransaction.Dispose();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
            }
        }
    }
}
