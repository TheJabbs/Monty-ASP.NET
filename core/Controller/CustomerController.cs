using api.core.Dtos.Customer;
using api.core.Service;
using Microsoft.AspNetCore.Mvc;

namespace api.core.Controller;

[Route("api/Customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;
    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] PostCustomerDto createCustomerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _customerService.CreateCustomerAsync(createCustomerDto);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _customerService.UpdateCustomerAsync(updateCustomerDto);
        if (response.Success)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
    
    [HttpGet("/{custId}")]
    public async Task<IActionResult> GetCustomer(int custId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _customerService.GetCustomerByIdAsync(custId);
        if (response != null)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCustomers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _customerService.GetAllCustomersAsync();
        if (response != null && response.Any())
        {
            return Ok(response);
        }
        return NotFound("No customers found.");
    }
    
    [HttpDelete("/{custId}")]
    public async Task<IActionResult> DeleteCustomer(int custId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var response = await _customerService.DeleteCustomerAsync(custId);
        if (response)
        {
            return Ok(response);
        }
        return NotFound(response);
    }
    
}