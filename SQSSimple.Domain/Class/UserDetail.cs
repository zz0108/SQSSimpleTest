namespace SQSSimple.Domain.Class;
public class UserDetail:User
{
    /// <summary>
    /// 使用者ID
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 新增時間
    /// </summary>
    public DateTime CreaDateTime { get; set; }
    /// <summary>
    /// 更新時間
    /// </summary>
    public DateTime UpdateDateTime { get; set; }
}