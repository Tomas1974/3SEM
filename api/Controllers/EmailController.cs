using api.TransferModels;
using Microsoft.AspNetCore.Mvc;
using service.Services;

namespace api.Controllers;

public class EmailController
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }
 
    [HttpPost]
    [Route("api/email/{order_id}")]
    public ResponseDto SendEmail([FromRoute] int order_id)
    { 
        _emailService.SendEmail(order_id);
        
        return new ResponseDto()
        {
            MessageToClient = "Successfully created an order",
        }; 
    }
}