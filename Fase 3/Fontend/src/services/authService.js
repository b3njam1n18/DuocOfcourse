import api from "./api";

export const registerUser = (payload) =>
  api.post("/Auth/register", payload);