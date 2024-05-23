using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using patientBackend1.Services;
using patientBackend1.Services.UserServices;
using PatientBackend1.Models;
using PatientBackend1.Services.PatientServices;
using PatientBackend1.utils;

public class SubscriptionService:MongoDBService

{
    private readonly List<Package> _availablePackages; // List of available packages (replace with your data access logic)
    IMongoCollection<Patient> _collection;
    private readonly IPatientService _patientService;


    public SubscriptionService(IOptions<MongoDBSettings> options,List<Package> availablePackages,IPatientService patientService):base(options) // Constructor to inject available packages
    {
        _availablePackages = availablePackages;
        _collection = GetCollection<Patient>("Patients");
        _patientService = patientService;
    }
public async Task UpdatePatientSubscriptionInSystemAsync(Patient patient)
{
    try
    {
        var filter = Builders<Patient>.Filter.Eq(p => p.Id, patient.Id); // Filter to find the patient document
        var update = Builders<Patient>.Update.Set(p => p.CurrentPackage, patient.CurrentPackage)
                                            .Set(p => p.CurrentPackage.Status, SubscriptionStatus.Active); // Update package and status

        var updateResult = await _collection.UpdateOneAsync(filter, update);

        // if (updateResult.IsModifiedCountZero)
        // {
        //     Console.WriteLine("Failed to update patient subscription in database.");
        // }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error updating patient subscription: {ex.Message}");
    }
}

    public async Task<bool> SubscribePatientToTelemedicine(string patientId, string packageId, ServiceProviderType desiredType,Patient patient)
    {
        //var patient =  await _patientService.GetpatientrById(patientId);
        var package = _availablePackages.FirstOrDefault(p => p.Id == packageId);

        if ( package == null)
        {
            return false;
        }

        // Validate chosen service provider type against the package
        if (package.AllowedProviderType != desiredType)
        {
            return false;
        }

        // Initiate payment transaction
        var transactionRequest = new TransactionRequest
        {
            Amount = package.Price,
            Currency = "ETB",           // ... other transaction details
        };
        var paymentSuccessful = await ProcessPayment(transactionRequest);

        if (paymentSuccessful)
        {
            // Update patient's subscription details
            patient.CurrentPackage = package;
            patient.CurrentPackage.Status = SubscriptionStatus.Active; // Set package as active
            await UpdatePatientSubscriptionInSystemAsync(patient); // Update patient info asynchronously

            return true;
        }
        else
        {
            // Handle payment failure
            return false;
        }
    }

    // public bool HasActiveTelemedicinePackage(int patientId, ServiceProviderType desiredType,Patient patient)
    // {
        


        
    //     if (patient == null || patient.CurrentPackage == null)
    //     {
    //         return false;
    //     }

    //     // Ensure package is active
    //     if ((patient.CurrentPackage.Status & SubscriptionStatus.Active) == 0)
    //     {
    //         return false;
    //     }

    //     // // Check package expiry
    //      var isExpired = DateTime.UtcNow >= patient.CurrentPackage.StartDate.AddMonths(patient.CurrentPackage.DurationInMonths);

    //     // // Check if the chosen service provider type is allowed by the package
    //      return !isExpired && patient.CurrentPackage.AllowedProviderType == desiredType;
    // }

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
}
