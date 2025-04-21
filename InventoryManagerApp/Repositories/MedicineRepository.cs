using MedicineInventoryApp.Data;
using MedicineInventoryApp.Interfaces.Repositories;
using MedicineInventoryApp.Models;

namespace MedicineInventoryApp.Repositories
{
    public class MedicineRepository : Repository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(InventoryContext context) : base(context)
        {
        }

    }
}
