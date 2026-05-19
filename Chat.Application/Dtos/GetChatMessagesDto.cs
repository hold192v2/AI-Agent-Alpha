using System.ComponentModel;

namespace Chat.Application.Dtos;

public class GetChatMessagesDto
{
    public GetChatMessagesDto(Guid id, Guid? userId, string content, bool isDigestMessage)
    {
        Id = id;
        SenderId = userId;
        Content = content;
        IsDigestMessage = isDigestMessage;
    }
    public Guid Id { get; private set; }
    [Description("Id пользователя. Null - ответ ИИ")]
    public Guid? SenderId { get; private set; }
    public string Content { get; private set; }
    public bool IsDigestMessage { get; private set; }
    
}