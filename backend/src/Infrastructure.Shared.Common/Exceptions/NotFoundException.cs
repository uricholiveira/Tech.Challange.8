namespace Infrastructure.Shared.Common.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string id, string message) : base(message)
    {
        Id = id;
    }

    public string Id { get; } = null!;
}