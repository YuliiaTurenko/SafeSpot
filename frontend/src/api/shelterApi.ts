import { api } from "./axios";
import {
  CreateShelterRequest,
  UpdateShelterRequest,
} from "./models/Shelter";

export const getShelters = () =>
  api.get("/shelters");

export const getSheltersByUserId = (userId: number) =>
  api.get(`/shelters/user-${userId}`);

export const getShelterById = (id: number) =>
  api.get(`/shelters/shelter-details?id=${id}`);

export const createShelter = (data: CreateShelterRequest) =>
  api.post("/shelters", data);

export const updateShelter = (data: UpdateShelterRequest) =>
  api.put("/shelters", data);

export const deleteShelter = (id: number) =>
  api.delete(`/shelters/${id}`);