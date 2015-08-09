using System;
using System.Collections.Generic;

using Dapper;

using StarterKit.Framework.Data;
using StarterKit.Framework.Logging;

namespace StarterKit.Framework.Data.Dapper
{
    public class DataModelRepository<T> : Repository, IDataModelRepository<T> where T : IDataModel
    {
        public DataModelRepository(IConnectionFactory connectionFactory, ILogger logger)
            : base(connectionFactory, logger)
        {
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.GetList<T>();
            }
        }

        public virtual T Get(Guid id)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Get<T>(id);
            }
        }

        public virtual Guid Insert(T entity)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                return conn.Insert<Guid>(entity);
            }
        }

        public virtual void Update(T entity)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                conn.Update(entity);
            }
        }

        public virtual void Delete(Guid id)
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                conn.Delete<T>(id);
            }
        }

        public void DeleteAll()
        {
            using (var conn = ConnectionFactory.GetConnection())
            {
                var entities = conn.GetList<T>();
                foreach (var entity in entities)
                {
                    conn.Delete(entity);
                }
            }
        }
    }
}
