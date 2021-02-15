using CecSessions.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Services.ProcedureTest
{
    public interface IProcedureTest
    {
        List<IdentityRole> GetRoles();
        IdentityRole GetRole(string id);
    }
}
