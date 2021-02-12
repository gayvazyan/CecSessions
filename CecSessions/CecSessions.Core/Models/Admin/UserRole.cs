using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Models.Admin
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
