using JChat.Application.Shared.Interfaces;
using JChat.Infrastructure.Interfaces;
using Ory.Keto.Client.Api;
using Ory.Keto.Client.Model;

namespace JChat.Infrastructure.Identity;

/// <summary>
/// @link https://www.ory.sh/keto/docs/
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    private readonly WriteApi _writeApi;
    private readonly ReadApi _readApi;

    public AuthorizationService(IInfrastructureConfig infrastructureConfig)
    {
        _readApi = new ReadApi(new Ory.Keto.Client.Client.Configuration
        {
            BasePath = infrastructureConfig.KetoReadBasePath,
        });
        _writeApi = new WriteApi(new Ory.Keto.Client.Client.Configuration
        {
            BasePath = infrastructureConfig.KetoWriteBasePath,
        });
    }

    public async Task Authorize( )
    {
        var result = await _writeApi.CreateRelationTupleAsync(new KetoRelationQuery());

        if (result == null)
            throw new Exception();
    }

    public async Task<bool> IsAuthorized()
    {
        return false;
    }
}