using MedicineInventoryApp.Models;

namespace InventoryManagerApp.Interfaces.Export
{
    public interface IExportService
    {
        byte[] ExportMedicinesToExcel(IEnumerable<Medicine> medicines);
        byte[] ExportMedicinesToPdf(IEnumerable<Medicine> medicines);
    }

}
