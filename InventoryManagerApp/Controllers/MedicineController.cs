using Microsoft.AspNetCore.Mvc;
using MedicineInventoryApp.Models;
using MedicineInventoryApp.Interfaces.Repositories;
using InventoryManagerApp.Interfaces.Export;

namespace MedicineInventoryApp.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExportService _exportService;

        public MedicinesController(IUnitOfWork unitOfWork, IExportService exportService)
        {
            _unitOfWork = unitOfWork;
            _exportService = exportService;
        }

        public async Task<IActionResult> Index()
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            return View(medicines);
        }

        public async Task<IActionResult> Details(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.BeginTransactionAsync();
                await _unitOfWork.Medicines.AddAsync(medicine);
                await _unitOfWork.CommitTransactionAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medicine medicine)
        {
            if (id != medicine.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.BeginTransactionAsync();
                    _unitOfWork.Medicines.Update(medicine);
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return BadRequest("An error occurred while updating the record.");
                }

                return RedirectToAction(nameof(Index));
            }
            return View(medicine);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null) return NotFound();
            return View(medicine);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null) return NotFound();

            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.Medicines.Delete(medicine);
            await _unitOfWork.CommitTransactionAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ExportPdf()
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            var pdf = _exportService.ExportMedicinesToPdf(medicines);
            return File(pdf, "application/pdf", "medicine_inventory.pdf");
        }

        [HttpGet]
        public async Task<IActionResult> ExportExcel()
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            var excel = _exportService.ExportMedicinesToExcel(medicines);
            return File(excel,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "medicine_inventory.xlsx");
        }

    }
}
