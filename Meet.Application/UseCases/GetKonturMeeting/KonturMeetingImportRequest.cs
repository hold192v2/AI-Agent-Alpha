using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.GetKonturMeeting;

public record KonturMeetingImportRequest(string userEmail) : IRequest<Response<KonturMeetingImport>>;
