using MediatR;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.CreateMeeting.Empty;

public record CreateEmptyMeetingRequest(): IRequest<Response<string>>;