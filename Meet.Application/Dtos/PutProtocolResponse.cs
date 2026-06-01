using System.ComponentModel;

namespace Meet.Application.Dtos;

public record PutProtocolResponse([Description("Id протокола")]Guid  ProtocolId);