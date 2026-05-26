using AutoMapper;
using Chat.Application.HandlerResponse;
using Chat.Domain.Interfaces;
using MediatR;

namespace Chat.Application.UseCases.Message.PostMessage;

public class PostMessageHandler: IRequestHandler<PostMessageRequest, Response>
{
    private IMapper _mapper;
    private IMessageRepository _messageRepository;
    private IUnitOfWork _unitOfWork;

    public PostMessageHandler(IMapper mapper, IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _unitOfWork =  unitOfWork;
    }
    public async Task<Response> Handle(PostMessageRequest request, CancellationToken cancellationToken)
    {
        
    }
}