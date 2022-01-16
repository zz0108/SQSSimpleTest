using Amazon.SQS.Model;
using SQSSimple.Domain.Class;

namespace SQSSimple.Domain.Helper;

public interface IAWSSQSHelper
{
    Task<bool> SendMessageAsync(UserDetail userDetail);  
    Task<List<Message>> ReceiveMessageAsync();  
    Task<bool> DeleteMessageAsync(string messageReceiptHandle);  

}