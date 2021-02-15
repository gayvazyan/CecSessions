using CecSessions.Core.Entities;
using CecSessions.Core.Models.Session;

namespace CecSessions.Core
{
    public class ResultRepasitory : Repositories<Result>, IResultRepasitory
    {
        public ResultRepasitory(CecSessionsContext dbContext) : base(dbContext) { }
    }
}
