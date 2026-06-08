using System.Text.Json.Serialization;

namespace Meeting.Domain.KonturEntities;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AttendeeType
{
    [JsonPropertyName("user")]
    User,
    [JsonPropertyName("group")]
    Group,
    [JsonPropertyName("person")]
    Person
}