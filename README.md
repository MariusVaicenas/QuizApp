
# Quiz App

A simple quiz application built with **ASP.NET Core** for the backend and **React** for the frontend. The app supports single-choice, multiple-choice, and text-based questions, and displays high scores with styling for the top 3 positions.

---

## Features

### Quiz Page
- Users can solve a quiz with **10 questions** of varying types:
  - **Radio Buttons**: Single-answer questions.
  - **Checkboxes**: Multiple-answer questions.
  - **Text Input**: Manual input questions.
- Email input is required to save the user's score.
- Unanswered questions are processed with a default score of **0 points**.

### High Scores Page
- Displays the top **10 high scores**, sorted by score (descending) and timestamp.
- Highlights the top 3 positions with **gold**, **silver**, and **bronze** colors.

---

## Technologies Used

### Backend
- **ASP.NET Core 7**: Handles API requests and business logic.
- **Entity Framework Core (In-Memory Database)**: Used to store quiz questions, options, and high scores.

### Frontend
- **React**: Handles user interactions and UI rendering.
- **Material-UI**: Provides a modern and responsive design.

---

## Installation and Setup

### Prerequisites
- **Node.js**: Required for the frontend.
- **.NET SDK**: Version 7.0 or higher.

### Steps to Run the App

1. **Backend Setup**

   cd backend/QuizAppBackend
   dotnet clean
   dotnet restore
   dotnet build
   dotnet run

   - The backend will start on `http://localhost:5119`.

2. **Frontend Setup**

   cd frontend/quiz-app-frontend
   npm install
   npm start

   - The frontend will start on `http://localhost:3000`.

---

## API Endpoints

### Questions API
- **GET /api/quiz/questions**
  - Fetches all questions and their options.

### Submit Quiz API
- **POST /api/quiz/submit**
  - Submits the user's answers and calculates their score.
  - Example Request:
    ```json
    {
      "email": "user@example.com",
      "answers": [
        { "questionId": 1, "text": "Paris" },
        { "questionId": 2, "text": "Python, Java" }
      ]
    }
    ```

### High Scores API
- **GET /api/quiz/highscores**
  - Fetches the top 10 high scores.

---

## Testing

The backend includes unit tests to verify functionality.

### Running Tests
1. Navigate to the test project:

   cd backend/QuizAppBackend.Tests

2. Run the tests:

   dotnet test


---


