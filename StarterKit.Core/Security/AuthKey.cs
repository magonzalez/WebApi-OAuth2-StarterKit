using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Security
{
    public class AuthKey : DataModel
    {
        public Guid UserId { get; set; }
        public Guid IssuedGuid { get; set; }
    }
}
