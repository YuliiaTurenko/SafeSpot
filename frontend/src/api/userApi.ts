import { api } from "./axios";

export const createUser = (data: {
  firstName: string;
  lastName: string;
}) => api.post("/users/create", data);

export const getMe = () => api.get("/users/me");

export const updateUser = (data: {
  firstName: string;
  lastName: string;
}) => api.put("/users/update", data);