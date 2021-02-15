using System;
using System.Collections.Generic;
using System.Text;

namespace CecSessions.Core.Models.Session
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public bool IsActive { get; set; }
        public int QuestionId { get; set; }
        public int ResultId { get; set; }
    }
}
