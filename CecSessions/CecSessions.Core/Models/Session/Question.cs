using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Models.Session
{
  public class Question
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Body { get; set; }
        public int ResultId { get; set; }
    }
}
