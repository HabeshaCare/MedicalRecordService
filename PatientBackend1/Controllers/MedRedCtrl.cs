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
        [HttpPost("post/prescriptions/{medicalRecordId}")]
        public async Task<IActionResult> AddPrescription([FromBody] Prescriptions prescription, string medicalRecordId)
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


        [HttpPut]
[Route("api/medicalRecords/{medicalRecordId}/prescriptions/{prescriptionId}")]
public async Task<IActionResult> UpdatePrescriptionById(string medicalRecordId, string prescriptionId, [FromBody] Prescriptions updatedPrescription)
{
  if (!ModelState.IsValid)
  {
    return BadRequest(ModelState); // Return bad request if model validation fails
  }

  try
  {
    var isUpdated = await _medRecServices.UpdatePrescriptionById(medicalRecordId, prescriptionId, updatedPrescription);

    if (isUpdated)
    {
      return Ok(new { data = updatedPrescription, message = "Prescription status updateded successfully" });; // Return 204 No Content on successful update
    }
    else
    {
      return NotFound(); // Return 404 Not Found if prescription wasn't updated (e.g., not found)
    }
  }
  catch (Exception ex)
  {
    Console.WriteLine($"Error updating prescription: {ex.Message}");
    return StatusCode(500, "Internal Server Error"); // Return 500 on internal server errors
  }
}


    [HttpPost("post/Reports/{medicalRecordId}")]
    public async Task<IActionResult> AddMedicalReport([FromBody] MedicalReport report,string medicalRecordId)
{
  try
  {
    var reportAdded = await _medRecServices.AddMedicalReport(medicalRecordId, report);

    if (reportAdded)
    {
// Fetch the updated medical record with the added MedReports
                    var updatedMedicalRecord = await GetMedicalRecord(medicalRecordId);  // Assuming you have a GetMedicalRecord method

                    return Ok(new { data = updatedMedicalRecord, message = "Report added successfully" });
    }
    else
    {
      return BadRequest("Failed to add medical report");
    }
  }
  
  catch (Exception ex)
  {
    return BadRequest($"Error adding medical report: {ex.Message}"); // Provide more details in production
  }
}

[HttpPost("post/Tests/{medicalRecordId}")]
public async Task<IActionResult> AddLabTest([FromBody]  LabTestResult Tests,string medicalRecordId)
{
  try
  {
    var TestAdded = await _medRecServices.AddLabResults(medicalRecordId, Tests);

    if (TestAdded)
    {
// Fetch the updated medical record with the added LabTestResults
                    var updatedMedicalRecord = await GetMedicalRecord(medicalRecordId);  // Assuming you have a GetMedicalRecord method

                    return Ok(new { data = updatedMedicalRecord, message = "LabTestResul added successfully" });
    }
    else
    {
      return BadRequest("Failed to add LabTestResuls");
    }
  }
 
  catch (Exception ex)
  {
    return BadRequest($"Error adding LabTestResuls: {ex.Message}"); // Provide more details in production
  }
}



    }
}




