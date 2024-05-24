using Microsoft.AspNetCore.Mvc;
using PatientBackend1.Models;
using PatientBackend1.Services.PatientServices;
using AutoMapper;


[Route("api/subscriptions")]
public class SubscriptionController : ControllerBase
{    private readonly IPatientService _patientService; 
    private readonly SubscriptionService _subscriptionService;
    private readonly ILogger<SubscriptionController> _logger; // Added for logging
    private readonly IMapper _mapper;

    public SubscriptionController(SubscriptionService subscriptionService, ILogger<SubscriptionController> logger, IMapper mapper)
    {
        _subscriptionService = subscriptionService;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> SubscribePatientToTelemedicine(
        [FromBody] SubscriptionRequest request) // Use a dedicated request model
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
        }

        try
        {     
            // var (status, message, patientDto) = await _patientService.GetpatientrById(patientIdString);
            // var patient = _mapper.Map<Patient>(patientDto);
             var successful = await _subscriptionService.SubscribePatientToTelemedicine(request.PatientId, request.AmountInBirr);

            if (successful)
            {
                return Ok("Subscription successful");
            }
            else
            {
                return BadRequest("Subscription failed"); // More specific error message can be provided
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error subscribing patient: {ex.Message}");
            return StatusCode(500, "Internal Server Error"); // Handle unexpected errors
        }
    }

}

public class SubscriptionRequest // Model for subscription request
{
    public string PatientId { get; set; }
    public double AmountInBirr { get; set; }
}
