namespace Infrastructure.Shared.Common.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string id, string message) : base(message)
    {
        Id = id;
    }

    public string Id { get; } = null!;
}