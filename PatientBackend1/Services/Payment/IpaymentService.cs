using patientBacken1.Models;

public interface IChapaPaymentService
{
    Task<HttpResponseMessage> InitiateTransaction(TransactionRequest request);
    
}