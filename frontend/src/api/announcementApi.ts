import { api } from "./axios";
import {
  CreateAnnouncementRequest,
  UpdateAnnouncementRequest,
} from "./models/Announcement";

export const getAnnouncementsByShelterId = (
  shelterId: number
) => api.get(`/announcements?shelterId=${shelterId}`);

export const createAnnouncement = (data: CreateAnnouncementRequest) =>
  api.post("/announcements", data);

export const updateAnnouncement = (data: UpdateAnnouncementRequest) =>
  api.put("/announcements", data);

export const deleteAnnouncement = (id: number) =>
  api.delete(`/announcements/${id}`);