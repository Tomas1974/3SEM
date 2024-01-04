using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class CustomerBuyController : Controller
{
    private readonly CustomerBuyService _customerBuyService;

    public CustomerBuyController(CustomerBuyService customerBuyService)
    {
        _customerBuyService = customerBuyService;
    }

    /*
     * Gets all customer buys from the database. 
     */
    [HttpGet]
    [Route("/customerbuy/all")]
    [Authorize]
    public ResponseDto GetAllCustomerBuy()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got ",
            ResponseData = _customerBuyService.GetAllCustomerBuy()
        };
    }

    /*
     * Creates a new customer buy in the database. 
     */
    [HttpPost]
    [Route("/customerbuy")]
    [Authorize]
    public ResponseDto postCustomerBuy([FromBody] CustomerBuyModel customerbuy)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Succesfully created an customerbuy ", 
            ResponseData = _customerBuyService.CreateCustomerBuy(customerbuy)
        };
    }
    
    /*
     * Deletes an existing customer buy from the database.
     */
    [HttpDelete]
    [Route("/customerbuy/{customer_buy_id}")]
    [Authorize]
    public void deleteCustomerBuy([FromRoute] int customer_buy_id)
    {
        _customerBuyService.deleteCustomerBuy(customer_buy_id);
        new ResponseDto()
        {
            MessageToClient = "Successfully deleted customerbuy"
        };
    }
}