using System.Threading.Tasks;

namespace StarterKit.Framework.Extensions
{
    public static class ThreadingExtensions
    {
        public static Task NoResult
        {
            get { return Task.FromResult((object)null); }
        }
    }
}
