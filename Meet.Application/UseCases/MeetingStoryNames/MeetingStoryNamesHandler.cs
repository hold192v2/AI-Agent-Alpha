using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.MeetingStoryNames;

public class MeetingStoryNamesHandler: IRequestHandler<MeetingStoryNamesRequest, Response<GetMeetingStoryNames>>
{
    private readonly IKonturTalkApiClient _konturTalkApiClient;

    public MeetingStoryNamesHandler(IKonturTalkApiClient konturTalkApiClient)
    {
        _konturTalkApiClient = konturTalkApiClient;
    }
    
    public async Task<Response<GetMeetingStoryNames>> Handle(MeetingStoryNamesRequest request, CancellationToken cancellationToken)
    {
        var meetings = await _konturTalkApiClient.GetMeetingsByUserEmail(request.UserEmail);
        var meetingsStoryNames = meetings.Items.Select(meeting => new GetMeetingStoryNames(new Guid(meeting.Id), meeting.Subject)).ToList();
        return new Response<GetMeetingStoryNames>("Meeting Story Names", 200, meetingsStoryNames);
    }
}