using System.ComponentModel;

namespace Meet.Application.Dtos;

public class KonturMeetingImport
{
    public KonturMeetingImport(Guid meetingId, string? description, DateTime startedAt)
    {
        KonturMeetingId = meetingId;
        Description = description ?? "Встреча без названия" ;
        StartedAt = startedAt;
    }
    [Description("Id встречи в Контур.Толке")]
    public Guid KonturMeetingId { get; private set; }
    
    [Description("Краткое описание встречи, взятое из Контур.Толка")]
    public string Description { get; private set; }
    
    [Description("Время начала встречи")]
    public DateTime StartedAt { get; private set; }
}    
 