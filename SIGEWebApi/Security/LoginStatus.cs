using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIGEWebApi.Security
{
    /// <summary>
    /// Login Status
    /// </summary>
    public enum LoginStatus
    {
        Success,
        Failed,
        Disabled,
        PasswordExpired,
        LockedOut,
        DoesNotBelongToSecurityGroup
    }
}