using CecSessions.Core.Entities;

namespace CecSessions.Core
{
    public class UserRepasitory : Repositories<ApplicationUser>, IUserRepasitory
    {
        public UserRepasitory(CecSessionsContext dbContext) : base(dbContext) { }
    }
}
