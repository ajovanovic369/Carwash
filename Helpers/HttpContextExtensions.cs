using Microsoft.EntityFrameworkCore;

namespace CarWash.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPaginationParametersInResponse<T>(this HttpContext httpContext,
            IQueryable<T> queryable, int recordsPerPage)
        {
            if (httpContext == null) { throw new ArgumentNullException(nameof(httpContext)); }

            double count = await queryable.CountAsync();
            double totalAmountPages = Math.Ceiling(count / recordsPerPage);
            httpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Authorization");
            httpContext.Response.Headers.Add("totalAmountPages", totalAmountPages.ToString());
            httpContext.Response.Headers.Add("count", count.ToString());
            
        }
    }
}
