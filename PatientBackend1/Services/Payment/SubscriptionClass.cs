using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using patientBackend1.Services;
using patientBackend1.Services.UserServices;
using PatientBackend1.Models;
using PatientBackend1.Services.PatientServices;
using PatientBackend1.utils;
using Stripe;

public class SubscriptionService:MongoDBService,ISubscription

{
    //private readonly List<Package> _availablePackages; // List of available packages (replace with your data access logic)
    IMongoCollection<Patient> _collection;
    private readonly IPatientService _patientService;
    private readonly IMapper _mapper;


    public SubscriptionService(IOptions<MongoDBSettings> options,IPatientService patientService, IMapper mapper):base(options) // Constructor to inject available packages
    {
        
        _collection = GetCollection<Patient>("Patients");
        _patientService = patientService;
        _mapper = mapper;
    }
public async Task UpdatePatientSubscriptionInSystemAsync(Patient patient)
{
    try
    {
        var filter = Builders<Patient>.Filter.Eq(p => p.Id, patient.Id); // Filter to find the patient document
        var update = Builders<Patient>.Update.Set(p => p.CurrentBalance, patient.CurrentBalance);
        // .Set(p => p.CurrentBalance.Status, SubscriptionStatus.Active); // Update package and status

        var updateResult = await _collection.UpdateOneAsync(filter, update);

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating patient subscription: {ex.Message}");
    }
}

    public async Task<bool> SubscribePatientToTelemedicine(string patientId, double amountInBirr)
    {
        var (status, message,patientDto) =  await _patientService.GetpatientrById(patientId);
        var patient = _mapper.Map<Patient>(patientDto);

        // Initiate payment transaction
        var transactionRequest = new TransactionRequest
        {
            Amount = amountInBirr,
            Currency = "ETB",           // ... other transaction details
        };
        var paymentSuccessful = await ProcessPayment(transactionRequest);

        if (paymentSuccessful)
        {
            // var package = new Package() {AmountInBirr = amountInBirr}
            // Update patient's subscription details
            patient.CurrentBalance += amountInBirr;
            // patient.CurrentPackage.Status = SubscriptionStatus.Active; // Set package as active
            await UpdatePatientSubscriptionInSystemAsync(patient); // Update patient info asynchronously

            return true;
        }
        else
        {
            // Handle payment failure
            return false;
        }
    }

        public async Task<bool> ProcessPayment(TransactionRequest request)
    {
        try
        {
            // Use ChapaApiHelper to initiate the transaction
            var chapaApiHelper = new ChapaApiHelper("CHASECK_TEST-VFg0DUe9gvFKHjFf4OM8ZravzJsPzMOb");
            var response = await chapaApiHelper.InitiateTransaction(request);

            // Check for successful response
            if (response.IsSuccessStatusCode)
            {
                // to further process the response here
                //to Extract the payment link or reference from the response content
                
                return true; // Indicate successful payment processing
            }
            else
            {
                // Handle unsuccessful response
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error processing payment: {errorContent}");
                return false; // Indicate payment processing failure
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error calling Chapa API: {ex.Message}");
            return false; // Indicate payment processing failure
        }
    }

    public Task<bool> SubscribePatientToTelemedicine(int patientId, int packageId, ServiceProviderType desiredType, Patient patient)
    {
        throw new NotImplementedException();
    }

    public bool HasActiveTelemedicinePackage(int patientId, ServiceProviderType desiredType)
    {
        throw new NotImplementedException();
    }
}
