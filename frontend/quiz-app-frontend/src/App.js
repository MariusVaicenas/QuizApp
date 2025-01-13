import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import QuizPage from "./components/QuizPage";
import HighScoresPage from "./components/HighScoresPage";
import { Box, Typography } from "@mui/material";

function App() {
  return (
    <Router>
      <Box
        sx={{
          maxWidth: "800px",
          margin: "20px auto",
          padding: "20px",
          textAlign: "center",
          borderRadius: "8px",
          boxShadow: "0 4px 10px rgba(0, 0, 0, 0.1)",
          backgroundColor: "#fff",
        }}
      >
        <Typography variant="h4" component="h1" sx={{ mb: 4 }}>
          
        </Typography>
        <Routes>
          <Route path="/" element={<QuizPage />} />
          <Route path="/highscores" element={<HighScoresPage />} />
        </Routes>
      </Box>
    </Router>
  );
}

export default App;
