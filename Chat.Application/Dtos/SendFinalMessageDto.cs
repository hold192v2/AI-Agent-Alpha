using System.ComponentModel;

namespace Chat.Application.Dtos;

public class SendFinalMessageDto
{
    [Description("ID выполненной сессии")]
    public Guid SessionId { get; set; }
    
    [Description("Ответ нейросети")]
    public string Answer { get; set; } = "";
    
    [Description("Флаг, показывает, является ли ответ дайджестом")]
    public bool IsDigest { get; set; } = false;
}