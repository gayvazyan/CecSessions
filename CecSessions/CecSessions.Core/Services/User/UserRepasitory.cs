using CecSessions.Core.Entities;

namespace CecSessions.Core
{
    public class UserRepasitory : Repositories<UserDb>, IUserRepasitory
    {
        public UserRepasitory(CecSessionsContext dbContext) : base(dbContext) { }
    }
}
