using System.ComponentModel;

namespace Chat.Application.Dtos;

public class GetChatStoryNamesDto
{
    public GetChatStoryNamesDto(Guid id, Guid teamId, string name)
    {
        Id = id;
        TeamId = teamId;
        Name = name;
    }
    [Description("ID чата")]
    public Guid Id { get; private  set; }
    [Description("ID команды, привязанной к этому чату")]
    public Guid TeamId { get; private  set; }
    [Description("Название чата, содержит краткое описание чата")]
    public string Name { get; private set; }
}