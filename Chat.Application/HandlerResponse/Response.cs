using Chat.Application.Dtos;

namespace Chat.Application.HandlerResponse;

public class Response
{
    public string Message { get; set; }
    public int Status { get; set; }
    public List<GetChatMessagesDto>? ChatMessages { get; set; }
    public List<GetChatStoryNamesDto>? ChatStoryNames { get; set; }
    
    public Response(string message, int  status)
    {
        Message = message;
        Status = status;
    }

    public Response(string message, int status, List<GetChatMessagesDto> chatMessages)
    {
        Message = message;
        Status = status;
        ChatMessages = chatMessages;
    }

    public Response(string message, int status, List<GetChatStoryNamesDto> chatStoryNames)
    {
        Message = message;
        Status = status;
        ChatStoryNames = chatStoryNames;
    }
}