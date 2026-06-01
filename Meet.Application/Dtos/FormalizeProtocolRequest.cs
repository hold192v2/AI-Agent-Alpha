using System.ComponentModel;

namespace Meet.Application.Dtos;

public record FormalizeProtocolRequest([Description("Старое содержание протокола")] string OldProtocolDesc);