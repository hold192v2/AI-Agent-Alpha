using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.MeetingInfo;

public record MeetingInfoRequest(Guid Id, string UserEmail): IRequest<Response<GetMeetingInfo>>;