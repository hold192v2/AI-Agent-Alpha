using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.MeetingStoryNames;

public record MeetingStoryNamesRequest(string UserEmail, Guid UserId): IRequest<Response<GetMeetingStoryNames>>;