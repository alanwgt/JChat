using JChat.Infrastructure.Interfaces;

namespace JChat.Infrastructure.Models;

public class InfrastructureConfig : IInfrastructureConfig
{
    public string KratosBasePath { get; init; }
    public string KetoWriteBasePath { get; init; }
    public string KetoReadBasePath { get; init; }
    public string DatabaseConnectionString { get; init; }
}
