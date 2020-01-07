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
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private string dbString;

        public BaseRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings");
            dbString = connectionString.GetSection("db").Value;
        }

        protected SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(dbString);
            return con;
        }

        public void Delete(int id)
        {
            string SQL = EntityUtil.GetDeleteQuery<TEntity>();

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        public void Insert(TEntity obj)
        {
            string SQL = EntityUtil.GetInsertQuery(obj);

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                con.Execute(SQL, obj);
                con.Close();
            }
        }

        public TEntity Select(int id)
        {
            string sql = EntityUtil.GetSelectByIdQuery<TEntity>();
            TEntity result;

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                result = con.QueryFirstOrDefault<TEntity>(sql, new { ID = id });
                con.Close();
            }
            return result;
        }

        public IList<TEntity> SelectAll()
        {
            string SQL = EntityUtil.GetSelectAllQuery<TEntity>();

            List<TEntity> list = null;

            using (SqlConnection con = GetConnection())
            {
                con.Open();
                list = con.Query<TEntity>(SQL).ToList();
                con.Close();
            }

            return list;
        }

        public void Update(TEntity obj)
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
