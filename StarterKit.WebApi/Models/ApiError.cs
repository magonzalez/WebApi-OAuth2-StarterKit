using StarterKit.Framework;

namespace StarterKit.WebApi.Models
{
    /// <summary>
    /// Error returned inside an HttpError from the API layer; it contains the ErrorCode enum value for this particular error, 
    /// and a error message
    /// </summary>
    public class ApiError
    {
        /// <summary>
        /// Gets and sets the resource-specific error code for this error
        /// </summary>
        public ErrorCode ErrorCode { get; set; }
        /// <summary>
        /// Gets and sets the error message associated with this error
        /// </summary>
        public string Message { get; set; }
    }
}