using System.Data;

namespace StarterKit.Framework.Data
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection(string csName = null);
    }
}
