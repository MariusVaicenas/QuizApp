import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";
import {
  Stepper,
  Step,
  StepLabel,
  Button,
  TextField,
  FormControlLabel,
  Radio,
  RadioGroup,
  Checkbox,
  Typography,
  Box,
} from "@mui/material";

const QuizPage = () => {
  const [questions, setQuestions] = useState([]);
  const [answers, setAnswers] = useState({});
  const [email, setEmail] = useState("");
  const [currentStep, setCurrentStep] = useState(0);
  const [error, setError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("http://localhost:5119/api/quiz/questions")
      .then((response) => setQuestions(response.data))
      .catch(() => setError("Unable to fetch questions. Please try again later."));
  }, []);

  const handleNext = () => setCurrentStep((prev) => prev + 1);
  const handleBack = () => setCurrentStep((prev) => prev - 1);

  const handleAnswerChange = (questionId, value) => {
    setAnswers((prevAnswers) => ({
      ...prevAnswers,
      [questionId]: value,
    }));
  };

  const handleSubmit = () => {
    if (!email) {
      setError("Please enter a valid email.");
      return;
    }

    // Ensure every question is included in the submission
    const submissionData = {
      email,
      answers: questions.map((question) => ({
        questionId: question.id,
        text: Array.isArray(answers[question.id]) // Handle checkbox type
          ? answers[question.id].join(", ")
          : answers[question.id] || "", 
      })),
    };

    axios
      .post("http://localhost:5119/api/quiz/submit", submissionData)
      .then(() => navigate("/highscores"))
      .catch(() => setError("Error submitting quiz. Please try again."));
  };

  const currentQuestion = questions[currentStep];

  return (
    <Box
      sx={{
        maxWidth: "600px",
        margin: "50px auto",
        padding: "20px",
        borderRadius: "8px",
        boxShadow: "0 4px 10px rgba(0, 0, 0, 0.1)",
        backgroundColor: "#fff",
      }}
    >
      <Typography variant="h5" textAlign="center" mb={3}>
        Quiz
      </Typography>
      <TextField
        type="email"
        placeholder="Email" 
        value={email}
        onChange={(e) => setEmail(e.target.value)}
        fullWidth
        margin="normal"
        variant="outlined"
      />
      {error && <Typography color="error">{error}</Typography>}

      {/* Scrollable Stepper */}
      <Box
        sx={{
          overflowX: "auto",
          whiteSpace: "nowrap",
          marginBottom: "20px",
        }}
      >
        <Stepper
          activeStep={currentStep}
          alternativeLabel
          sx={{
            minWidth: "600px", 
          }}
        >
          {questions.map((_, index) => (
            <Step key={index}>
              <StepLabel>Question {index + 1}</StepLabel>
            </Step>
          ))}
        </Stepper>
      </Box>

      {currentQuestion && (
        <div>
          <Typography variant="h6" mb={2}>
            {currentQuestion.text}
          </Typography>
          {currentQuestion.type === "Radio" && (
            <RadioGroup
              value={answers[currentQuestion.id] || ""}
              onChange={(e) => handleAnswerChange(currentQuestion.id, e.target.value)}
            >
              {currentQuestion.options.map((option) => (
                <FormControlLabel
                  key={option.id}
                  value={option.text}
                  control={<Radio />}
                  label={option.text}
                />
              ))}
            </RadioGroup>
          )}

          {currentQuestion.type === "Checkbox" && (
            <div>
              {currentQuestion.options.map((option) => (
                <FormControlLabel
                  key={option.id}
                  control={
                    <Checkbox
                      checked={(answers[currentQuestion.id] || []).includes(option.text)}
                      onChange={(e) => {
                        const currentAnswers = answers[currentQuestion.id] || [];
                        if (e.target.checked) {
                          handleAnswerChange(currentQuestion.id, [...currentAnswers, option.text]);
                        } else {
                          handleAnswerChange(
                            currentQuestion.id,
                            currentAnswers.filter((answer) => answer !== option.text)
                          );
                        }
                      }}
                    />
                  }
                  label={option.text}
                />
              ))}
            </div>
          )}

          {currentQuestion.type === "Text" && (
            <TextField
              value={answers[currentQuestion.id] || ""}
              onChange={(e) => handleAnswerChange(currentQuestion.id, e.target.value)}
              fullWidth
              margin="normal"
              variant="outlined"
            />
          )}
        </div>
      )}

      <Box sx={{ display: "flex", justifyContent: "space-between", mt: 4 }}>
        <Button
          variant="contained"
          color="secondary"
          onClick={handleBack}
          disabled={currentStep === 0}
        >
          Back
        </Button>
        {currentStep === questions.length - 1 ? (
          <Button variant="contained" color="primary" onClick={handleSubmit}>
            Submit
          </Button>
        ) : (
          <Button variant="contained" color="primary" onClick={handleNext}>
            Next
          </Button>
        )}
      </Box>

      <Box sx={{ textAlign: "center", mt: 3 }}>
        <Button variant="text" onClick={() => navigate("/highscores")}>
          View High Scores
        </Button>
      </Box>
    </Box>
  );
};

export default QuizPage;
