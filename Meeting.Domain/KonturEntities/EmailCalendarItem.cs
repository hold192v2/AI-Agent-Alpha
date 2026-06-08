using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class EmailCalendarItem
{
    [JsonPropertyName("calendarSource")]
    public string CalendarSource { get; set; }
    [JsonPropertyName("busyType")]
    public CalendarItemBusyType BuseType { get; set; }
    [JsonPropertyName("subject")]
    public string Subject { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("start")]
    public DateTime Start { get; set; }
    [JsonPropertyName("end")]
    public DateTime End { get; set; }
    [JsonPropertyName("location")]
    public string Location { get; set; }
    [JsonPropertyName("locationAttendee")]
    public EmailCalendarItemAttendee LocationAttendee { get; set; }
    [JsonPropertyName("organizer")]
    public EmailCalendarItemAttendee Organizer { get; set; }
    [JsonPropertyName("requiredAttendees")]
    public List<EmailCalendarItemAttendee> RequiredAttendees { get; set; }
    [JsonPropertyName("optionalAttendees")]
    public List<EmailCalendarItemAttendee> OptionalAttendees { get; set; }
    [JsonPropertyName("isAllDayEvent")]
    public bool IsAllDayEvent { get; set; }
    [JsonPropertyName("isCancelled")]
    public bool IsCancelled { get; set; }
    [JsonPropertyName("enableAutoRecording")]
    public bool EnableAutoRecording { get; set; }
    [JsonPropertyName("onlineUsersCount")]
    public int OnlineUsersCount { get; set; }
    [JsonPropertyName("onlineUsers")]
    public List<TalkUserRef>  OnlineUsers { get; set; }
    [JsonPropertyName("roomName")]
    public string RoomName { get; set; }
    [JsonPropertyName("stream")]
    public TalkRoomStream Stream { get; set; }
    [JsonPropertyName("room")]
    public TalkRoom Room { get; set; }
    [JsonPropertyName("onlineMeetingUrl")]
    public string OnlineMeetingUrl { get; set; }
    [JsonPropertyName("externalMeeting")]
    public EmailExternalMeeting ExternalMeeting { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("isRecurring")]
    public bool IsRecurring { get; set; }
    [JsonPropertyName("recurrence")]
    public TalkCalendarRecurrence Recurrence { get; set; }
    [JsonPropertyName("isPrivate")]
    public bool IsPrivate { get; set; }
}