using System.Text;
using Application.Abstractions.IRepositories;
using Application.Abstractions.IServices;
using Application.Dtos;
using Domain.Entities;

namespace Persistence.PaymentServices;

public class PaymentService : IPaymentService
{
    private static readonly HttpClient client = new HttpClient();

    const string mySecretKey = "sk_test_e363cf351fa1d32898025e2f96513da3cd091b08";

    private readonly IPaymentRepository _paymentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public PaymentService(IPaymentRepository paymentRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _paymentRepository = paymentRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<string> GetTransactionRecieptAsync(string transactionReference)
    {
          if (transactionReference == null)
        {
            return null;
        }
        var transaction = await _paymentRepository.GetPaymentAsync(transactionReference);
        if (transaction == null)
        {
            return null;    
        }
        string url = $"https://api.paystack.co/transaction/verify/{transactionReference}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {mySecretKey}");
        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            // Transaction verification successful
            // Process the response content, which includes payment details

            // Deserialize the response JSON
            // var transaction = JsonConvert.DeserializeObject<dynamic>(responseContent);
            // Console.WriteLine(transaction);
            // Access the receipt information
            // var receiptUrl = transaction.data.receipt.url;
            // var receiptNumber = transaction.data.receipt.number;

            // Do further processing with the receipt information
            var transactionDto = new PaymentDto
            {
                CustomerDto = new CustomerDto
                {
                    Users = new UserDto
                    {
                        PhoneNumber = transaction.Customer.User.PhoneNumber,
                        FirstName = transaction.Customer.User.FirstName,
                        LastName = transaction.Customer.User.LastName,
                        Email = transaction.Customer.User.Email,
                        
                        
                        
                    }
                },
                ResponseContent = responseContent,
            };
            return responseContent;
        }
        else
        {
            // Transaction verification failed
            throw new Exception($"Transaction verification failed. Response: {responseContent}");
        }
    }

    public async Task<string>  InitiatePayment(CreatePaymentRequestModel model, string customerId, string orderId)
    {
        var customer = _customerRepository.GetAsync(customerId);
            if (customer is null)
            {
                return null;
            }
            string url = "https://api.paystack.co/transaction/initialize";

            // Set reciever account details
            var recipients = new
            {
                account_number = "7054770135",
                bank_code = "652",  // Bank code for the receiver's bank (e.g., GTBank)
                                    // Add any other recipient details as required
            };
            var get = await _orderRepository.GetOrderAsync(orderId);
            var payload = new
            {
                // quantity = get.
                amount = model.Amount * 100,  // Set the amount in kobo (e.g., 5000 = ₦5000)
                email = model.Email,
                phone = model.PhoneNumber,
                reference = Guid.NewGuid().ToString(),
                // callback_url = $"http://127.0.0.1:5501/FrontEnd/dashboard/receipt.html?id={orderId}",
                first_name = model.CustomerDto.Users.FirstName[0],
                last_name = model.CustomerDto.Users.LastName[1]

                // recipient = recipients,
                // card = new
                // {
                //     number = "4084084084084081",  // Card number
                //     cvv = "123",                  // CVV
                //     expiry_month = "01",          // Expiry month
                //     expiry_year = "24"            // Expiry year
                // }
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

        // Create the HTTP request
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        request.Headers.Add("Authorization", $"Bearer {mySecretKey}");

        // Send the request and retrieve the response
        var response = await client.SendAsync(request);
        var responseContent = await response.Content.ReadAsStringAsync();

        // Process the response
        if (response.IsSuccessStatusCode)
        {
            // Payment initiation successful
            var payment = new Payment
            {
                OrderId = model.OrderDto.Id,
                CustomerId = model.CustomerDto.Users.Id,
                ReferenceNumber = responseContent.Split("\"reference\":")[1].Split("\"")[1],
            };
            await _paymentRepository.CreateAsync(payment);
            await _paymentRepository.SaveAsync();
            return responseContent;
        }
        else
        {
            // Payment initiation failed
            throw new Exception($"Payment initiation failed. Response: {responseContent}");
        }
       
    }

    public Task<string> VerifyPayment(UpdatePaymentRequestModel model, string customerId)
    {
        throw new NotImplementedException();
    }
}
