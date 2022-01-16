namespace SQSSimple.Domain.Class;

public class AllMessage
{
    public AllMessage()
    {
        this.UserDetail = new UserDetail();
    }
    /// <summary>
    /// 訊息ID
    /// </summary>
    public string MessageID { get; set; }
    /// <summary>
    /// ReceiptHandle
    /// </summary>
    public string ReceiptHandle { get; set; }
    /// <summary>
    /// 使用者明細
    /// </summary>
    public UserDetail? UserDetail { get; set; }
}