using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.GetKonturMeeting;

public class KonturMeetingImportHandler: IRequestHandler<KonturMeetingImportRequest, Response<KonturMeetingImport>>
{
    private readonly IKonturTalkApiClient _konturTalkApiClient;

    public KonturMeetingImportHandler(IKonturTalkApiClient konturTalkApiClient)
    {
        _konturTalkApiClient = konturTalkApiClient;
    }

    public async Task<Response<Dtos.KonturMeetingImport>> Handle(KonturMeetingImportRequest request, CancellationToken cancellationToken)
    {
        var meetings = await _konturTalkApiClient.GetMeetingsByUserEmail(request.userEmail);
        var konturMeetingImport = meetings.Items.Select(meeting => new KonturMeetingImport(new Guid(meeting.Id), meeting.Description, meeting.Start)).ToList();
        return new Response<KonturMeetingImport>("KonturMeetingImport", 200, konturMeetingImport);
    }
}