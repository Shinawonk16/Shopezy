namespace Application.Dtos;

public class PaymentDto
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }
    public CustomerDto CustomerDto { get; set; }
    public string ResponseContent { get; set; }
}
public class CreatePaymentRequestModel
{
    public int Amount { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public CustomerDto CustomerDto { get; set; }
    public OrderDto OrderDto { get; set; }
}

public class UpdatePaymentRequestModel
{
    public bool Successful { get; set; }
}