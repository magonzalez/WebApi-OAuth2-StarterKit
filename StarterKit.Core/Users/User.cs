using System;
using StarterKit.Framework.Data;

namespace StarterKit.Core.Users
{
    public class User : DataModel
    {
        /// <summary>
        /// The type of user determined by integer values
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The last time this user logged in.
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// The amount of times the user logged in unsuccessfully for one session.
        /// </summary>
        public byte LoginAttempts { get; set; }

        /// <summary>
        /// The locked status of the user.
        /// </summary>
        public bool Locked { get; set; }

        /// <summary>
        /// The date time that the user was first created and saved into the system
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }
    }
}
