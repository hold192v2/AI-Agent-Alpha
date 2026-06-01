using System.ComponentModel;

namespace Meet.Application.Dtos;

public class GetProtocolInfo
{
    public GetProtocolInfo(Guid protocolId, string protocolName, string protocolDescription, DateTime lastChangeTime, bool isKonturAttached)
    {
        Id = protocolId;
        Name = protocolName;
        Description = protocolDescription;
        ChangedAt = lastChangeTime;
        IsKonturAttached = isKonturAttached;
    }
    [Description("Id протокола")]
    public Guid Id { get; private set; }
    
    [Description("Название протокола")]
    public string Name { get; private set; }
    
    [Description("Описание протокола")]
    public string Description { get; private set; }
    
    [Description("Дата последнего изменения протокола")]
    public DateTime ChangedAt { get; private set; }
    
    [Description("Привязана ли встреча к Контур.Толку")]
    public bool IsKonturAttached { get; private set; }
    
}