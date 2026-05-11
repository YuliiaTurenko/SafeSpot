import { api } from "./axios";
import {
  CreateResourceRequest,
  UpdateResourceRequest,
} from "./models/requests/ShelterResourceRequests";

export const getShelterResources = (shelterId: number) =>
  api.get(`/shelter-resources/${shelterId}`);

export const createShelterResource = (data: CreateResourceRequest) =>
  api.post("/shelter-resources", data);

export const updateShelterResource = (data: UpdateResourceRequest) =>
  api.put("/shelters", data);

export const deleteShelterResource = (id: number) =>
  api.delete(`/shelters/${id}`);
