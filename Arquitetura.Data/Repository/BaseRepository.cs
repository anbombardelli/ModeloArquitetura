using Arquitetura.Domain.Entities;
using Arquitetura.Domain.Interfaces.Repository;
using Arquitetura.Lib.Util;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Arquitetura.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private string dbString;

        public BaseRepository(IConfiguration configuration)
        {
             dbString = configuration.GetConnectionString("DatabaseConnection");
        }

        protected SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(dbString);
            return con;
        }

        public void Delete(int id)
        {
            string SQL = EntityUtil.GetDeleteQuery<T>();

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        public void Insert(T obj)
        {
            string SQL = EntityUtil.GetInsertQuery(obj);

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                con.Execute(SQL, obj);
                con.Close();
            }
        }

        public T Select(int id)
        {
            string sql = EntityUtil.GetSelectByIdQuery<T>();
            T result;

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                result = con.QueryFirstOrDefault<T>(sql, new { ID = id });
                con.Close();
            }
            return result;
        }

        public IList<T> SelectAll()
        {
            string SQL = EntityUtil.GetSelectAllQuery<T>();

            List<T> list = null;

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                list = con.Query<T>(SQL).ToList();
                con.Close();
            }

            return list;
        }

        public void Update(T obj)
        {
            string sql = EntityUtil.GetUpdateQuery(obj);

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                con.Execute(sql, obj);
                con.Close();
            }
        }
    }
}
