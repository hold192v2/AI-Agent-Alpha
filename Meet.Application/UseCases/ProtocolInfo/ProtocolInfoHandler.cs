using MediatR;
using Meet.Application.Dtos;
using Meet.Application.HandleResponse;

namespace Meet.Application.UseCases.ProtocolInfo;

public class ProtocolInfoHandler: IRequestHandler<ProtocolInfoRequest, Response<GetProtocolInfo>>
{
    private IRequestHandler<ProtocolInfoRequest, Response<GetProtocolInfo>> _requestHandlerImplementation;
    public Task<Response<GetProtocolInfo>> Handle(ProtocolInfoRequest request, CancellationToken cancellationToken)
    {
        return _requestHandlerImplementation.Handle(request, cancellationToken);
    }
}