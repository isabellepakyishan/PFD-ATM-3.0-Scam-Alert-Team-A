using System;
using System.Data.SqlClient;

namespace PFD_ATM_3._0_Team_A.Repositories.RowMapper
{
    public abstract class RowMapper<MODEL> : IRowMapper<MODEL>
    {
        public abstract MODEL Convert(SqlDataReader reader);

        /// <summary>
        ///     Method behaviour depends on the natural of the value <br />
        ///     - If the given <paramref name="value"/> is a <see cref="DBNull"/>: the <paramref name="defaultValue"/> will be return <br />
        ///     - If the given <paramref name="value"/> is <b>NOT</b> a <see cref="DBNull"/>: cast the <paramref name="value"/> to <typeparamref name="T"/> then return <br />
        /// </summary>
        /// <typeparam name="T">Required DataType</typeparam>
        /// <param name="value">Value read from <see cref="SqlDataReader"/></param>
        /// <param name="defaultValue">Default value to return if <paramref name="value"/> is <see cref="DBNull"/></param>
        /// <returns><paramref name="defaultValue"/> if the <paramref name="value"/> is <see cref="DBNull"/> else cast the <paramref name="value"/> to <typeparamref name="T"/> then return</returns>
        protected T? GetOrElseDefault<T>(object value, T? defaultValue) where T : struct
        {
            if (value is DBNull)
            {
                return defaultValue;
            }

            return (T)value;
        }

        /// <summary>
        ///     Method behavior depends on the natural of the value <br />
        ///     - If the given <paramref name="value"/> is a <see cref="DBNull"/>: the <see cref="string.Empty"/> will be return <br />
        ///     - If the given <paramref name="value"/> is <b>NOT</b> a <see cref="DBNull"/>: cast the <paramref name="value"/> to <see cref="string"/> then return <br />
        /// </summary>
        /// <param name="value">Value read from <see cref="SqlDataReader"/></param>
        /// <returns><see cref="string.Empty"/> if the <paramref name="value"/> is <see cref="DBNull"/> else cast the <paramref name="value"/> to <see cref="string"/> then return</returns>
        protected string GetOrElseDefault(object value)
        {
            if (value is DBNull)
            {
                return string.Empty;
            }

            return (string)value;
        }

        /// <summary>
        ///     Method behaviour depends on the natural of the value <br />
        ///     - If the given <paramref name="value"/> is a <see cref="DBNull"/>: null will be return <br />
        ///     - If the given <paramref name="value"/> is <b>NOT</b> a <see cref="DBNull"/>: cast the <paramref name="value"/> to <typeparamref name="T"/> then return <br />
        /// </summary>
        /// <typeparam name="T">Required DataType</typeparam>
        /// <param name="value">Value read from <see cref="SqlDataReader"/></param>
        /// <returns>null if the <paramref name="value"/> is <see cref="DBNull"/> else cast the <paramref name="value"/> to <typeparamref name="T"/> then return</returns>
        protected T? GetOrElseDefault<T>(object value) where T : struct
        {
            if (value is DBNull)
            {
                return null;
            }

            return (T)value;
        }
    }
}
