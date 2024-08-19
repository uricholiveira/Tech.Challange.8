namespace Infrastructure.Shared.Common.Exceptions;

public class ConflictException : ApplicationException
{
    public ConflictException(string message) : base(message)
    {
    }

    public ConflictException(string id, string message) : base(message)
    {
        Id = id;
    }

    public string Id { get; } = null!;
}