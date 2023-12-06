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
    [Route("/order/all")]
    public ResponseDto GetAllOrder()
    {
        return new ResponseDto()
        {
            MessageToClient = "Succesfully got all Order",
            ResponseData = _orderService.GetAllOrder()
        };
    }

    [HttpPost]
    [ValidateModel]
    [Route("/order")]
    public ResponseDto postOrder([FromBody] Order order)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
            ResponseData = _orderService.CreateOrder(order.user_id)
        };
    }
    
    [HttpDelete]
    [ValidateModel]
    [Route("/order/{order_id}")]
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