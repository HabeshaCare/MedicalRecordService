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
        {     var patientIdString = request.PatientId;
            var (status, message, patientDto) = await _patientService.GetpatientrById(patientIdString);
            var patient = _mapper.Map<Patient>(patientDto);
            var successful = await _subscriptionService.SubscribePatientToTelemedicine(
                request.PatientId, request.PackageId, request.DesiredProviderType,patient);

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

    // [HttpGet("active/{patientId}/{providerType}")]
    // public IActionResult HasActiveTelemedicinePackage(int patientId, ServiceProviderType providerType)
    // {
    //     var hasActivePackage = _subscriptionService.HasActiveTelemedicinePackage(patientId, providerType);
    //     return Ok(hasActivePackage);
    // }
}

public class SubscriptionRequest // Model for subscription request
{
    public string PatientId { get; set; }
    public string PackageId { get; set; }
    public ServiceProviderType DesiredProviderType { get; set; }
}
