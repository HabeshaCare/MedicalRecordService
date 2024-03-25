using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using patientBackend1.Models.DTOs.UserDTOs;
using patientBackend1.Services.UserServices;
using PatientBackend1.Services.PatientServices;

namespace PatientBackend1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
            
        }
        [HttpGet]

public async Task<IActionResult> GetPatient (String PatientId){
    
    var (status,message,user) = await _patientService.GetpatientrById( PatientId);
    if(status == 0 || user == null)
        return BadRequest(new{errors=message});

    return Ok(new{message, user});
// Console.WriteLine(user);
    
   return Ok(user);
}
// Get All the Patients
[HttpGet("getAll")]
public async Task<IActionResult> GetPatients( ) {
    var (Status,Message,patients) = await _patientService.GetPatients();
    if(Status == 0 || patients == null){
        return NotFound(new { error = Message});
    }
    return Ok(new {usrs = patients});

}

// updating the patient
[HttpPut]
public async Task<IActionResult> UpdatePatient(UsagePatientDTO model, string patientId){

    var (status,message,patient)= await _patientService.UpdatePatient(model, patientId);
    if(status == 0 || patient == null)
        return BadRequest(new{errors=message});

    return Ok(new{message, patient});

}


    }
}

