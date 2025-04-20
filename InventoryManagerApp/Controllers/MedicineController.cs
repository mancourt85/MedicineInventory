using Microsoft.AspNetCore.Mvc;
using MedicineInventoryApp.Models;
using MedicineInventoryApp.Interfaces.Repositories;

namespace MedicineInventoryApp.Controllers
{
    public class MedicinesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicinesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
