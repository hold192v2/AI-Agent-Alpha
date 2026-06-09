using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;

namespace Meet.Application.UseCases.Protocol.Post;

public class CreateProtocolHandler: IRequestHandler<CreateProtocolRequest, Response<CreateProtocolResponse>>
{
    private readonly IProtocolRepository _protocolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProtocolHandler(IProtocolRepository protocolRepository, IUnitOfWork unitOfWork)
    {
        _protocolRepository = protocolRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Response<CreateProtocolResponse>> Handle(CreateProtocolRequest request, CancellationToken cancellationToken)
    {
        var protocol = new Meeting.Domain.Entities.Protocol
        {
            Id = Guid.NewGuid(),
            Title = request.Name,
            MeetingId =  request.MeetingId,
            Content = request.Description,
            CreatedAt = DateTime.UtcNow,
            IsImproved = false,
            UpdatedAt = DateTime.UtcNow
        };
        _protocolRepository.CreateProtocol(protocol);
        
        _unitOfWork.Commit(cancellationToken);
        return new Response<CreateProtocolResponse>("Protocol created", 200, new List<CreateProtocolResponse>{new(protocol.Id,  protocol.CreatedAt)});
    }
}