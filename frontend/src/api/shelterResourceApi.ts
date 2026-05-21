import { api } from "./axios";
import {
  CreateResourceRequest,
  UpdateResourceRequest,
} from "./models/ShelterResource";

export const getResourcesByShelterId = (
  shelterId: number
) => api.get(`/shelter-resources?shelterId=${shelterId}`);

export const createShelterResource = (data: CreateResourceRequest) =>
  api.post("/shelter-resources", data);

export const updateShelterResource = (data: UpdateResourceRequest) =>
  api.put("/shelter-resources", data);

export const deleteShelterResource = (id: number) =>
  api.delete(`/shelter-resources/${id}`);
