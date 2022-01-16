using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SQSSimple.Domain.Class;
using SQSSimple.Domain.Helper;

namespace SQSSimple.Business.Helper;

public class AWSSQSHelper : IAWSSQSHelper
{
    private readonly IAmazonSQS _sqs;
    private readonly ServiceConfiguration _settings;

    public AWSSQSHelper(
        IAmazonSQS sqs,
        IOptions<ServiceConfiguration> settings)
    {
        this._sqs = sqs;
        this._settings = settings.Value;
    }

    public async Task<bool> SendMessageAsync(UserDetail userDetail)
    {
        var message = JsonConvert.SerializeObject(userDetail);
        var sendRequest = new SendMessageRequest(_settings.AWSSQS.QueueUrl, message);
        // Post message or payload to queue  
        var sendResult = await _sqs.SendMessageAsync(sendRequest);

        return sendResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }

    public async Task<List<Message>> ReceiveMessageAsync()
    {
        //Create New instance  
        var request = new ReceiveMessageRequest
        {
            QueueUrl = _settings.AWSSQS.QueueUrl,
            MaxNumberOfMessages = 10,
            WaitTimeSeconds = 5
        };
        //CheckIs there any new message available to process  
        var result = await _sqs.ReceiveMessageAsync(request);

        return result.Messages.Any() ? result.Messages : new List<Message>();
    }

    public async Task<bool> DeleteMessageAsync(string messageReceiptHandle)
    {
        //Deletes the specified message from the specified queue  
        var deleteResult = await _sqs.DeleteMessageAsync(_settings.AWSSQS.QueueUrl, messageReceiptHandle);
        return deleteResult.HttpStatusCode == System.Net.HttpStatusCode.OK;
    }
}