using AutoMapper;
using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.MeetingInfo;

public class MeetingInfoHandler: IRequestHandler<MeetingInfoRequest, Response<GetMeetingInfo>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IProtocolRepository _protocolRepository;
    private readonly IKonturTalkApiClient _konturTalkApiClient;
    private readonly IMapper _mapper;

    public MeetingInfoHandler(IMeetingRepository meetingRepository, IMapper mapper, IProtocolRepository protocolRepository,  IKonturTalkApiClient konturTalkApiClient)
    {
        _meetingRepository = meetingRepository;
        _mapper = mapper;
        _protocolRepository = protocolRepository;
        _konturTalkApiClient = konturTalkApiClient;
    }
    public async Task<Response<GetMeetingInfo>> Handle(MeetingInfoRequest request, CancellationToken cancellationToken)
    {
        var meetingInfo = await _meetingRepository.GetMeetingById(request.Id);
        if (meetingInfo == null)
            return new Response<GetMeetingInfo>("Meeting not found", 404);

        var protocols = await _protocolRepository.GetProtocolsByMeetingId(meetingInfo.Id);
        var protocolInfo = protocols.Select(protocol => new Dtos.ProtocolInfo(protocol.Id, protocol.Title, protocol.Title, protocol.CreatedAt)).ToList();
        
        var users = await _konturTalkApiClient.GetUsersByMeetingId(request.Id);
        var usersInfo = users.Select(user =>
            new Dtos.UserMeetingInfo(new Guid(user.Key), user.Surname, user.Name, user.AvatarUrl)).ToList();
        
        var response = new GetMeetingInfo(meetingInfo.Id, meetingInfo.Description, meetingInfo.StartDate, (meetingInfo.StartDate -  meetingInfo.EndDate).Minutes, usersInfo, protocolInfo);
        
        return new Response<GetMeetingInfo>("Meeting Info", 200, new List<GetMeetingInfo> {response});
    }
}