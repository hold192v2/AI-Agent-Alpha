using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.ProtocolInfo;

public class ProtocolInfoHandler: IRequestHandler<ProtocolInfoRequest, Response<GetProtocolInfo>>
{
    private readonly IProtocolRepository _protocolRepository;

    public ProtocolInfoHandler(IProtocolRepository protocolRepository)
    {
        _protocolRepository = protocolRepository;
    }
    public async Task<Response<GetProtocolInfo>> Handle(ProtocolInfoRequest request, CancellationToken cancellationToken)
    {
        var protocol = await _protocolRepository.GetProtocolById(request.Id);
        var protocolInfo = new GetProtocolInfo(protocol.Id, protocol.Title, protocol.Content.Length > 100 ? protocol.Content.Substring(0,100) + "...": protocol.Content, protocol.CreatedAt, protocol.IsImproved);
        return new Response<GetProtocolInfo>("ProtocolInfo", 200, new List<GetProtocolInfo> { protocolInfo });
    }
}