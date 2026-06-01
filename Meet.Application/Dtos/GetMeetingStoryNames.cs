using System.ComponentModel;

namespace Meet.Application.Dtos;

public class GetMeetingStoryNames
{
    public GetMeetingStoryNames(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    [Description("Id встречи")]
    public Guid Id { get; private  set; }
    [Description("Краткое название встречи")]
    public string Name { get; private set; }
    
}