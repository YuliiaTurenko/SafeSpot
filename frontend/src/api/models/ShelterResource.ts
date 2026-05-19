export enum ResourceType {
  Water = 0,
  Electricity = 1,
  Wifi = 2,
  Generator = 3,
  Heating = 4,
  MedicalKit = 5,
  Food = 6,
  Toilet = 7,
  Ventilation = 8,
}

export enum ResourceStatus {
  Unknown = 0,
  Available = 1,
  Limited = 2,
  Unavailable = 3,
}

export type CreateResourceRequest = {
  shelterId: number;
  type: ResourceType;
  status: ResourceStatus;
  amount: number;
}

export type UpdateResourceRequest = {
  id: number;
  status: ResourceStatus;
  amount: number;
}
