using ClosedXML.Excel;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using MedicineInventoryApp.Models;
using InventoryManagerApp.Interfaces.Export;
using System.IO;

public class ExportService : IExportService
{
    public byte[] ExportMedicinesToExcel(IEnumerable<Medicine> medicines)
    {
        try
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Medicines");

            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Description";
            worksheet.Cell(1, 3).Value = "Quantity";
            worksheet.Cell(1, 4).Value = "Expiration Date";

            int row = 2;
            foreach (var med in medicines)
            {
                worksheet.Cell(row, 1).Value = med.Name;
                worksheet.Cell(row, 2).Value = med.Description;
                worksheet.Cell(row, 3).Value = med.Quantity;
                worksheet.Cell(row, 4).Value = med.ExpirationDate.ToString("yyyy-MM-dd");
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error exporting to Excel", ex);
        }
    }

    public byte[] ExportMedicinesToPdf(IEnumerable<Medicine> medicines)
    {
        try
        {
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            var font = new XFont("Arial", 12);

            double x = 40;
            double y = 40;
            double rowHeight = 20;
            double columnWidth = 100;

            gfx.DrawString("Name", font, XBrushes.Black, new XRect(x, y, columnWidth, rowHeight), XStringFormats.Center);
            gfx.DrawString("Description", font, XBrushes.Black, new XRect(x + columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);
            gfx.DrawString("Quantity", font, XBrushes.Black, new XRect(x + 2 * columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);
            gfx.DrawString("Expiration Date", font, XBrushes.Black, new XRect(x + 3 * columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);

            y += rowHeight;
            foreach (var med in medicines)
            {
                gfx.DrawString(med.Name, font, XBrushes.Black, new XRect(x, y, columnWidth, rowHeight), XStringFormats.Center);
                gfx.DrawString(med.Description, font, XBrushes.Black, new XRect(x + columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);
                gfx.DrawString(med.Quantity.ToString(), font, XBrushes.Black, new XRect(x + 2 * columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);
                gfx.DrawString(med.ExpirationDate.ToString("yyyy-MM-dd"), font, XBrushes.Black, new XRect(x + 3 * columnWidth, y, columnWidth, rowHeight), XStringFormats.Center);

                y += rowHeight;
            }

            using var stream = new MemoryStream();
            document.Save(stream, false);
            return stream.ToArray();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error exporting to PDF", ex);
        }
    }
}
