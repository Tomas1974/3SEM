using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
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

    [HttpGet]
    [Route("/customerbuy/all")]
    public ResponseDto GetAllCustomerBuy()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got ",
            ResponseData = _customerBuyService.GetAllCustomerBuy()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/customerbuy")]
    public ResponseDto postCustomerBuy([FromBody] CustomerBuy customerbuy)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Succesfully created an customerbuy ", 
            ResponseData = _customerBuyService.CreateCustomerBuy(customerbuy)
        };
    }
    
    [HttpDelete]
    [ValidateModel]
    [Route("/customerbuy/{customer_buy_id}")]
    public void deleteCustomerBuy([FromRoute] int customer_buy_id)
    {
        _customerBuyService.deleteCustomerBuy(customer_buy_id);
        new ResponseDto()
        {
            MessageToClient = "Successfully deleted customerbuy"
        };
    }
}