using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

public class TalkUser
{
    [JsonPropertyName("userType")]
    public TalkUserType UserType { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("hasEmail")]
    public bool HasEmail { get; set; }

    [JsonPropertyName("firstname")]
    public string Name { get; set; }

    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [JsonPropertyName("patronymic")]
    public string Patronymic { get; set; }

    [JsonPropertyName("department")]
    public string Department { get; set; }

    [JsonPropertyName("disabled")]
    public bool IsDisabled { get; set; }

    [JsonPropertyName("deletedBySelf")]
    public bool IsDeletedBySelf { get; set; }
    
    [JsonPropertyName("roles")]
    public List<string> Roles { get; set; }

    [JsonPropertyName("roleInfos")]
    public List<TalkRoleInfo> RoleInfos { get; set; }
    
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("post")]
    public string Post { get; set; }

    [JsonPropertyName("avatarUrl")]
    public string AvatarUrl { get; set; }

    [JsonPropertyName("profileUrl")]
    public string ProfileUrl { get; set; }

    [JsonPropertyName("hasMobilePhone")]
    public bool HasMobilePhone { get; set; }

    [JsonPropertyName("hasInnerPhone")]
    public bool HasInnerPhone { get; set; }

    [JsonPropertyName("avatarInfo")]
    public TalkAvatarInfo AvatarInfo { get; set; }

    [JsonPropertyName("status")]
    public TalkUserStatus Status { get; set; }

    [JsonPropertyName("removed")]
    public bool IsRemoved { get; set; }

    [JsonPropertyName("hidden")]
    public bool IsHidden { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("features")]
    public List<string> Features { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; }

    [JsonPropertyName("innerPhone")]
    public string InnerPhone { get; set; }
}