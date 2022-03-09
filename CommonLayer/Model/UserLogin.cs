//-----------------------------------------------------------------------
// <copyright file="UserLogin.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace CommonLayer.Model
{
using System;
using System.Collections.Generic;
using System.Text;

    /// <summary>
    /// User Login Model
    /// </summary>
    public class UserLogin
    {
        /// <summary>Gets or sets the email.</summary>
        /// <value>The email.</value>
        public string Email { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        public string Password { get; set; }
    }
}
