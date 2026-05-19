export enum ShelterStatus {
  Unknown = 0,
  Active = 1,
  Full = 2,
  Closed = 3,
}

export type Shelter = {
  id: number;
  address: string;
  latitude: number;
  longitude: number;
  capacity: number;
  status: ShelterStatus;
  description?: string | null;
  imageUrl?: string | null;
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