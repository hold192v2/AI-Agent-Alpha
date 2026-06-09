using System.ComponentModel;
using Meet.Application;
using Meet.Application.Dtos;
using Meet.Application.UseCases.MeetingInfo;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Meet.Application.UseCases.CreateMeeting.Empty;
using Meet.Application.UseCases.CreateMeeting.Kontur;
using Meet.Application.UseCases.GetKonturMeeting;
using Meet.Application.UseCases.MeetingStoryNames;
using Meet.Application.UseCases.Protocol.Post;
using Meet.Application.UseCases.Protocol.Put;
using Meet.Application.UseCases.ProtocolInfo;

namespace Meet.WebApi.Controllers;

[ApiController]
[Route("meeting")]
public class MeetingController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("all")]
    [EndpointSummary("Получить названия встреч")]
    [EndpointDescription(
        "Выводит все названия встреч, с которыми связан пользователь"
    )]
    [ProducesResponseType(typeof(GetMeetingStoryNames),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetCommands()
    {
        var claims = User.Claims
            .GroupBy(c => c.Type)
            .ToDictionary(g => g.Key, g => g.First().Value);
        var userEmail = claims.GetValueOrDefault("preferred_username")!;
        var userId = new Guid(claims.GetValueOrDefault("user-id")!);
        var request = new MeetingStoryNamesRequest(userEmail, userId);
        var response = _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpPost("empty")]
    [EndpointSummary("Создать пустую встречу")]
    [EndpointDescription(
        "Создает встречу без привязки к Контуру."
    )]
    [ProducesResponseType(typeof(CreateEmptyMeeting),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult MeetingEmpty()
    {
        var response = _mediator.Send(new CreateEmptyMeetingRequest());
        return Ok(response);
    }
    
    [HttpPost("import/kontur")]
    [EndpointSummary("Создать/изменить встречу с данными из Контура")]
    [EndpointDescription(
        "Создает встречу на основе встречи из Контур.Толка или дополняет существующую информацией из Контур.Толка."
    )]
    [ProducesResponseType(typeof(CreateEmptyMeeting),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult KonturMeeting([FromBody]
        KonturMeetingRequest request)
    {
        var claims = User.Claims
            .GroupBy(c => c.Type)
            .ToDictionary(g => g.Key, g => g.First().Value);
        var userEmail = claims.GetValueOrDefault("preferred_username")!;
        var newRequest = new CreateMeetingByKontureRequest(request.MeetingId, request.KonturMeetingId.ToString(), userEmail);
        var response = _mediator.Send(newRequest);
        return Ok(response);
    }
    
    [HttpGet("import/kontur")]
    [EndpointSummary("Получить встречи из Контур.Толка")]
    [EndpointDescription(
        "Производит импорт встреч из Контур.Толка для отображения на клиенте и привязке к новой или существующей встрече."
    )]
    [ProducesResponseType(typeof(KonturMeetingImport),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult KonturMeetingsImport()
    {
        var claims = User.Claims
            .GroupBy(c => c.Type)
            .ToDictionary(g => g.Key, g => g.First().Value);
        var userEmail = claims.GetValueOrDefault("preferred_username")!;
        var request = new KonturMeetingImportRequest(userEmail);
        var response = _mediator.Send(request);
        return Ok(response);
    }
    [HttpGet]
    [EndpointSummary("Получить информацию о конкретной встрече")]
    [EndpointDescription(
        "Получает информацию о конкретной встрече по id, указанном в Query запросе."
    )]
    [ProducesResponseType(typeof(GetMeetingInfo),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetMeetingInfo([FromQuery] [Description("Id встречи")]
        Guid meetingId)
    {
        var request = new MeetingInfoRequest(meetingId);
        var response = _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpGet("protocol")]
    [EndpointSummary("Получить информацию о конкретном протоколе")]
    [EndpointDescription(
        "Получает информацию о конкретном протоколе по id, указанном в Query запросе."
    )]
    [ProducesResponseType(typeof(GetProtocolInfo),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult GetProtocolInfo([FromQuery] [Description("Id протокола")]
        Guid protocolId)
    {
        var request = new ProtocolInfoRequest(protocolId);
        var response = _mediator.Send(request);
        return Ok(response);
    }
    
    [HttpPost("protocol")]
    [EndpointSummary("Создать новый протокол")]
    [EndpointDescription(
        "Создание нового протокола."
    )]
    [ProducesResponseType(typeof(CreateProtocolResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult CreateProtocol([FromBody]
        CreateProtocolRequest protocolRequest)
    {
        var response = _mediator.Send(protocolRequest);
        return Ok(response);
    }
    
    [HttpPut("protocol")]
    [EndpointSummary("Изменить существующий протокол")]
    [EndpointDescription(
        "Изменение существующего протокола."
    )]
    [ProducesResponseType(typeof(PutProtocolResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult ChangeProtocol([FromBody]
        ChangeProtocolRequest protocolRequest)
    {
        var response = _mediator.Send(protocolRequest);
        return Ok(response);
    }
    
    [HttpPost("protocol/formalize")]
    [EndpointSummary("Формализовать протокол с помощью ИИ")]
    [EndpointDescription(
        "Формализация написанного протокола с помощью ИИ и вывод."
    )]
    [ProducesResponseType(typeof(FormalizeProtocolResponse),StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult AiFormalizeProtocol([FromBody] FormalizeProtocolRequest oldProtocolDesc)
    {
        return Ok(new FormalizeProtocolResponse(oldProtocolDesc.OldProtocolDesc));
    }
    
    [HttpPost("protocol/email/send")]
    [EndpointSummary("Отправить протокол почтой")]
    [EndpointDescription(
        "Отправляет протокол на почту пользователей, присутствовавшим на встрече."
    )]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public IActionResult ProtocolSendByEmail([FromBody] EmailProtocolSendRequest protocolRequest)
    {
        return Ok();
    }
}