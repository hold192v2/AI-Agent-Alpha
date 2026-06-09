using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.Protocol.Post;

public record CreateProtocolRequest(string Name, string Description):  IRequest<Response<CreateProtocolResponse>>;