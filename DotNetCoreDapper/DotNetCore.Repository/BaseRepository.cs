using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using DotNetCore.Entities;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Data;
using System.Linq;
using DotNetCore.Repository.Interfaces;

namespace DotNetCore.Repository
{
    /// <summary>
    /// 基础操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly Option _option;
        protected readonly MySqlConnection connection;
        public BaseRepository(IOptionsSnapshot<Option> optionsAccessor)
        {
            try
            {
                _option = optionsAccessor.Value;
                connection = new MySqlConnection() { ConnectionString = _option.ConnectionString };
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<T> GetAll()
        //{
        //    using (var conn = connection)
        //    {
        //        return conn.GetList<T>();
        //    }
        //}

        /// <summary>
        /// 根据sql查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数列表</param>
        /// <returns></returns>
        public IEnumerable<T> SelectCommond(string sql, object parameters = null)
        {
            return connection.Query<T>(sql, parameters);
        }
        /// <summary>
        /// 根据表名查询
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(string tableName)
        {
            return connection.Query<T>(string.Format("select * from {0}", tableName));

        }
        /// <summary>
        /// 根据主键ID查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int? id)
        {
            if (id == null)
                return default(T);
            using (var conn = connection)
            {

                //return conn.QueryFirst<T>(string.Format("select * from {0}", tableName);
                return default(T);
            }
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        //public bool Update(T t)
        //{
        //    using (var conn = connection)
        //    {
        //        //t.EditorDate = DateTime.Now;
        //        return conn.Update(t);
        //    }
        //}

        /// <summary>
        /// 得到数量
        /// </summary>
        /// <returns></returns>
        //public int GetCount()
        //{
        //    using (var conn = connection)
        //    {
        //        return conn.Count<T>(null);
        //    }
        //}

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="pageindex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        //public Tuple<int, IEnumerable<T>> GetPaged(object predicate, int pageindex, int pageSize)
        //{
        //    using (var conn = connection)
        //    {
        //        var sort = new List<ISort>
        //        {
        //            //Predicates.Sort<T>(p=>p.EditorDate)
        //        };

        //        var total = conn.Count<T>(predicate);
        //        return new Tuple<int, IEnumerable<T>>(total, conn.GetPage<T>(predicate, sort, pageindex, pageSize).ToList());
        //    }
        //}
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <returns></returns>
        //public dynamic Insert(T t)
        //{
        //    //t.EditorDate = DateTime.Now;
        //    return this.Add(t, false);
        //}
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //public bool Insert(List<T> list)
        //{
        //    using (var conn = connection)
        //    {
        //        //list.ForEach(p => p.EditorDate = DateTime.Now);
        //        return conn.Insert(list);
        //    }
        //}


        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="t">实体对象</param>
        /// <param name="isAutoGenId"></param>
        /// <returns></returns>
        //public dynamic Add(T t, bool isAutoGenId = true)
        //{
        //    using (var conn = connection)
        //    {
        //        //var maxindex = conn.Query<int?>(string.Format("select max(indexs) from {0}", typeof(T).Name)).FirstOrDefault() ?? 0;
        //        //t.Indexs = maxindex + 1;
        //        dynamic tt = conn.Insert(t);

        //        return tt;
        //    }
        //}
        /// <summary>
        /// 根据编号删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public bool Delete(int? id)
        //{
        //    var obj = this.GetById(id);
        //    if (obj == null) return false;
        //    return this.Delete(obj);
        //}
        /// <summary>
        /// 根据编号修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mark"></param>
        /// <returns></returns>
        //public bool UpdataStatus(int? id)
        //{
        //    //var obj = this.GetById(id);
        //    if (obj == null) return false;
        //    return this.Update(obj);
        //}
        /// <summary>
        /// 根据外键得到数据
        /// </summary>
        /// <param name="foreignKeyName">外键名称</param>
        /// <param name="foreignKeyValue">外键的值</param>
        /// <returns></returns>
        public IEnumerable<T> GetByForeignKey(string foreignKeyName, Guid foreignKeyValue)
        {

            return connection.Query<T>(string.Format("select * from {0} where {1}=@value", typeof(T).Name, foreignKeyName), new { value = foreignKeyValue });

        }
        /// <summary>
        /// 根据列查询
        /// </summary>
        /// <param name="fieldName">列名称</param>
        /// <param name="fieldValue">列的值</param>
        /// <returns></returns>
        public IEnumerable<T> GetByField(string fieldName, dynamic fieldValue)
        {

            return connection.Query<T>(string.Format("select * from {0} where {1}=@value", typeof(T).Name, fieldName), new { value = fieldValue });

        }
        /// <summary>
        /// 据某列查询的方法--带排序
        /// </summary>
        /// <param name="fieldName">查询列名</param>
        /// <param name="fieldValue">条件内容</param>
        /// <param name="sortFieldName">排序列名</param>
        /// <returns></returns>
        public IEnumerable<T> GetByField(string fieldName, dynamic fieldValue, string sortFieldName)
        {
            return connection.Query<T>(string.Format("select * from {0} where {1}=@value order by {2}", typeof(T).Name, fieldName, sortFieldName), new { value = fieldValue });
        }

        /// <summary>
        ///获取排序号的方法
        /// </summary>
        /// <returns></returns>
        //public int GetNextSequence(T t)
        //{
        //    using (var conn = ConnectionFactory.Connection)
        //    {
        //        return conn.Query<int>(string.Format("select isnull(max(Sequence),0)+1 from {0}", typeof(T).Name)).FirstOrDefault();
        //    }
        //}
        /// <summary>
        /// 存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public List<dynamic> SelectProc(string procName, object obj = null)
        {
            return connection.Query(procName, obj, commandType: CommandType.StoredProcedure).ToList();
        }

    }
}
