using Microsoft.AspNetCore.Mvc;

public class PaymentController : Controller
{
    private readonly ChapaApiHelper _chapaApiHelper;

    public PaymentController(ChapaApiHelper chapaApiHelper)
    {
        _chapaApiHelper = chapaApiHelper;
    }

    public async Task<IActionResult> InitiatePayment(TransactionRequest request)
    {
        var response = await _chapaApiHelper.InitiateTransaction(request);

        if (response.IsSuccessStatusCode)
        {
            // Handle successful response (e.g., redirect to checkout page)
            return Redirect("Google.com");
        }
        else
        {
            // Handle error
            return BadRequest("Error initiating transaction");
        }
    }
}
