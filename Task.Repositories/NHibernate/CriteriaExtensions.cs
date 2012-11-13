using System;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Impl;

namespace Task.Repositories.NHibernate
{
    static class CriteriaExtensions
    {
        /// <summary>
        /// Adds <c>T</c> expression to ICriteria instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ICriteria Add<T>(this ICriteria criteria,
                                        Expression<Func<T, bool>> expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression(expression);
            criteria.Add(criterion);
            return criteria;
        }

        /// <summary>
        /// Adds bool expression to ICriteria instance
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static ICriteria Add(this ICriteria criteria,
                                    Expression<Func<bool>> expression)
        {
            ICriterion criterion = ExpressionProcessor.ProcessExpression(expression);
            criteria.Add(criterion);
            return criteria;
        }

        public static ICriterion Between<T>(this ICriteria criteria, Expression<Func<T, double>> expression,
                                            object lo,
                                            object hi)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return Restrictions.Between(property, lo, hi);
        }
    }
}
