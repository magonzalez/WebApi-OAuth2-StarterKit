namespace StarterKit.Framework.Validation
{
    public class Validation : ErrorInfo
    {
        public Validation(string itemName, string errorMessage, ErrorCode errorCode)
            : base(itemName, errorMessage, errorCode)
        {
        }
    }
}
