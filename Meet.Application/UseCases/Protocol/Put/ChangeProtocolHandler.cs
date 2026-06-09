using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;
using Meeting.Domain.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Meet.Application.UseCases.Protocol.Put;

public class ChangeProtocolHandler: IRequestHandler<ChangeProtocolRequest, Response<PutProtocolResponse>>
{ 
    private readonly IProtocolRepository _protocolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeProtocolHandler(IProtocolRepository protocolRepository, IUnitOfWork unitOfWork)
    {
        _protocolRepository = protocolRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Response<PutProtocolResponse>> Handle(ChangeProtocolRequest request, CancellationToken cancellationToken)
    {
        var protocol = await _protocolRepository.GetProtocolById(request.ProtocolId);
        if (protocol == null)
            return new Response<PutProtocolResponse>("Protocol not found", 404);

        protocol.Title = request.Name;
        protocol.Content = request.Description;
        _protocolRepository.UpdateProtocol(protocol);
        
        _unitOfWork.Commit(cancellationToken);
        return new Response<PutProtocolResponse>("Protocol updated", 200, new List<PutProtocolResponse> {new(protocol.Id)});
    }
}