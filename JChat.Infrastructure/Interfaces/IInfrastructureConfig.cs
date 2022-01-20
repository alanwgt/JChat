namespace JChat.Infrastructure.Interfaces;

public interface IInfrastructureConfig
{
    string KratosBasePath { get; }
    string KetoWriteBasePath { get; }
    string KetoReadBasePath { get; }
    string DatabaseConnectionString { get; }
}