import { api } from "./axios";

export const sendAdminRequest = (data: { message: string }) =>
  api.post("/admin/request", data);

export const getUsers = () => api.get("/admin/users");

export const getModerators = () => api.get("/admin/moderators");

export const assignModerator = (data: { targetUserId: number; shelterId: number }) =>
  api.post("/admin/moderators/assign", data);

export const revokeModerator = (data: { targetUserId: number }) =>
  api.post("/admin/moderators/revoke", data);