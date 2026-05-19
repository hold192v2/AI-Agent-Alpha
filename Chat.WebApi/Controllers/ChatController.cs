using System.ComponentModel;
using Chat.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.WebApi.Controllers;
[Tags("Chat Service")]
[ApiController]
[Route("chat")]
public class ChatController : ControllerBase
{
    //[Authorize(Roles = "teamlead")] При интеграции вернуть
    [HttpGet("storyNames")]
    [EndpointSummary("Получить названия чатов")]
    [EndpointDescription("Выводит все названия чатов, с которыми связан пользователь.")]
    [ProducesResponseType(typeof(GetChatStoryNamesDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetCommands()
    {
        return Ok(
            new List<GetChatStoryNamesDto>()
            {
                new GetChatStoryNamesDto(Guid.Empty,Guid.NewGuid(), "Аналитика по команде на момент 16.05.2026")
            });
    }
    //[Authorize(Roles = "teamlead")]
    [HttpGet("messageStory")]
    [EndpointSummary("Получить историю сообщений")]
    [EndpointDescription("Выводит всю историю конкретного чата.")]
    [ProducesResponseType(typeof(GetChatMessagesDto),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetMessages([Description("ID чата, сообщения которого выводятся на экран")]
        [FromQuery]Guid chatId)
    {

        var messages = new List<GetChatMessagesDto>()
        {
            new GetChatMessagesDto(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Привет! Как дела?",
                false
            ),

            new GetChatMessagesDto(
                Guid.NewGuid(),
                null,
                "Я AI-ассистент, чем могу помочь?",
                false
            ),

            new GetChatMessagesDto(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Расскажи про микросервисы",
                false
            ),

            new GetChatMessagesDto(
                Guid.NewGuid(),
                null,
                "Микросервисы — это архитектурный стиль распределённых систем...",
                false
            ),

            new GetChatMessagesDto(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Спасибо, понял",
                false
            )
        };
        return Ok(messages);
    }
    //[Authorize(Roles = "teamlead")]
    [HttpPost("message")]
    [EndpointSummary("Отправка сообщения в чат")]
    [EndpointDescription("Отправляет сообщение на обработку, получает id сессии для последующего получения ответа.")]
    [ProducesResponseType(typeof(SendChatMessageResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public  IActionResult SendMessageRequest(
        [FromBody] SendChatMessageRequestDto request,
        CancellationToken cancellationToken)
    {
            var result = new SendChatMessageResponseDto
            {
                SessionId = new Guid("c098fcbd-28b5-42ff-8317-fd4572e4796a")
            };
        return Ok(result);
    }
    
    //Я почитал еще документики, все же лучшим вариантом вывода - long polling, не просто polling.
    //Вот диалог с чаткой на этот счет, рекомендую ознакомиться
    //https://chatgpt.com/share/6a0bffb3-fc18-832e-8184-085b8ea7e2b4
    
    //[Authorize(Roles = "teamlead")]
    [HttpGet("message")]
    [EndpointSummary("Получение ответного сообщения")]
    [EndpointDescription("Получает ответ от нейросети по ID сессии.")]
    [ProducesResponseType(typeof(SendFinalMessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult SendFinalMessage([FromQuery][Description("ID сессии")] 
        Guid sessionId)
    {
        var result = new SendFinalMessageDto
        {
            SessionId = new Guid("c098fcbd-28b5-42ff-8317-fd4572e4796a"),
            Answer = "Ответ от ИИ."
        };
        return Ok(result);
    }
}