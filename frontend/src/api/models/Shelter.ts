export enum ShelterStatus {
  Available = 0,
  Closed = 1,
  Full = 2,
  Maintenance = 3,
}

export type ShelterDto = {
  id: number;
  address: string;
  latitude: number;
  longitude: number;
  capacity: number;
  status: ShelterStatus;
  description?: string | null;
  imageUrl?: string | null;
};

export type ShelterPreviewDto = {
  id: number;
  address: string;
  latitude: number;
  longitude: number;
  capacity: number;
  status: ShelterStatus;
};

export type CreateShelterRequest = {
  address: string;
  latitude: number;
  longitude: number;
  capacity: number;
  status: ShelterStatus;
  description?: string | null;
  imageUrl?: string | null;
};

export type UpdateShelterRequest = {
  id: number;
  address: string;
  latitude: number;
  longitude: number;
  capacity: number;
  status: ShelterStatus;
  description?: string | null;
  imageUrl?: string | null;
};