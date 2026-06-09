using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.Formalize;

public record FormalizeRequest(string OldProtocolDesc): IRequest<Response<NewProtocolDesc>>;