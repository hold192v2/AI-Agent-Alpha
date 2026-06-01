using System.ComponentModel;

namespace Meet.Application.Dtos;

public class GetMeetingInfo
{
    public GetMeetingInfo(Guid meetingId, string? description, DateTime startedAt, int durationInMinutes, List<UserMeetingInfo>? userInfo = null,  List<ProrocolInfo>? prorocolInfo = null)
    {
        Id = meetingId;
        Description = description ?? "Неизвестная встреча";
        StartedAt = startedAt;
        DurationInMinutes = durationInMinutes;
        UserInfo = userInfo;
        ProrocolInfo = prorocolInfo;
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
    public List<ProrocolInfo>? ProrocolInfo { get; private set; }
    
}

public abstract record UserMeetingInfo(
    [Description("Id Пользователя")] Guid Id, 
    [Description("Фамилия пользователя")] string Surname, 
    [Description("Имя пользователя")] string Name, 
    [Description("Ссылка на фотография пользователя")] string PhotoUrl);

public abstract record ProrocolInfo(
    [Description("Id Проотокола")] Guid Id, 
    [Description("Название протокола")] string Name, 
    [Description("Краткое описание протокола")] string Description, 
    [Description("Дата создания протокола")] DateTime CreatedAt);
