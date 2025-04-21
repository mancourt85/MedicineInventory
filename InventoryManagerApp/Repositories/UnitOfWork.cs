using MedicineInventoryApp.Data;
using MedicineInventoryApp.Interfaces.Repositories;
using MedicineInventoryApp.Models;

namespace MedicineInventoryApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly InventoryContext _context;
        public IMedicineRepository Medicines { get; }

        public UnitOfWork(InventoryContext context, IMedicineRepository medicineRepository)
        {
            _context = context;
            Medicines = medicineRepository;
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> CommitTransactionAsync()
        {
            if (_context.Database.CurrentTransaction == null)
                return false;

            try
            {
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                return false;
            }
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
