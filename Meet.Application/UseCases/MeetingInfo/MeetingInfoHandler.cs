using AutoMapper;
using MassTransit;
using MediatR;
using Meet.Application.Dtos;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.MeetingInfo;

public class MeetingInfoHandler: IRequestHandler<MeetingInfoRequest, HandleResponse.Response<GetMeetingInfo>>
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly IProtocolRepository _protocolRepository;
    private readonly IRequestClient<UsersByIdRequest> _userClient;
    private readonly IUserMeetingRepository _userMeetingRepository;

    public MeetingInfoHandler(IMeetingRepository meetingRepository, IRequestClient<UsersByIdRequest> userClient, IProtocolRepository protocolRepository, IUserMeetingRepository userMeetingRepository)
    {
        _meetingRepository = meetingRepository;
        _protocolRepository = protocolRepository;
        _userClient = userClient;
        _userMeetingRepository = userMeetingRepository;
    }
    public async Task<HandleResponse.Response<GetMeetingInfo>> Handle(MeetingInfoRequest request, CancellationToken cancellationToken)
    {
        var meetingInfo = await _meetingRepository.GetMeetingById(request.Id);
        if (meetingInfo == null)
            return new HandleResponse.Response<GetMeetingInfo>("Meeting not found", 404);

        var protocols = await _protocolRepository.GetProtocolsByMeetingId(meetingInfo.Id);
        var protocolInfo = protocols.Select(protocol => new Dtos.ProtocolInfo(protocol.Id, protocol.Title, protocol.Content.Length > 100 ? protocol.Content.Substring(0,100) + "...": protocol.Content, protocol.CreatedAt)).ToList();
        
        var userIds = await _userMeetingRepository.GetUserIdsByMeetingId(meetingInfo.Id);
        var usersResponse = await _userClient.GetResponse<UsersByIdResponse>
            (new UsersByIdRequest{ UserId = userIds });
        var usersInfo = usersResponse.Message.UsersInfo;
        var response = new GetMeetingInfo(meetingInfo.Id, meetingInfo.Description, meetingInfo.StartDate, (meetingInfo.StartDate -  meetingInfo.EndDate).Minutes, usersInfo, protocolInfo);
        
        return new HandleResponse.Response<GetMeetingInfo>("Meeting Info", 200, new List<GetMeetingInfo> {response});
    }
}