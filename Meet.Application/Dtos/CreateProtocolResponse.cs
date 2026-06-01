using System.ComponentModel;

namespace Meet.Application.Dtos;

public record CreateProtocolResponse(
    [Description("Id протокола")] 
    Guid ProtocolId, 
    [Description("Дата сохранения")] 
    DateTime CreationTime);
