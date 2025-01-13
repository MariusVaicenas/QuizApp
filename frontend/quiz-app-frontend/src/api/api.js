import axios from 'axios';

const API_URL = 'http://localhost:5119/api/quiz';

export const fetchQuestions = async () => {
    const response = await axios.get(`${API_URL}/questions`);
    return response.data;
};

export const submitQuiz = async (submission) => {
    const response = await axios.post(`${API_URL}/submit`, submission);
    return response.data;
};

export const fetchHighScores = async () => {
    const response = await axios.get(`${API_URL}/highscores`);
    return response.data;
};
