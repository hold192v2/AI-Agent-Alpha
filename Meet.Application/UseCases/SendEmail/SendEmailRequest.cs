using MediatR;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.SendEmail;

public record SendEmailRequest(Guid ProtocolId, string ProtocolDesc, string? UserEmail = null): IRequest<Response<string>>;