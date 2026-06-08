import { api } from "./axios";

export const sendAdminRequest = (data: { message: string }) =>
  api.post("/admin/request", data);

export const getUsers = () => api.get("/admin/users");

export const getOperators = () => api.get("/admin/moderators");

export const assignOperator = (data: { targetUserId: number; shelterId: number }) =>
  api.post("/admin/moderators/assign", data);

export const revokeOperator = (data: { targetUserId: number }) =>
  api.post("/admin/moderators/revoke", data);