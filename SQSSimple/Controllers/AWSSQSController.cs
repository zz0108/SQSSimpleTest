using Microsoft.AspNetCore.Mvc;
using SQSSimple.Domain;
using SQSSimple.Domain.Class;
using SQSSimple.Domain.Service;

namespace SQSSimple.Controllers;
[Produces("application/json")]  
[Route("api/[controller]/[action]")]  
[ApiController]  
public class AWSSQSController:ControllerBase
{
    private readonly IAWSSQSService _AWSSQSService;  
  
    public AWSSQSController(IAWSSQSService AWSSQSService)  
    {  
        _AWSSQSService = AWSSQSService;  
    }  
    
    [HttpPost]  
    public async Task<IActionResult> PostMessageAsync([FromBody] User user)  
    {  
        var result = await _AWSSQSService.PostMessageAsync(user);  
        return Ok(new { isSucess = result });  
    }
    [HttpGet]  
    public async Task<IActionResult> GetAllMessagesAsync()  
    {  
        var result = await _AWSSQSService.GetAllMessagesAsync();  
        return Ok(result);  
    }
    [HttpDelete]  
    public async Task<IActionResult> DeleteMessageAsync(DeleteMessage deleteMessage)  
    {  
        var result = await _AWSSQSService.DeleteMessageAsync(deleteMessage);  
        return Ok(new { isSucess = result });  
    } 
}