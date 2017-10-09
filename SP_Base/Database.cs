﻿using SP_Base.Models;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace SP_Base
{
    public static class Database
    {
        private static string _connection_string = "";
        private static readonly object _sync_root = new object();

        static Database()
        {
            // By default connection string is taken from web.config
            ConnectionString = ConfigurationManager.ConnectionStrings["BaseEntities"].ConnectionString;
        }

        /// <summary>
        ///     ADO.NET connection string.
        ///     This property was created to allow to set connection strings dynamically
        ///     for test and development purposes.
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                lock (_sync_root)
                {
                    return _connection_string;
                }
            }

            set
            {
                lock (_sync_root)
                {
                    _connection_string = value;
                }
            }
        }

        public static SP_BaseEntities SP_BaseModel()
        {
            return new SP_BaseEntities(ConnectionString);
        }
    }

    public static class DataContextExtension
    {
        public static void DeleteWhere<T>(this DbContext db, Expression<Func<T, bool>> filter)
            where T : class
        {
            var query = db.Set<T>().Where(filter);

            var select_sql = query.ToString();
            var delete_sql = "DELETE [Extent1] " + select_sql.Substring(select_sql.IndexOf("FROM"));

            var internal_query =
                query.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.Name == "_internalQuery")
                    .Select(field => field.GetValue(query))
                    .First();
            var object_query =
                internal_query.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.Name == "_objectQuery")
                    .Select(field => field.GetValue(internal_query))
                    .First() as ObjectQuery;
            var parameters = object_query.Parameters.Select(p => new SqlParameter(p.Name, p.Value)).ToArray();

            db.Database.ExecuteSqlCommand(delete_sql, parameters);
        }
    }
}
