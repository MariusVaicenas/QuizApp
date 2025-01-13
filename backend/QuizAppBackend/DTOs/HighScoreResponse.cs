namespace QuizAppBackend.DTOs
{
    public class HighScoreResponse
    {
        public string Email { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
    }
}
