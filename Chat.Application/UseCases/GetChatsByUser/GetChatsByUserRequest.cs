using Chat.Application.HandlerResponse;
using MediatR;

namespace Chat.Application.UseCases.GetChatsByUser;

public record GetChatsByUserRequest(Guid UserId): IRequest<Response>;