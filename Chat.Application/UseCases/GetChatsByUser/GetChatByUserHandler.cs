using AutoMapper;
using Chat.Application.Dtos;
using Chat.Application.HandlerResponse;
using Chat.Domain.Interfaces;
using MediatR;

namespace Chat.Application.UseCases.GetChatsByUser;

public class GetChatByUserHandler: IRequestHandler<GetChatsByUserRequest, Response>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;

    public GetChatByUserHandler(IChatRepository chatRepository, IMapper mapper)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
    }
    
    public async Task<Response> Handle(GetChatsByUserRequest request, CancellationToken cancellationToken)
    {
        List<GetChatStoryNamesDto> chats;
        
        var entityChats = _chatRepository.GetChatsByUserId(request.UserId);
        chats = _mapper.Map<List<GetChatStoryNamesDto>>(entityChats);
        
        return new Response("Chats", 200, chats);
    }
}