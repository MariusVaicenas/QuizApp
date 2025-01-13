
using QuizAppBackend.Models; 

public class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Type { get; set; }
    public string CorrectAnswer { get; set; }
    public List<Option> Options { get; set; } 
}
