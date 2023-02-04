using CarWash.DTOs;

namespace CarWash.Helpers
{
    public static class QueryableExtensionsEarning
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, EarningsPaginationDTO pagination)
        {
            return queryable
                .Skip((pagination.Page - 1) * pagination.RecordsPerPage)
                .Take(pagination.RecordsPerPage);
        }
    }
}
