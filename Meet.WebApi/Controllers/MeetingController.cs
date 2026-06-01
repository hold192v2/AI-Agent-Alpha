using System.ComponentModel;
using Meet.Application;
using Meet.Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Meet.WebApi.Controllers;

[ApiController]
[Route("meeting")]
public class MeetingController : ControllerBase
{
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
        return Ok(
            new List<GetMeetingStoryNames>()
            {
                new GetMeetingStoryNames(new Guid(), "Встреча с командой - 15.05.2026.")
            });
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
        return Ok(new CreateEmptyMeeting());
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
        return Ok(new CreateEmptyMeeting());
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
        return Ok(new List<KonturMeetingImport>()
        {
            new KonturMeetingImport(Guid.NewGuid(), "Встреча аналитиков", DateTime.UtcNow)
        });
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
        return Ok(new List<GetMeetingInfo>()
        {
            new GetMeetingInfo(Guid.NewGuid(), "Встреча аналитиков",  DateTime.UtcNow, 30)
        });
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
        return Ok(new List<GetProtocolInfo>()
        {
            new GetProtocolInfo(Guid.NewGuid(), "Встреча аналитиков",  "Было озвучено переделать анализ такой-то функции", DateTime.UtcNow, true)
        });
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
        return Ok(new CreateProtocolResponse(Guid.NewGuid(), DateTime.UtcNow));
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
        return Ok(new PutProtocolResponse(protocolRequest.ProtocolId));
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