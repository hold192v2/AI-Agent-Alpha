using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.Protocol.Put;

public record ChangeProtocolRequest(Guid ProtocolId, string Name, string Description): IRequest<Response<PutProtocolResponse>>;