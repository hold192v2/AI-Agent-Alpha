using System.ComponentModel;

namespace Meet.Application.Dtos;

public record EmailProtocolSendRequest(
    [Description("Id протокола")]
    Guid ProtocolId,
    [Description("Cодержание протокола")]
    string ProtocolDesc);