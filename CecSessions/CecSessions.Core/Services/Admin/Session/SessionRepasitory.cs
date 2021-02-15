using CecSessions.Core.Entities;
using CecSessions.Core.Models.Session;

namespace CecSessions.Core
{
    public class SessionRepasitory : Repositories<Session>, ISessionRepasitory
    {
        public SessionRepasitory(CecSessionsContext dbContext) : base(dbContext) { }
    }
}
