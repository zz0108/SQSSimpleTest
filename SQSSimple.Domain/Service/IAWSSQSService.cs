using SQSSimple.Domain.Class;

namespace SQSSimple.Domain.Service;

public interface IAWSSQSService
{
    Task<bool> PostMessageAsync(User user);  
    Task<List<AllMessage>> GetAllMessagesAsync();  
    Task<bool> DeleteMessageAsync(DeleteMessage deleteMessage);  
}