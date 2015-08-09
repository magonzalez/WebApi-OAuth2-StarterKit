namespace StarterKit.Framework
{
    public class ErrorInfo
    {
        public ErrorInfo(string itemName, string errorMessage, ErrorCode errorCode)
        {
            ItemName = itemName;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public string ItemName { get; set; }

        public string ErrorMessage { get; set; }

        public ErrorCode ErrorCode { get; set; }
    }
}
