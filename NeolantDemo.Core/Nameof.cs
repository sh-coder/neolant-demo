using System;
using System.Linq.Expressions;

namespace NeolantDemo.Core
{
    /// <summary>
    /// Class Nameof.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Nameof<T>
    {
        /// <summary>
        /// Properties the specified expression.
        /// </summary>
        /// <typeparam name="TProp">The type of the t property.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentException">'expression' should be a member expression</exception>
        public static string Property<TProp>(Expression<Func<T, TProp>> expression)
        {
            var body = expression.Body as MemberExpression;
            if (body == null)
                throw new ArgumentException("'expression' should be a member expression");
            return body.Member.Name;
        }
    }
}