import { api } from "./axios";

export const getShelters = () =>
  api.get("/shelters");

export const createShelter = (data: any) =>
  api.post("/shelters", data);

export const updateShelter = (data: any) =>
  api.put("/shelters", data);

export const deleteShelter = (id: number) =>
  api.delete(`/shelters/${id}`);