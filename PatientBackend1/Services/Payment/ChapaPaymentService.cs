

using System.Configuration;
using Microsoft.AspNetCore.Mvc;

// 
using ChapaNET;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ChapaApiHelper
{
    private readonly string _chapaApiKey;

    public ChapaApiHelper(string chapaApiKey)
    {
        _chapaApiKey = chapaApiKey;
    }

    public async Task<HttpResponseMessage> InitiateTransaction(TransactionRequest request)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _chapaApiKey);

        var url = "https://api.chapa.co/v1/transaction/initialize";
        var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

        return await client.PostAsync(url, content);
    }
}

public class TransactionRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public string Email { get; set; }
    // Other transaction details as required by Chapa
}




