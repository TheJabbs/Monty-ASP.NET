namespace api.core.Dtos.Customer;

public class GetCustomerDto
{
    public int CustId { get; set; }
    
    public string CustFullName { get; set; } = "";
    
    public DateTime Dob { get; set; }

    public string Email { get; set; } = "";
    
    public string Phone { get; set; } = "";
    
    public string Address { get; set; } = "";
}