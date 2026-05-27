using AutoMapper;
using Chat.Application.Dtos;
using Chat.Application.HandlerResponse;
using Chat.Domain.Interfaces;
using MediatR;

namespace Chat.Application.UseCases.Message.GetMessage;

public class GetMessageHandler: IRequestHandler<GetMessageRequest, Response>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetMessageHandler(ISessionRepository sessionRepository, IUnitOfWork unitOfWork, IMapper mapper,  IMessageRepository messageRepository)
    {
        _sessionRepository = sessionRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _messageRepository = messageRepository;
    }
    
    public async Task<Response> Handle(GetMessageRequest request, CancellationToken cancellationToken)
    {
        var sessionId = request.SessionId;
        var timeout = TimeSpan.FromSeconds(30);

        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        
        cts.CancelAfter(timeout);

        while (!cts.Token.IsCancellationRequested)
        {
            var session = _sessionRepository.GetSessionById(sessionId).Result;
            if (session.IsReady)
            {
                var answer = _messageRepository.GetAnswerByChatId(session.ChatId).Result;
                
                var finalMessage = new SendFinalMessageDto
                {
                    SessionId = sessionId,
                    Answer = answer.Content,
                    IsDigest = answer.DigestKey != null
                };
                
                return new Response("Success", 200, finalMessage);
            }
            
            await Task.Delay(500, cts.Token);
        }

        return new Response("Error", 400);
    }
}