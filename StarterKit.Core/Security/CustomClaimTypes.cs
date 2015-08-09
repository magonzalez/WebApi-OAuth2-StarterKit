using System.Runtime.InteropServices;

namespace StarterKit.Core.Security
{
    [ComVisible(false)]
    public static class CustomClaimTypes
    {
        /// <summary>
        /// The URI for a claim that specifies the user roles a user has access, http://mxtoolbox.com/identity/claims/usertype.
        /// </summary>
        public const string UserType = "http://mxtoolbox.com/identity/claims/usertype";

        /// <summary>
        /// The URI for a claim that specifies the users extended type ID (TeacherID, StudentID, etc),
        /// http://mxtoolbox.com/identity/claims/extendedusertypeid.
        /// </summary>
        public const string ExtendedUserTypeId = "http://mxtoolbox.com/identity/claims/extendedusertypeid";
    }
}
