using MediatR;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.CreateMeeting.Kontur;

public record CreateMeetingByKontureRequest(Guid? MeetingId, string KonturId, string userEmail):  IRequest<Response<string>>;