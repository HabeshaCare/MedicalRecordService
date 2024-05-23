using PatientBackend1.Models;

public interface ISubscription
{
    Task<bool> SubscribePatientToTelemedicine(int patientId, int packageId, ServiceProviderType desiredType,Patient patient);
    bool HasActiveTelemedicinePackage(int patientId, ServiceProviderType desiredType);

    Task UpdatePatientSubscriptionInSystemAsync(Patient patient);
}
