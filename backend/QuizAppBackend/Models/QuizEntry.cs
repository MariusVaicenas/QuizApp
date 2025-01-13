
using System;

namespace QuizAppBackend.Models
{
    public class QuizEntry
    {
        public int Id { get; set; }
        public string Email { get; set; }  
        public int Score { get; set; }     
        public DateTime DateTime { get; set; }  
    }
}

