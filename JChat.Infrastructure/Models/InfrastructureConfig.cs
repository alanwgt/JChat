using JChat.Infrastructure.Interfaces;

namespace JChat.Infrastructure.Models;

public record InfrastructureConfig(string KratosBasePath, string KetoWriteBasePath, string KetoReadBasePath,
    string DatabaseConnectionString) : IInfrastructureConfig;