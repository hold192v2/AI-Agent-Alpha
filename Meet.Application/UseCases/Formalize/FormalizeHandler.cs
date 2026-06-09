using MassTransit;
using MediatR;
using Meet.Application.Dtos;

namespace Meet.Application.UseCases.Formalize;

public class FormalizeHandler: IRequestHandler<FormalizeRequest, HandleResponse.Response<NewProtocolDesc>>
{
    private readonly IRequestClient<OldProtocolDesc> _formalizeClient;

    public FormalizeHandler(IRequestClient<OldProtocolDesc> formalizeClient)
    {
        _formalizeClient = formalizeClient;
    }
    
    public async Task<HandleResponse.Response<NewProtocolDesc>> Handle(FormalizeRequest request, CancellationToken cancellationToken)
    {
        var newDesc = await _formalizeClient.GetResponse<NewProtocolDesc>
            (new OldProtocolDesc { Description = request.OldProtocolDesc });
        
        return new HandleResponse.Response<NewProtocolDesc>("New protocol desc", 200, new List<NewProtocolDesc> {new() { Description = newDesc.Message.Description}});
    }
}