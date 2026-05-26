using Chat.Application.HandlerResponse;
using MediatR;

namespace Chat.Application.UseCases.GetMessageStory;

public record GetMessageStoryRequest(Guid ChatId): IRequest<Response>;