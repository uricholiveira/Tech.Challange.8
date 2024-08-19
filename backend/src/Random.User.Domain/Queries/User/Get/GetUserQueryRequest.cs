using MediatR;

namespace Random.User.Domain.Queries.User.Get;

public class GetUserQueryRequest : IRequest<GetUserQueryResponse>
{
    public int Id { get; set; }
}