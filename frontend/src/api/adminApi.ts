import { api } from "./axios";

export const sendAdminRequest = (data: { message: string }) =>
  api.post("/admin/request", data);