using System.Runtime.Serialization;
using Npgsql;

namespace JChat.Infrastructure.Exceptions;

public class ViolatesUniqueKeyConstraintException : InfrastructureException
{
    public string ConstraintName { get; }
    public string TableName { get; }
    public string SqlState { get; }

    public ViolatesUniqueKeyConstraintException()
    {
    }

    protected ViolatesUniqueKeyConstraintException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ViolatesUniqueKeyConstraintException(string? message) : base(message)
    {
    }

    public ViolatesUniqueKeyConstraintException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public ViolatesUniqueKeyConstraintException(string constraintName, string tableName, string sqlState, Exception e)
    : base($"constraint {constraintName} violated on table {tableName}. code: {sqlState}", e)
    {
        ConstraintName = constraintName;
        TableName = tableName;
        SqlState = sqlState;
    }
}