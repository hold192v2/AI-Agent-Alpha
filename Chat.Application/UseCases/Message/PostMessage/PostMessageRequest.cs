using Chat.Application.HandlerResponse;
using MediatR;

namespace Chat.Application.UseCases.Message.PostMessage;

public record PostMessageRequest(Guid? ChatId, string Text, string? Role, Guid? TeamId, Guid UserId): IRequest<Response>;