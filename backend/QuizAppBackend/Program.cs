using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAppBackend.Data;
using QuizAppBackend.Models;

var builder = WebApplication.CreateBuilder(args);

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Allow any origin
              .AllowAnyMethod()  // Allow any HTTP method
              .AllowAnyHeader();  // Allow any header
    });
});

builder.Services.AddControllers();

// Add In-Memory Database
builder.Services.AddDbContext<QuizContext>(options =>
    options.UseInMemoryDatabase("QuizApp"));

builder.Services.AddControllers(); 

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the database with questions and options
SeedDatabase(app.Services.CreateScope().ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Method to seed the database
void SeedDatabase(IServiceProvider serviceProvider)
{
    var context = serviceProvider.GetRequiredService<QuizContext>();

    if (context.Questions.Any()) return;

    var questions = new[]
    {
        new Question
        {
            Text = "What is the capital of France?",
            Type = "Radio",
            CorrectAnswer = "Paris",
            Options = new List<Option>
            {
                new Option { Text = "Paris", IsCorrect = true },
                new Option { Text = "Berlin", IsCorrect = false },
                new Option { Text = "Madrid", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "Select all programming languages.",
            Type = "Checkbox",
            CorrectAnswer = "Python, Java",
            Options = new List<Option>
            {
                new Option { Text = "Python", IsCorrect = true },
                new Option { Text = "HTML", IsCorrect = false },
                new Option { Text = "Java", IsCorrect = true },
                new Option { Text = "CSS", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "What year did the Titanic sink?",
            Type = "Text",
            CorrectAnswer = "1912",
            Options = new List<Option>()
        },
        new Question
        {
            Text = "What is 2 + 2?",
            Type = "Radio",
            CorrectAnswer = "4",
            Options = new List<Option>
            {
                new Option { Text = "3", IsCorrect = false },
                new Option { Text = "4", IsCorrect = true },
                new Option { Text = "5", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "Select all fruits.",
            Type = "Checkbox",
            CorrectAnswer = "Apple, Banana",
            Options = new List<Option>
            {
                new Option { Text = "Apple", IsCorrect = true },
                new Option { Text = "Banana", IsCorrect = true },
                new Option { Text = "Carrot", IsCorrect = false },
                new Option { Text = "Potato", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "What is the square root of 16?",
            Type = "Text",
            CorrectAnswer = "4",
            Options = new List<Option>()
        },
        new Question
        {
            Text = "Who wrote 'Romeo and Juliet'?",
            Type = "Radio",
            CorrectAnswer = "William Shakespeare",
            Options = new List<Option>
            {
                new Option { Text = "Charles Dickens", IsCorrect = false },
                new Option { Text = "William Shakespeare", IsCorrect = true },
                new Option { Text = "Mark Twain", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "Select all prime numbers.",
            Type = "Checkbox",
            CorrectAnswer = "2, 3, 5",
            Options = new List<Option>
            {
                new Option { Text = "2", IsCorrect = true },
                new Option { Text = "3", IsCorrect = true },
                new Option { Text = "4", IsCorrect = false },
                new Option { Text = "5", IsCorrect = true }
            }
        },
        new Question
        {
            Text = "What is the chemical symbol for water?",
            Type = "Text",
            CorrectAnswer = "H2O",
            Options = new List<Option>()
        },
        new Question
        {
            Text = "What is the capital of Germany?",
            Type = "Radio",
            CorrectAnswer = "Berlin",
            Options = new List<Option>
            {
                new Option { Text = "Berlin", IsCorrect = true },
                new Option { Text = "Paris", IsCorrect = false },
                new Option { Text = "Rome", IsCorrect = false }
            }
        }
    };

    context.Questions.AddRange(questions);
    context.SaveChanges();
}



