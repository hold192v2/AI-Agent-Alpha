using MediatR;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.CreateMeeting.Kontur;

public class CreateMeetingByKontureHandler: IRequestHandler<CreateMeetingByKontureRequest, Response<string>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IKonturTalkApiClient _konturTalkApiClient;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeetingByKontureHandler(IMeetingRepository meetingRepository, IKonturTalkApiClient konturTalkApiClient)
    {
        _meetingRepository = meetingRepository;
        _konturTalkApiClient = konturTalkApiClient;
    }
    
    public async Task<Response<string>> Handle(CreateMeetingByKontureRequest request, CancellationToken cancellationToken)
    {
        var emailCalendarItem = await _konturTalkApiClient.FindMeetingByUserEmail(request.userEmail, request.KonturId);
        

        if (request.MeetingId == null)
        {
            var newMeeting = new Meeting.Domain.Entities.Meeting();
            newMeeting.Id = Guid.NewGuid();
            newMeeting.Title = emailCalendarItem.Subject;
            newMeeting.Description = emailCalendarItem.Description;
            newMeeting.KonturMeetingId = emailCalendarItem.Id;
            newMeeting.StartDate =  emailCalendarItem.Start;
            newMeeting.EndDate =  emailCalendarItem.End;
            newMeeting.CreatedAt = DateTime.UtcNow;
            _meetingRepository.CreateMeeting(newMeeting);
        }
        else
        {
            var meeting = await _meetingRepository.GetMeetingById(request.MeetingId.Value);
            meeting.Title = emailCalendarItem.Subject;
            meeting.Description = emailCalendarItem.Description;
            meeting.KonturMeetingId = emailCalendarItem.Id;
            meeting.StartDate = emailCalendarItem.Start;
            meeting.EndDate = emailCalendarItem.End;
            meeting.CreatedAt = DateTime.UtcNow;
        }
        
        _unitOfWork.Commit(cancellationToken);
        return new Response<string>("MeetingCreated", 200);
    }
}