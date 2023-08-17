using Application.Dtos;

namespace Application.Abstractions.IServices;


public interface IPaymentService
{
    Task<string> InitiatePayment(CreatePaymentRequestModel model, string customerId, string orderId);
    Task<string> GetTransactionRecieptAsync(string transactionReference);
    Task<string> VerifyPayment(UpdatePaymentRequestModel model, string customerId);
}
