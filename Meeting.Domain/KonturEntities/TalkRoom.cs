using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkRoom
{
    [JsonPropertyName("roomName")]
    public string RoomName { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("sessionHalls")]
    public List<TalkSessionHallInfo> SessionHalls { get; set; }
    [JsonPropertyName("enableLobby")]
    public bool EnableLobby { get; set; }
    [JsonPropertyName("stageConferenceId")]
    public string StageConferenceId { get; set; }
    [JsonPropertyName("moderators")]
    public List<TalkUser> Moderators { get; set; }
    [JsonPropertyName("anonymouseModerators")]
    public List<TalkAnonymouss> AnonymouseModerators { get; set; }
    [JsonPropertyName("allowAnonymous")]
    public bool AllowAnonymouse { get; set; }
    [JsonPropertyName("anonymousAccessExpirationDate")]
    public string AnonymousAccessExpirationDate { get; set; }
    [JsonPropertyName("anonymousAccessModifiedDate")]
    public string AnonymousAccessModifiedDate { get; set; }
    [JsonPropertyName("audioPolicy")]
    public TalkInputPolicy AudioPolicy { get; set; }
    [JsonPropertyName("videoPolicy")]
    public TalkInputPolicy VideoPolicy { get; set; }
    [JsonPropertyName("screenSharePolicy")]
    public TalkInputPolicy ScreenSharePolicy { get; set; }
    [JsonPropertyName("securityType")]
    public TalkRoomSecurityType SecurityType { get; set; }
    [JsonPropertyName("pinCode")]
    public string PinCode { get; set; }
    [JsonPropertyName("isModerator")]
    public bool IsModerator { get; set; }
    [JsonPropertyName("conferenceId")]
    public string ConferenceId { get; set; }
    [JsonPropertyName("sipSettings")]
    public RoomSipSettings SipSettings { get; set; }
    [JsonPropertyName("usersOnline")]
    public int UsersOnline { get; set; }
    [JsonPropertyName("onlineUsers")]
    public List<TalkUserRef> OnlineUsers { get; set; }
    [JsonPropertyName("roomStream")]
    public TalkRoomStream RoomStream { get; set; }
    [JsonPropertyName("simultaneousTranslation")]
    public TalkSimultaneouseTranslation SimultaneousTranslation { get; set; }
    [JsonPropertyName("chatChannelSettings")]
    public TalkChatChannelRoomsSettinds ChatChannelSettings { get; set; }
    [JsonPropertyName("showSpikerInfo")]
    public bool ShowSpikerInfo { get; set; }
    [JsonPropertyName("soundpadEnabled")]
    public bool SoundpadEnabled { get; set; }
    [JsonPropertyName("disableVirtualBackgrounds")]
    public bool DisableVirtualBackgrounds { get; set; }
    [JsonPropertyName("maskingSettings")]
    public TalkMaskingSettings MaskingSettings { get; set; }
    [JsonPropertyName("autoRunDeepFakeDetection")]
    public bool AutoRunDeepFakeDetection { get; set; }
}