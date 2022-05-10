using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Orizon.Rest.Chat.Domain.Paginacao
{
    public static class DataPagerExtension
    {
        public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
            this IQueryable<TModel> query, int page, int limit, CancellationToken cancellationToken, string[] includes = default
            ) where TModel : class
        {

            if (includes != null)
                foreach (var property in includes);

            var paged = new PagedModel<TModel>();

            page = (page < 0) ? 1 : page;

            paged.CurrentPage = page;
            paged.PageSize = limit;

            //var totalItemsCountTask = await query.CountAsync(cancellationToken);

            var startRow = (page - 1) * limit;

            //paged.Items = await query.Skip(startRow).Take(limit).ToListAsync(cancellationToken);

            //paged.TotalItems = totalItemsCountTask;
            paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

            return paged;
        }
    }
}
