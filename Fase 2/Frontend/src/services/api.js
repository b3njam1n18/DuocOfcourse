import axios from "axios";

const api = axios.create({
  baseURL: "https://localhost:7037/api", 
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export const login = (email, password) => {
  return api.post("/Auth/login", { email, password });
};


export default api;