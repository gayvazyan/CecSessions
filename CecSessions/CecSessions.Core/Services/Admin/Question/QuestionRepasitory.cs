using CecSessions.Core.Entities;
using CecSessions.Core.Models.Session;

namespace CecSessions.Core
{
    public class QuestionRepasitory : Repositories<Question>, IQuestionRepasitory
    {
        public QuestionRepasitory(CecSessionsContext dbContext) : base(dbContext) { }
    }
}
