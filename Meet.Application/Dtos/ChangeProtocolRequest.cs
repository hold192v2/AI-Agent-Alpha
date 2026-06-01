namespace Meet.Application.Dtos;

public record ChangeProtocolRequest(Guid ProtocolId, string Name, string Description);