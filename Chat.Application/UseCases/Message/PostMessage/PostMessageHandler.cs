using AutoMapper;
using Chat.Application.Dtos;
using Chat.Application.HandlerResponse;
using Chat.Domain.Entities;
using Chat.Domain.Interfaces;
using MediatR;

namespace Chat.Application.UseCases.Message.PostMessage;

public class PostMessageHandler: IRequestHandler<PostMessageRequest, Response>
{
    private readonly IMapper _mapper;
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISessionRepository _sessionRepository;

    public PostMessageHandler(IMapper mapper, IMessageRepository messageRepository, IUnitOfWork unitOfWork, IChatRepository chatRepository, ISessionRepository sessionRepository)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _unitOfWork =  unitOfWork;
        _chatRepository = chatRepository;
        _sessionRepository = sessionRepository;
    }
    public async Task<Response> Handle(PostMessageRequest request, CancellationToken cancellationToken)
    {
        var chatId = request.ChatId;
        var userId = request.UserId;
        var teamId = request.TeamId;
        Domain.Entities.Chat chat;
        if (chatId != null)
        {
            chat = _chatRepository.GetChatById(chatId).Result;
            if (chat.TeamId == null && teamId != null)
                chat.TeamId = teamId;
        }
        else
        {
            chat = new Domain.Entities.Chat
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TeamId = request.TeamId,
                Role = request.Role
            };
            _chatRepository.CreateChat(chat);
        }

        _messageRepository.addMessage(chat.Id, userId, request.Text);

        var session = new Session
        {
            Id = Guid.NewGuid(),
            IsReady = false,
            ChatId = chat.Id
        };
        _sessionRepository.CreateSession(session);

        var chatMessageResponse = new SendChatMessageResponseDto()
        {
            SessionId = session.Id,
            Message = "expect",
        };

        _unitOfWork.Commit(cancellationToken);
        return new Response("Created message successfully", 200, chatMessageResponse);
    }
}