using JChat.Application.Shared.Interfaces;
using JChat.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Ory.Keto.Client.Api;
using Ory.Keto.Client.Client;
using Ory.Keto.Client.Model;

namespace JChat.Infrastructure.Identity;

/// <summary>
/// @link https://www.ory.sh/keto/docs/
/// </summary>
public class AuthorizationService : IAuthorizationService
{
    private readonly WriteApi _writeApi;
    private readonly ReadApi _readApi;
    private readonly ILogger<AuthorizationService> _logger;

    public AuthorizationService(IInfrastructureConfig infrastructureConfig, ILogger<AuthorizationService> logger)
    {
        _logger = logger;
        _readApi = new ReadApi(new Configuration
        {
            BasePath = infrastructureConfig.KetoReadBasePath,
        });
        _writeApi = new WriteApi(new Configuration
        {
            BasePath = infrastructureConfig.KetoWriteBasePath,
        });
    }

    public Task<KetoRelationQuery> Authorize(string? @namespace = null, string? @object = null, string? relation = null,
        string? subjectId = null,
        string? subjectSetNamespace = null, string? subjectSetObject = null, string? subjectSetRelation = null,
        CancellationToken cancellationToken = default)
    {
        KetoSubjectSet? subjectSet = null;

        if (subjectSetNamespace != null || subjectSetObject != null || subjectSetRelation != null)
            subjectSet = new KetoSubjectSet(subjectSetNamespace, subjectSetObject, subjectSetRelation);

        return _writeApi.CreateRelationTupleAsync(
            new KetoRelationQuery(@namespace, @object, relation, subjectId, subjectSet), cancellationToken
        );
    }

    public Task<KetoRelationQuery> Authorize(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = default)
        => Authorize(@namespace, @object, relation, subjectId, null, null, null, cancellationToken);

    public async Task<bool> Can(string @namespace, string @object, string relation, string subjectId,
        string? subjectSetNamespace = null, string? subjectSetObject = null, string? subjectSetRelation = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _readApi.GetCheckAsync(
                @namespace,
                @object,
                relation,
                subjectId,
                null,
                null,
                null,
                cancellationToken
            );

            return result is { Allowed: true };
        }
        catch (ApiException e)
        {
            if (e.ErrorCode == 403)
                return false;

            _logger.LogError(e, "failed to validate authorization with keto");
            return false;
        }
    }

    public Task<bool> Can(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = default)
        => Can(@namespace, @object, relation, subjectId, null, null, null, cancellationToken);

    /// <summary>
    /// Display who has Access to an ObjectIdFromProperty
    /// </summary>
    /// <param name="namespace"></param>
    /// <param name="object"></param>
    /// <param name="relation"></param>
    /// <param name="maxDepth"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<KetoExpandTree> Expand(string @namespace, string @object, string relation,
        long maxDepth = long.MaxValue,
        CancellationToken cancellationToken = default)
    {
        return _readApi.GetExpandAsync(@namespace, @object, relation, maxDepth, cancellationToken);
    }

    public Task<KetoGetRelationTuplesResponse> ListObjectsWithAccess(
        string @namespace,
        string? pageToken = null,
        long? pageSize = null,
        string? @object = null,
        string? relation = null,
        string? subjectId = null,
        string? subjectSetNamespace = null,
        string? subjectSetObject = null,
        string? subjectSetRelation = null,
        CancellationToken cancellationToken = default)
    {
        return _readApi.GetRelationTuplesAsync(
            @namespace,
            pageToken,
            pageSize,
            @object,
            relation,
            subjectId,
            subjectSetNamespace,
            subjectSetObject,
            subjectSetRelation,
            cancellationToken
        );
    }

    public Task RemoveAuthorization(string? @namespace = null, string? @object = null, string? relation = null,
        string? subjectId = null, string? subjectSetNamespace = null, string? subjectSetObject = null,
        string? subjectSetRelation = null, CancellationToken cancellationToken = default
    )
    {
        return _writeApi.DeleteRelationTupleAsync(
            @namespace, @object, relation, subjectId, subjectSetNamespace, subjectSetObject,
            subjectSetRelation, cancellationToken
        );
    }

    public Task RemoveAuthorization(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = default
    ) => RemoveAuthorization(@namespace, @object, relation, subjectId, null,
        null, null, cancellationToken);
}