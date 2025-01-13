using System;
using System.Collections.Generic;

namespace QuizAppBackend.Models
{
    public class QuizSubmission
    {
        public string Email { get; set; } 
        public List<Answer> Answers { get; set; } 
    }
}
