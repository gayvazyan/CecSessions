using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Models.Session
{
   public class Result
    {
        public int Id { get; set; }
        public bool Side { get; set; }
        public bool Opposite { get; set; }
        public bool Abstain { get; set; }
        public string UserId { get; set; }
    }
}
