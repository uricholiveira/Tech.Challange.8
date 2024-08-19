using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<Person> IncludeAll(this IQueryable<Person> query)
    {
        return query
            .Include(p => p.Name)
            .Include(p => p.Login)
            .Include(p => p.Dob)
            .Include(p => p.Registered)
            .Include(p => p.IdInfo)
            .Include(p => p.Picture);
    }
}