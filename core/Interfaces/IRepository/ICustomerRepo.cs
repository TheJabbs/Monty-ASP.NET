using api.core.Dtos.Customer;
using api.core.Dtos.Shared;
using api.Models;

namespace api.core.Interfaces.IRepository;

public interface ICustomerRepo
{
    Task<ResponseDto<GetCustomerDto>> CreateCustomerAsync(Customer customer);
    Task<ResponseDto<GetCustomerDto>> UpdateCustomerAsync(Customer customer);
    Task<bool> DeleteCustomerAsync(int id);
    Task<GetCustomerDto?> GetCustomerByIdAsync(int id);
    Task<Customer?> GetCustomerRawByIdAsync(int id);

    Task<List<GetCustomerDto>?> GetAllCustomersAsync();
    Task<GetCustomerDto?> GetCustomerByContactAsync(string email, string phone);
}