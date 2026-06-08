using MediatR;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.CreateMeeting.Empty;

public class CreateEmptyMeetingHandler: IRequestHandler<CreateEmptyMeetingRequest, Response<string>>
{ 
    private readonly IMeetingRepository _meetingRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEmptyMeetingHandler(IMeetingRepository meetingRepository,  IUnitOfWork unitOfWork)
    {
        _meetingRepository = meetingRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Response<string>> Handle(CreateEmptyMeetingRequest request, CancellationToken cancellationToken)
    {
        var meeting = new Meeting.Domain.Entities.Meeting();
        meeting.Id = Guid.NewGuid();
        meeting.CreatedAt = DateTime.UtcNow;
        _meetingRepository.CreateMeeting(meeting);
        _unitOfWork.Commit(cancellationToken);
        return new Response<string>("MeetingCreated", 200);
    }
}