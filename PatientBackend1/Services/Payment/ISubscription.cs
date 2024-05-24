using PatientBackend1.Models;

public interface ISubscription
{
    Task<bool> SubscribePatientToTelemedicine(string patientId, double amountInBirr);
    Task UpdatePatientSubscriptionInSystemAsync(Patient patient);
}
