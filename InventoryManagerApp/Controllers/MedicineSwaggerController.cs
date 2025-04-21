using Microsoft.AspNetCore.Mvc;
using MedicineInventoryApp.Models;
using MedicineInventoryApp.Interfaces.Repositories; 

namespace MedicineInventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesApiController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public MedicinesApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/MedicinesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicine>>> GetMedicines()
        {
            var medicines = await _unitOfWork.Medicines.GetAllAsync();
            return Ok(medicines);
        }

        // GET: api/MedicinesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicine>> GetMedicine(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            return Ok(medicine);
        }

        // POST: api/MedicinesApi
        [HttpPost]
        public async Task<ActionResult<Medicine>> PostMedicine(Medicine medicine)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var addedMedicine = await _unitOfWork.Medicines.AddAsync(medicine);

                var result = await _unitOfWork.CommitTransactionAsync();

                if (addedMedicine == null || result == false)
                {
                    return BadRequest("Failed to create the medicine.");
                }

                return CreatedAtAction(nameof(GetMedicine), new { id = addedMedicine.Id }, addedMedicine);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                // log ex if needed
                return StatusCode(500, "An error occurred while creating the medicine.");
            }
        }


        // PUT: api/MedicinesApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine(int id, Medicine medicine)
        {
            if (id != medicine.Id)
                return BadRequest();
            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.Medicines.Update(medicine);
            await _unitOfWork.CommitTransactionAsync();

            return NoContent();
        }

        // DELETE: api/MedicinesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine(int id)
        {
            var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
            if (medicine == null)
                return NotFound();

            await _unitOfWork.BeginTransactionAsync();
            _unitOfWork.Medicines.Delete(medicine);
            await _unitOfWork.CommitTransactionAsync();

            return NoContent();
        }
    }
}
