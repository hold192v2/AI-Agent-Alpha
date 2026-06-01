using System.ComponentModel;

namespace Meet.Application.Dtos;

public class CreateEmptyMeeting
{
    public CreateEmptyMeeting()
    {
        MeetingId = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }
    [Description("Id созданной встречи")]
    public Guid MeetingId { get; private set; }
    [Description("Дата создания")]
    public DateTime CreatedAt { get; private set; }
}