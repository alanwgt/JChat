using Ory.Keto.Client.Model;

namespace JChat.Application.Shared.Interfaces;

public interface IAuthorizationService
{
    Task<KetoRelationQuery> Authorize(string? @namespace = null, string? @object = null, string? relation = null,
        string? subjectId = null,
        string? subjectSetNamespace = null, string? subjectSetObject = null, string? subjectSetRelation = null,
        CancellationToken cancellationToken = new());

    Task<KetoRelationQuery> Authorize(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = new());

    Task<bool> Can(string @namespace, string @object, string relation, string subjectId,
        string? subjectSetNamespace = null, string? subjectSetObject = null, string? subjectSetRelation = null,
        CancellationToken cancellationToken = new());

    Task<bool> Can(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = new());

    Task<KetoExpandTree> Expand(string @namespace, string @object, string relation,
        long maxDepth = long.MaxValue,
        CancellationToken cancellationToken = new());

    Task<KetoGetRelationTuplesResponse> ListObjectsWithAccess(
        string @namespace,
        string? pageToken = null,
        long? pageSize = null,
        string? @object = null,
        string? relation = null,
        string? subjectId = null,
        string? subjectSetNamespace = null,
        string? subjectSetObject = null,
        string? subjectSetRelation = null,
        CancellationToken cancellationToken = default);

    Task RemoveAuthorization(string? @namespace = null, string? @object = null, string? relation = null,
        string? subjectId = null, string? subjectSetNamespace = null, string? subjectSetObject = null,
        string? subjectSetRelation = null, CancellationToken cancellationToken = default
    );

    Task RemoveAuthorization(string @namespace, string @object, string relation, string subjectId,
        CancellationToken cancellationToken = default
    );
}