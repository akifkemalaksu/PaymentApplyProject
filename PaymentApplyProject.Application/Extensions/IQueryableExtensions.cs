﻿using PaymentApplyProject.Application.Dtos.DatatableDtos;
using System.Linq.Expressions;

namespace PaymentApplyProject.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string orderByMember, DtOrderDir orderDirection)
        {
            var param = Expression.Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var queryable = orderDirection == DtOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Expression.Lambda(body, param));

            return queryable;
        }
    }
}