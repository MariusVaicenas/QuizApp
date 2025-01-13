import React, { useState, useEffect } from "react";
import axios from "axios";
import { Button, Typography, Box, Card, CardContent } from "@mui/material";
import { useNavigate } from "react-router-dom";

const HighScoresPage = () => {
  const [highScores, setHighScores] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    axios
      .get("http://localhost:5119/api/quiz/highscores")
      .then((response) => setHighScores(response.data))
      .catch(() => console.error("Error fetching high scores!"));
  }, []);

  const getRankStyle = (rank) => {
    switch (rank) {
      case 1:
        return { color: "gold", fontWeight: "bold" };
      case 2:
        return { color: "silver", fontWeight: "bold" };
      case 3:
        return { color: "#CD7F32", fontWeight: "bold" };
      default:
        return { color: "black" };
    }
  };

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
      <Typography variant="h4" textAlign="center" mb={3}>
        High Scores
      </Typography>
      <Box>
        {highScores.map((score, index) => (
          <Card
            key={index}
            sx={{
              marginBottom: "10px",
              backgroundColor: index < 3 ? "#f9f9f9" : "#fff",
            }}
          >
            <CardContent>
              <Typography
                style={getRankStyle(index + 1)}
              >{`${index + 1}. ${score.email} - ${score.score} points`}</Typography>
              <Typography variant="body2" color="textSecondary">
                {new Date(score.dateTime).toLocaleString()}
              </Typography>
            </CardContent>
          </Card>
        ))}
      </Box>
      <Box textAlign="center" mt={3}>
        <Button variant="contained" color="primary" onClick={() => navigate("/")}>
          Back to Quiz
        </Button>
      </Box>
    </Box>
  );
};

export default HighScoresPage;
