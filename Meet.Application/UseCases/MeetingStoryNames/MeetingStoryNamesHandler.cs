using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.MeetingStoryNames;

public class MeetingStoryNamesHandler: IRequestHandler<MeetingStoryNamesRequest, Response<GetMeetingStoryNames>>
{
    private readonly IUserMeetingRepository _userMeetingRepository;
    private readonly IMeetingRepository _meetingRepository;

    public MeetingStoryNamesHandler(IUserMeetingRepository userMeetingRepository, IMeetingRepository meetingRepository)
    {
        _userMeetingRepository = userMeetingRepository;
        _meetingRepository = meetingRepository;
    }
    
    public async Task<Response<GetMeetingStoryNames>> Handle(MeetingStoryNamesRequest request, CancellationToken cancellationToken)
    {
        var userMeetings = await _userMeetingRepository.GetUserIdsByMeetingId(request.UserId);
        var bdMeetings = new List<Meeting.Domain.Entities.Meeting>();
        foreach (var userMeeting in userMeetings)
        {
            bdMeetings.Add(await _meetingRepository.GetMeetingById(userMeeting));
        }

        var meetingsStoryNames = new List<GetMeetingStoryNames>();
        meetingsStoryNames.AddRange(bdMeetings.Select(bdMeeting => new GetMeetingStoryNames(bdMeeting.Id, bdMeeting.Title)));
        return new Response<GetMeetingStoryNames>("Meeting Story Names", 200, meetingsStoryNames);
    }
}