using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizController(QuizContext context)
        {
            _context = context;
        }

        // Get the list of questions with their options
        [HttpGet("questions")]
        public IActionResult GetQuestions()
        {
            var questions = _context.Questions
                .Include(q => q.Options) // Include options for each question
                .ToList();

            return Ok(questions);
        }

        // Submit the quiz and save the results
        [HttpPost("submit")]
        public IActionResult SubmitQuiz([FromBody] QuizSubmission submission)
        {
            // Validate submission data
            if (submission == null || string.IsNullOrEmpty(submission.Email) || submission.Answers == null)
            {
                return BadRequest("Invalid submission data.");
            }

            Console.WriteLine($"Received submission from: {submission.Email}");

            int score = 0;

            // Calculate the score based on the answers
            foreach (var answer in submission.Answers)
            {
                var question = _context.Questions
                    .Include(q => q.Options)
                    .FirstOrDefault(q => q.Id == answer.QuestionId);

                if (question == null)
                    continue;

                // Logic for calculating score based on question type
                if (question.Type == "Radio" || question.Type == "Text")
                {
                    if (!string.IsNullOrEmpty(answer.Text) &&
                        answer.Text.Equals(question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        score += 100; // Correct answer gets 100 points
                    }

                }
                else if (question.Type == "Checkbox")
                {
                    var correctOptions = question.Options.Where(o => o.IsCorrect).Select(o => o.Text).ToList();
                    var submittedOptions = string.IsNullOrEmpty(answer.Text)
                        ? new List<string>() 
                        : answer.Text.Split(',').Select(o => o.Trim()).ToList();

                    // Calculate the number of correct answers the user selected
                    var correctlyChecked = submittedOptions.Count(o => correctOptions.Contains(o));

                    // Calculate the number of incorrect answers the user selected
                    var incorrectlyChecked = submittedOptions.Count(o => !correctOptions.Contains(o));

                    // Calculate the score with penalty
                    var partialScore = (100.0 / correctOptions.Count) * (correctlyChecked - incorrectlyChecked);

                    // Ensure the score is not negative
                    if (partialScore > 0)
                    {
                        score += (int)Math.Ceiling(partialScore);
                    }
                }
            }

            // Create and save the quiz entry
            var quizEntry = new QuizEntry
            {
                Email = submission.Email,
                Score = score,
                DateTime = DateTime.Now
            };

            _context.QuizEntries.Add(quizEntry);
            _context.SaveChanges();

            return Ok(new { quizEntry.Email, quizEntry.Score });
        }

        // Get the high scores
        [HttpGet("highscores")]
        public IActionResult GetHighScores()
        {
            var highScores = _context.QuizEntries
                .OrderByDescending(q => q.Score) // Sort by score descending
                .ThenByDescending(q => q.DateTime) // If scores are equal, sort by date descending
                .Take(10) // Only take the top 10 scores
                .Select(q => new
                {
                    q.Email,
                    q.Score,
                    q.DateTime
                })
                .ToList();

            return Ok(highScores);
        }
    }
}
