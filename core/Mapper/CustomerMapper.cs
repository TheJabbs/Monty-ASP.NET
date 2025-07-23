using api.core.Dtos.Customer;
using api.Models;

namespace api.core.Mapper;

public class CustomerMapper
{
    public static Customer PostCustomerDtoToCustomer(PostCustomerDto customerDto)
    {
        return new Customer()
        {
            CustFullName = customerDto.CustFullName,
            Email = customerDto.Email,
            Phone = customerDto.Phone,
            Address = customerDto.Address,
            Dob = customerDto.Dob,
            PasswordHash = "",
        };
    }

    public static GetCustomerDto CustomerToGetCustomerDto(Customer customer)
    {
        return new GetCustomerDto()
        {
            CustId = customer.CustId,
            CustFullName = customer.CustFullName,
            Email = customer.Email,
            Phone = customer.Phone,
            Address = customer.Address,
            Dob = customer.Dob
        };
    }
    
    public static Customer UpdateCustomerDtoToCustomer(UpdateCustomerDto customerDto, Customer customer)
    {
        if (customerDto.CustFullName != null)
            customer.CustFullName = customerDto.CustFullName;
        if (customerDto.Email != null)
            customer.Email = customerDto.Email;
        if (customerDto.Phone != null)
            customer.Phone = customerDto.Phone;
        if (customerDto.Address != null)
            customer.Address = customerDto.Address;
        if (customerDto.PasswordHash != null)
            customer.PasswordHash = customerDto.PasswordHash;

        return customer;
    }
}