using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Security
{
    public class LoginSession : DataModel
    {
        public Guid UserId { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime? LogoutDateTime { get; set; }
    }
}
