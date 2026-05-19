using System.ComponentModel;

namespace Chat.Application.Dtos;

public class SendChatMessageResponseDto
{
    [Description("ID выполняемой сессии")]
    public Guid SessionId { get; set; }
    [Description("Системное сообщение")]
    public string Message { get; set; } = "Successful";
}