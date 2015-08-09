using System;
using System.Data;

using StarterKit.Framework.Logging;

namespace StarterKit.Framework.Data.Dapper
{
    public class Repository
    {
        protected readonly DateTime SqlMinValue = new DateTime(1900, 1, 1);

        public Repository(IConnectionFactory connectionFactory, ILogger logService)
        {
            ConnectionFactory = connectionFactory;
            Logger = logService;
        }

        protected IConnectionFactory ConnectionFactory { get; private set; }

        protected ILogger Logger { get; private set; }

        protected DateTime GetValidSqlDateTime(DateTime original)
        {
            if (original < SqlMinValue)
                return SqlMinValue;

            return original;
        }

        protected T Query<T>(Func<IDbConnection, T> func)
        {
            using (IDbConnection connection = ConnectionFactory.GetConnection())
            {
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return func(connection);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error while querying database.");
                }
                finally
                {
                    connection.Close();
                }
            }

            return default(T);
        }

        protected T Execute<T>(Func<IDbConnection, T> func)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    return func(connection);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error while executing command.");

                    throw;
                }
            }
        }

        protected void Execute(Action<IDbConnection> action)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                try
                {
                    action(connection);
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Error while executing command.");

                    throw;
                }
            }
        }

        protected T ExecuteTransaction<T>(Func<IDbConnection, IDbTransaction, T> func)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    var result = func(connection, transaction);

                    transaction.Commit();

                    return result;
                }
                catch (Exception)
                {
                    try
                    {
                        // Attempt to roll back the transaction.
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Logger.Warning(string.Format("Rollback Exception Type: {0},  Message: {1}",
                            ex2.GetType(), ex2.Message));
                    }

                    throw;
                }
            }
        }

        protected void ExecuteTransaction(Action<IDbConnection, IDbTransaction> action)
        {
            using (var connection = ConnectionFactory.GetConnection())
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();

                var transaction = connection.BeginTransaction();

                try
                {
                    action(connection, transaction);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    try
                    {
                        // Attempt to roll back the transaction.
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Logger.Warning(string.Format("Rollback Exception Type: {0},  Message: {1}",
                            ex2.GetType(), ex2.Message));
                    }

                    throw;
                }
            }
        }
    }
}
