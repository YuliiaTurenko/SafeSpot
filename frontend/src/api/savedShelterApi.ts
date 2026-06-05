import { api } from "./axios";

export const saveShelter = (shelterId: number) =>
  api.post(`/saved-shelters/${shelterId}`);

export const removeSavedShelter = (shelterId: number) =>
  api.delete(`/saved-shelters/${shelterId}`);

export const isShelterSaved = (shelterId: number) =>
  api.get(`/saved-shelters/is-saved?shelterId=${shelterId}`);

export const getSavedShelters = () =>
  api.get("/saved-shelters");