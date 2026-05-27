using Chat.Application.HandlerResponse;
using MediatR;

namespace Chat.Application.UseCases.Message.GetMessage;

public record GetMessageRequest(Guid SessionId): IRequest<Response>;