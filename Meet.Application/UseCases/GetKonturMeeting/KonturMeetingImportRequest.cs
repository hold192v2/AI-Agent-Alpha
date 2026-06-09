using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.GetKonturMeeting;

public record KonturMeetingImportRequest(DateTime Start, string? UserEmail = null) : IRequest<Response<KonturMeetingImport>>;
