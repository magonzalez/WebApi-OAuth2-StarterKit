namespace StarterKit.Framework.Data
{
    public interface IConnectionStringProvider
    {
        string GetConnectionString(string name = null);
    }
}
