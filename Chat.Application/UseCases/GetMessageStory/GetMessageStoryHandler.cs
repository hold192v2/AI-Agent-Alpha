using AutoMapper;
using Chat.Application.HandlerResponse;
using Chat.Application.Dtos;
using Chat.Domain.Interfaces;
using MediatR;

namespace Chat.Application.UseCases.GetMessageStory;

public class GetMessageStoryHandler: IRequestHandler<GetMessageStoryRequest, Response>
{
    private readonly IMapper _mapper;
    public readonly IMessageRepository _messageRepository;

    public GetMessageStoryHandler(IMapper mapper, IMessageRepository messageRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
    }
    
    public async Task<Response> Handle(GetMessageStoryRequest request, CancellationToken cancellationToken)
    {
        List<GetChatMessagesDto> messages;
        
        var entityMessages = _messageRepository.GetMessagesByChatId(request.ChatId).Result;
        messages = _mapper.Map<List<GetChatMessagesDto>>(entityMessages);
        
        return new Response("MessageStory", 200, messages);
    }
}