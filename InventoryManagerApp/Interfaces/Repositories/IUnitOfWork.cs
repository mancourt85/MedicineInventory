using System.Threading.Tasks;
using MedicineInventoryApp.Models;

namespace MedicineInventoryApp.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IMedicineRepository Medicines { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task<bool> CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
