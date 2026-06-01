using System.ComponentModel;

namespace Meet.Application.Dtos;

public record FormalizeProtocolResponse([Description("Протокол, сгенерированный нейросетью.")]string NewProtocolDesc);