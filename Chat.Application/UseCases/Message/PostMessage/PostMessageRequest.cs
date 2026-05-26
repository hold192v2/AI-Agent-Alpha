using Chat.Application.HandlerResponse;
using MediatR;

namespace Chat.Application.UseCases.Message.PostMessage;

public record PostMessageRequest(Guid? Chatid, string Text, string? Role, string? TeamId): IRequest<Response>;