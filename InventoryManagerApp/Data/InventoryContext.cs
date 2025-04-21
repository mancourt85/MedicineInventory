using MedicineInventoryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicineInventoryApp.Data
{
    public class InventoryContext : DbContext
    {
        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options) { }

        public DbSet<Medicine> Medicines { get; set; }
    }
}