using api.Filters;
using api.TransferModels;
using infrastructure.DataModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class OrderController : Controller
{
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    [Route("api/order/all")]
    public ResponseDto GetAllOrder()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all Order",
            ResponseData = _orderService.GetAllOrder()
        };
    }

    [HttpPost]
    //[ValidateModel]
    [Route("api/order/post")]
    public ResponseDto postOrder([FromBody] OrderModel order)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
            ResponseData = _orderService.CreateOrder(order.UserId)
        };
    }
    
    [HttpDelete]
    //[ValidateModel]
    [Route("api/order/{oroder_id}")]
    public void deleteOrder([FromRoute] int order_id)
    {
        HttpContext.Response.StatusCode = 204;
        _orderService.deleteOrder(order_id);
        new ResponseDto()
        {
            MessageToClient = "Succesfully deleted an order"
        };
    }
}