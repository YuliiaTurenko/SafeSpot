import { api } from "./axios";
import {
  CreateResourceRequest,
  UpdateResourceRequest,
} from "./models/requests/ShelterResourceRequests";

export const getResourcesByShelterId = (
  shelterId: number
) => api.get(`/shelters-resources?shelterId=${shelterId}`);

export const createShelterResource = (data: CreateResourceRequest) =>
  api.post("/shelter-resources", data);

export const updateShelterResource = (data: UpdateResourceRequest) =>
  api.put("/shelters", data);

export const deleteShelterResource = (id: number) =>
  api.delete(`/shelters/${id}`);
