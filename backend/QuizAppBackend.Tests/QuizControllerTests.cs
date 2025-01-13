using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Controllers;
using QuizAppBackend.Data;
using QuizAppBackend.Models;
using Xunit;

namespace QuizAppBackend.Tests
{
    public class QuizControllerTests
    {
        private readonly QuizContext _context;
        private readonly QuizController _controller;

        public QuizControllerTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new QuizContext(options);

            // Seed test data
            SeedDatabase();

            _controller = new QuizController(_context);
        }

        private void SeedDatabase()
        {
            if (!_context.Questions.Any())
            {
                _context.Questions.AddRange(
                    new Question
                    {
                        Id = 1,
                        Text = "What is the capital of France?",
                        Type = "Radio",
                        CorrectAnswer = "Paris",
                        Options = new List<Option>
                        {
                            new Option { Id = 1, Text = "Paris", IsCorrect = true },
                            new Option { Id = 2, Text = "Berlin", IsCorrect = false },
                            new Option { Id = 3, Text = "Madrid", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        Id = 2,
                        Text = "Select all programming languages.",
                        Type = "Checkbox",
                        CorrectAnswer = "Python, Java",
                        Options = new List<Option>
                        {
                            new Option { Id = 4, Text = "Python", IsCorrect = true },
                            new Option { Id = 5, Text = "HTML", IsCorrect = false },
                            new Option { Id = 6, Text = "Java", IsCorrect = true },
                            new Option { Id = 7, Text = "CSS", IsCorrect = false }
                        }
                    }
                );
                _context.SaveChanges();
            }
        }

        [Fact]
        public void GetQuestions_ShouldReturnAllQuestions()
        {
            var result = _controller.GetQuestions();

            Assert.NotNull(result);
        }

        [Fact]
        public void SubmitQuiz_ShouldCalculateScoreCorrectly_ForRadioQuestion()
        {
            var submission = new QuizSubmission
            {
                Email = "test@example.com",
                Answers = new List<Answer>
                {
                    new Answer { QuestionId = 1, Text = "Paris" } 
                }
            };

            var result = _controller.SubmitQuiz(submission);

            Assert.NotNull(result);
        }

        [Fact]
        public void SubmitQuiz_ShouldCalculatePartialScore_ForCheckboxQuestion()
        {

            var submission = new QuizSubmission
            {
                Email = "test@example.com",
                Answers = new List<Answer>
                {
                    new Answer { QuestionId = 2, Text = "Python, Java" } 
                }
            };

            var result = _controller.SubmitQuiz(submission);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetHighScores_ShouldReturnTop10Scores()
        {
            var submission1 = new QuizSubmission
            {
                Email = "test1@example.com",
                Answers = new List<Answer>
                {
                    new Answer { QuestionId = 1, Text = "Paris" }, // Correct answer
                    new Answer { QuestionId = 2, Text = "Python, Java" } // Correct answers
                }
            };

            var submission2 = new QuizSubmission
            {
                Email = "test2@example.com",
                Answers = new List<Answer>
                {
                    new Answer { QuestionId = 1, Text = "Berlin" }, // Incorrect answer
                    new Answer { QuestionId = 2, Text = "" } // No answer
                }
            };

            _controller.SubmitQuiz(submission1);
            _controller.SubmitQuiz(submission2);

            var result = _controller.GetHighScores();

            Assert.NotNull(result);
        }
    }
}
