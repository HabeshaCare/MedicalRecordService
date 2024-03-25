using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using patientBackend1.DTOs;
using patientBackend1.DTOs.MedDTOs;
using patientBackend1.Models;
using patientBackend1.Services;
using patientBackend1.Services.MedicalServices;

namespace patientBackend1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedRecController : ControllerBase
    {
        private readonly IMedRecServices _medRecServices;

        public MedRecController(IMedRecServices medRecServices)
        {
            _medRecServices = medRecServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedicalRecord([FromBody] MedRecDTO medRecDTO)
        {
            try
            {

                var medicalRecords = await _medRecServices.CreateMedicalRecord(medRecDTO);
                return Ok(new
                {
                    data = medicalRecords,
                    message = "Created Successfully"
                });


            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating medical record: {ex.Message}");
            }
        }

        // Get medical record    
        [HttpGet("get/{medRecId}")]
        public async Task<IActionResult> GetMedicalRecord(string medRecId)
        {
            try
            {
                var medicalRecord = await _medRecServices.GetMedicalRecord(medRecId);
                return Ok(new
                {
                    data = medicalRecord,
                    message = "Created Successfully"
                });

            }
            catch (Exception ex)
            {

                return BadRequest($"Error creating medical record: {ex.Message}");
            }

        }
        // update medical Record
        [HttpPut("put/{medrecId}")]
        public async Task<IActionResult> UpdateMedicalRecord([FromBody] MedRecDTO model, string medrecId)
        {
            try
            {
                var medrecUpdated = await _medRecServices.UpdateMedicalRecord(model, medrecId);
                return Ok(new
                {
                    data = medrecUpdated,
                    message = "Medical record Updated succesfully"
                });
            }
            catch (Exception ex)
            {

                return BadRequest($"Error updating medical record: {ex.Message}");
            }
        }
        // Add Prescriptions
        [HttpPost("post/{medicalRecordId}")] // Assuming this method handles a POST request
        public async Task<IActionResult> AddPrescription(string medicalRecordId, [FromBody] Prescriptions prescription)
        {
            try
            {
                var prescriptionAdded = await _medRecServices.AddPrescription(medicalRecordId, prescription);

                if (prescriptionAdded)
                {
                    // Fetch the updated medical record with the added prescription
                    var updatedMedicalRecord = await GetMedicalRecord(medicalRecordId);  // Assuming you have a GetMedicalRecord method

                    return Ok(new { data = updatedMedicalRecord, message = "Prescription added successfully" });
                }
                else
                {
                    return BadRequest("Failed to add prescription");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding prescription: {ex.Message}");
            }
        }


    }
}




