namespace Ravm.Application.Extensions;

using Microsoft.EntityFrameworkCore;
using Ravm.Domain.Common;
using Ravm.Domain.Models;

public static class DbSetExtensions
{
    public static IQueryable<Organization> IncludeParents(this DbSet<Organization> query, Guid tenantId)
    {
        return query.FromSqlRaw(@$"
WITH RECURSIVE cte_organizations AS (
    SELECT *
    FROM ""Organizations""
    WHERE ""Id"" = '{tenantId}'

    UNION ALL

    SELECT c.*
    FROM ""Organizations"" c
    INNER JOIN cte_organizations r ON r.""ParentId"" = c.""Id""
)

SELECT * FROM cte_organizations");
    }

    public static IQueryable<Organization> IncludeChilds(this DbSet<Organization> query, Guid tenantId)
    {
        return query.FromSqlRaw(@$"
WITH RECURSIVE cte_organizations AS (
    SELECT *
    FROM ""Organizations""
    WHERE ""Id"" = '{tenantId}'

    UNION ALL

    SELECT c.*
    FROM ""Organizations"" c
    INNER JOIN cte_organizations r ON r.""Id"" = c.""ParentId""
)

SELECT * FROM cte_organizations");
    }

    public static IQueryable<TEntity> IncludeChilds<TEntity>(this DbSet<TEntity> query, Guid tenantId)
        where TEntity : class, IHasOrganization
    {
        var tableName = query.EntityType.GetAnnotation("Relational:TableName");

        return query.FromSqlRaw(@$"
WITH RECURSIVE cte_organizations AS (
    SELECT ""Id"", ""ParentId""
    FROM ""Organizations""
    WHERE ""Id"" = '{tenantId}'

    UNION ALL

    SELECT c.""Id"", c.""ParentId""
    FROM ""Organizations"" c
    INNER JOIN cte_organizations r ON r.""Id"" = c.""ParentId""
)

SELECT e.* FROM ""{tableName.Value}"" e
INNER JOIN cte_organizations o ON o.""Id"" = e.""OrganizationId""");
    }
}
