using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.ProtocolInfo;

public record ProtocolInfoRequest(Guid Id): IRequest<Response<GetProtocolInfo>>;