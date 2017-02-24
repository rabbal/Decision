using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace NTierMvcFramework.Common.KendoLinq
{
    public static class QueryableExtensions
    {

        #region ToListResponse
        /// <summary>
        ///     Applies data processing (paging, sorting, filtering and aggregates) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <param name="aggregates">Specifies the current aggregates.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static TResponse ToListResponse<T, TResponse>(this IQueryable<T> queryable, int take, int skip,
            IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates) where TResponse : BaseListResponse, new()
        {
            queryable = queryable.Filter(filter);

            var total = queryable.LongCount();

            var aggregate = queryable.Aggregate(aggregates);

            queryable = queryable.Sort(sort);

            if (take > 0)
            {
                queryable = queryable.Page(take, skip);
            }

            return new TResponse
            {
                Data = queryable.ToList(),
                Total = total,
                Aggregates = aggregate
            };
        }

        /// <summary>
        ///     Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static TResponse ToListResponse<T, TResponse>(this IQueryable<T> queryable, int take, int skip,
            IEnumerable<Sort> sort, Filter filter) where TResponse : BaseListResponse, new()
        {
            return queryable.ToListResponse<T, TResponse>(take, skip, sort, filter, null);
        }

        /// <summary>
        ///     Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="request">The DataSourceRequest object containing take, skip, order, and filter data.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static TResponse ToListResponse<T, TResponse>(this IQueryable<T> queryable, BaseListRequest request) where TResponse : BaseListResponse, new()
        {
            return queryable.ToListResponse<T, TResponse>(request.Take, request.Skip, request.Sorts, request.Filter, null);
        }

        #endregion

        #region ToListResponseAsync
        /// <summary>
        ///     Applies data processing (paging, sorting, filtering and aggregates) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <param name="aggregates">Specifies the current aggregates.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static async Task<TResponse> ToListResponseAsync<T, TResponse>(this IQueryable<T> queryable, int take, int skip,
            IEnumerable<Sort> sort, Filter filter, IEnumerable<Aggregator> aggregates) where TResponse:BaseListResponse,new()
        {
            queryable = queryable.Filter(filter);

            var total = await queryable.LongCountAsync();

            var aggregate = queryable.Aggregate(aggregates);

            queryable = queryable.Sort(sort);

            if (take > 0)
            {
                queryable = queryable.Page(take, skip);
            }

            return new TResponse
            {
                Data = await queryable.ToListAsync(),
                Total = total,
                Aggregates = aggregate
            };
        }

        /// <summary>
        ///     Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="take">Specifies how many items to take. Configurable via the pageSize setting of the Kendo DataSource.</param>
        /// <param name="skip">Specifies how many items to skip.</param>
        /// <param name="sort">Specifies the current sort order.</param>
        /// <param name="filter">Specifies the current filter.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static Task<TResponse> ToListResponseAsync<T, TResponse>(this IQueryable<T> queryable, int take, int skip,
            IEnumerable<Sort> sort, Filter filter)where TResponse:BaseListResponse,new()
        {
            return queryable.ToListResponseAsync<T, TResponse>(take, skip, sort, filter, null);
        }

        /// <summary>
        ///     Applies data processing (paging, sorting and filtering) over IQueryable using Dynamic Linq.
        /// </summary>
        /// <typeparam name="T">The type of the IQueryable.</typeparam>
        /// <param name="queryable">The IQueryable which should be processed.</param>
        /// <param name="request">The DataSourceRequest object containing take, skip, order, and filter data.</param>
        /// <returns>A ListResponse object populated from the processed IQueryable.</returns>
        public static Task<TResponse> ToListResponseAsync<T, TResponse>(this IQueryable<T> queryable, BaseListRequest request) where TResponse:BaseListResponse,new()
        {
            return queryable.ToListResponseAsync<T, TResponse>(request.Take, request.Skip, request.Sorts, request.Filter, null);
        }

        #endregion

        #region Extension Methods
        public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, Filter filter)
        {
            if (filter?.Logic == null) return queryable;

            var filters = filter.All();

            // Get all filter values as array (needed by the Where method of Dynamic Linq)
            var values = filters.Select(f => f.Value).ToArray();

            // Create a predicate expression e.g. Field1 = @0 And Field2 > @1
            var predicate = filter.ToExpression(filters);

            // Use the Where method of Dynamic Linq to filter the data
            queryable = queryable.Where(predicate, values);

            return queryable;
        }

        public static object Aggregate<T>(this IQueryable<T> queryable, IEnumerable<Aggregator> aggregates)
        {
            if (aggregates == null) return null;

            var aggregators = aggregates as Aggregator[] ?? aggregates.ToArray();
            if (!aggregators.Any()) return null;

            var objProps = new Dictionary<DynamicProperty, object>();
            var groups = aggregators.GroupBy(g => g.Field);
            Type type;
            foreach (var group in groups)
            {
                var fieldProps = new Dictionary<DynamicProperty, object>();
                foreach (var aggregate in group)
                {
                    var prop = typeof(T).GetProperty(aggregate.Field);
                    var param = Expression.Parameter(typeof(T), "s");
                    var selector = aggregate.Aggregate == "count" &&
                                   (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                        ? Expression.Lambda(
                            Expression.NotEqual(Expression.MakeMemberAccess(param, prop),
                                Expression.Constant(null, prop.PropertyType)), param)
                        : Expression.Lambda(Expression.MakeMemberAccess(param, prop), param);
                    var mi = aggregate.MethodInfo(typeof(T));
                    if (mi == null)
                        continue;

                    var val = queryable.Provider.Execute(Expression.Call(null, mi,
                        aggregate.Aggregate == "count" && (Nullable.GetUnderlyingType(prop.PropertyType) == null)
                            ? new[] { queryable.Expression }
                            : new[] { queryable.Expression, Expression.Quote(selector) }));

                    fieldProps.Add(new DynamicProperty(aggregate.Aggregate, typeof(object)), val);
                }
                type = System.Linq.Dynamic.DynamicExpression.CreateClass(fieldProps.Keys);
                var fieldObj = Activator.CreateInstance(type);
                foreach (var p in fieldProps.Keys)
                    type.GetProperty(p.Name).SetValue(fieldObj, fieldProps[p], null);
                objProps.Add(new DynamicProperty(group.Key, fieldObj.GetType()), fieldObj);
            }

            type = System.Linq.Dynamic.DynamicExpression.CreateClass(objProps.Keys);

            var obj = Activator.CreateInstance(type);

            foreach (var p in objProps.Keys)
            {
                type.GetProperty(p.Name).SetValue(obj, objProps[p], null);
            }

            return obj;
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort == null) return queryable;

            var sorts = sort as Sort[] ?? sort.ToArray();
            if (!sorts.Any()) return queryable;

            var ordering = string.Join(",", sorts.Select(s => s.ToExpression()));

            return queryable.OrderBy(ordering);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int take, int skip)
        {
            return queryable.Skip(() => skip).Take(() => take);
        }
        #endregion

    }
}