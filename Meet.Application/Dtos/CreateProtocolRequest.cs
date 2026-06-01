using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Meet.Application.Dtos;

public class CreateProtocolRequest
{
    public CreateProtocolRequest(string protocolName, string protocolDescription)
    {
        Name = protocolName;
        Description = protocolDescription;
    }
    [Description("Название протокола")]
    public string Name { get; private set; }
    
    [Description("Описание протокола")]
    public string Description { get; private set; }
}