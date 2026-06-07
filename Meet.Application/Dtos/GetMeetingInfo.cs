using System.ComponentModel;
using Meeting.Domain.Entities;

namespace Meet.Application.Dtos;

public class GetMeetingInfo
{
    public GetMeetingInfo(Guid meetingId, string? description, DateTime startedAt, int durationInMinutes, List<UserMeetingInfo>? userInfo = null,  List<ProtocolInfo>? protocolInfo = null)
    {
        Id = meetingId;
        Description = description ?? "Неизвестная встреча";
        StartedAt = startedAt;
        DurationInMinutes = durationInMinutes;
        UserInfo = userInfo;
        ProtocolInfo = protocolInfo;
    }
    
    [Description("Id встречи")]
    public Guid Id { get; private set; }
    
    [Description("Описание встречи")]
    public string Description { get; private set; }
    
    [Description("Начало встречи")]
    public DateTime StartedAt { get; private set;}
    
    [Description("Пользователи, участвовавшие в встрече")]
    public List<UserMeetingInfo>? UserInfo { get; private set; }
    
    [Description("Длительность встречи в минуитах")]
    public int DurationInMinutes { get; private set; }
    
    [Description("Протоколы встречи")]
    public List<ProtocolInfo>? ProtocolInfo { get; private set; }
    
}