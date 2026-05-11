import { api } from "./axios";
import {
  CreateAnnouncementRequest,
  UpdateAnnouncementRequest,
} from "./models/requests/AnnouncementRequests";

export const getAnnouncements = () =>
  api.get("/announcements");

export const createAnnouncement = (data: CreateAnnouncementRequest) =>
  api.post("/announcements", data);

export const updateAnnouncement = (data: UpdateAnnouncementRequest) =>
  api.put("/announcements", data);

export const deleteAnnouncement = (id: number) =>
  api.delete(`/announcements/${id}`);