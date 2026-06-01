using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meet.Application.Dtos;

public class KonturMeetingRequest
{
    [Required]
    [Description("Id встречи из Контур.Толка")]
    public Guid KonturMeetingId{ get; init; }
    [Description("Id существующей встречи, к которой необходимо привязать встречу из Контур.Толка")]
    public Guid? MeetingId{ get; init; }
}