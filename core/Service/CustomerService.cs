using api.core.Dtos.Customer;
using api.core.Dtos.Shared;
using api.core.Mapper;
using api.core.Repository;
using api.Exceptions;
using api.utils;

namespace api.core.Service;

public class CustomerService
{
    private readonly CustomerRepo _customerRepo;
    
    public CustomerService(CustomerRepo customerRepo)
    {
        _customerRepo = customerRepo;
    }
    
    public async Task<ResponseDto<GetCustomerDto>> CreateCustomerAsync(PostCustomerDto customerDto)
    {
        var customer = CustomerMapper.PostCustomerDtoToCustomer(customerDto);
        
        var search = await _customerRepo.GetCustomerByContactAsync(customer.Email, customer.Phone);
        
        if (search != null)
        {
            return new ResponseDto<GetCustomerDto>()
            {
                Success = false,
                Message = "Customer already exists",
                Data = null
            };
        }
        
        customer.PasswordHash = Hash.HashPassword(customerDto.Password);
        return await _customerRepo.CreateCustomerAsync(customer);
    }
    
    public async Task<ResponseDto<GetCustomerDto>> UpdateCustomerAsync(UpdateCustomerDto customerDto)
    {
        var existingCustomer = await _customerRepo.GetCustomerRawByIdAsync(customerDto.CustId);
        if (existingCustomer == null)
        {
            return new ResponseDto<GetCustomerDto>()
            {
                Success = false,
                Message = "Customer not found",
                Data = null
            };
        }

        var updatedCustomer = CustomerMapper.UpdateCustomerDtoToCustomer(customerDto, existingCustomer);
        
        if (!string.IsNullOrEmpty(customerDto.PasswordHash))
        {
            updatedCustomer.PasswordHash = Hash.HashPassword(customerDto.PasswordHash);
        }

        return await _customerRepo.UpdateCustomerAsync(updatedCustomer);
    }
    
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var check = await _customerRepo.GetCustomerByIdAsync(id);
        if (check == null)
        {
            throw new NotFound("Customer not found");
        }
        return await _customerRepo.DeleteCustomerAsync(id);
    }
    
    public async Task<GetCustomerDto?> GetCustomerByIdAsync(int id)
    {
        return await _customerRepo.GetCustomerByIdAsync(id);
    }
    
    public async Task<List<GetCustomerDto>?> GetAllCustomersAsync()
    {
        return await _customerRepo.GetAllCustomersAsync();
    }
}