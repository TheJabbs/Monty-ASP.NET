using api.core.Dtos.Customer;
using api.core.Dtos.Shared;
using api.core.Interfaces.IRepository;
using api.core.Mapper;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.core.Repository;

public class CustomerRepo : ICustomerRepo
{
    private readonly ApplicationDbContext _context;

    public CustomerRepo(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ResponseDto<GetCustomerDto>> CreateCustomerAsync(Customer customer)
    {   
        try
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            var cust = CustomerMapper.CustomerToGetCustomerDto(customer);
            return new ResponseDto<GetCustomerDto>()
            {
                Success = true,
                Message = "Customer created successfully",
                Data = cust
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<GetCustomerDto>()
            {
                Success = false,
                Message = $"Error creating customer: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<ResponseDto<GetCustomerDto>> UpdateCustomerAsync(Customer customer)
    {
        try
        {
            await _context.SaveChangesAsync();

            var custDto = CustomerMapper.CustomerToGetCustomerDto(customer);
            return new ResponseDto<GetCustomerDto>
            {
                Success = true,
                Message = "Customer updated successfully",
                Data = custDto
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<GetCustomerDto>
            {
                Success = false,
                Message = $"Error updating customer: {ex.Message}",
                Data = null
            };
        }
    }

    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return false; 
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        return true; 
    }

    public async Task<GetCustomerDto?> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers
            .Where(c => c.CustId == id)
            .Select(c => CustomerMapper.CustomerToGetCustomerDto(c))
            .FirstOrDefaultAsync();

        return customer;
    }

    public async Task<GetCustomerDto?> GetCustomerByContactAsync(string email, string phone)
    {
        var customer = await _context.Customers
            .Where(c => c.Email == email || c.Phone == phone)
            .Select(c => CustomerMapper.CustomerToGetCustomerDto(c))
            .FirstOrDefaultAsync();

        return customer;
    }


    public async Task<List<GetCustomerDto>?> GetAllCustomersAsync()
    {
        var customers = await _context.Customers
            .Select(c => CustomerMapper.CustomerToGetCustomerDto(c))
            .ToListAsync();

        return customers;
    }
    public async Task<Customer?> GetCustomerRawByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }
}