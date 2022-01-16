using Amazon.SQS.Model;
using Newtonsoft.Json;
using SQSSimple.Domain.Class;
using SQSSimple.Domain.Helper;
using SQSSimple.Domain.Service;

namespace SQSSimple.Business.Service;

public class AWSSQSService:IAWSSQSService
{
    private readonly IAWSSQSHelper _awsSqsHelper;  
    public AWSSQSService(IAWSSQSHelper awsSQSHelper)  
    {  
        this._awsSqsHelper = awsSQSHelper;  
    }  
    public async Task<bool> PostMessageAsync(User user)
    {
        var userDetail = new UserDetail
        {
            ID = new Random().Next(999999999),
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            EmailId = user.EmailId,
            CreaDateTime = DateTime.UtcNow,
            UpdateDateTime = DateTime.UtcNow
        };
        return await _awsSqsHelper.SendMessageAsync(userDetail);
    }  
    public async Task<List<AllMessage>> GetAllMessagesAsync()  
    {  
        List<AllMessage> allMessages;
        var messages = await _awsSqsHelper.ReceiveMessageAsync();  
        allMessages = messages
            .Select(c => new AllMessage
            {
                MessageID = c.MessageId, ReceiptHandle = c.ReceiptHandle, UserDetail = JsonConvert.DeserializeObject<UserDetail>(c.Body)
            }).ToList();  
        return allMessages;
    }  
  
    public async Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage)
    {
        return await _awsSqsHelper.DeleteMessageAsync(deleteMessage.ReceiptHandle);
    } 
}