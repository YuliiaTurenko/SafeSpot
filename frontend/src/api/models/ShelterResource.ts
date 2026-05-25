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

export const resourceTypeLabels: Record<ResourceType, string> = {
  [ResourceType.Water]: "resources.types.water",
  [ResourceType.Electricity]: "resources.types.electricity",
  [ResourceType.Wifi]: "resources.types.wifi",
  [ResourceType.Generator]: "resources.types.generator",
  [ResourceType.Heating]: "resources.types.heating",
  [ResourceType.MedicalKit]: "resources.types.medicalKit",
  [ResourceType.Food]: "resources.types.food",
  [ResourceType.Toilet]: "resources.types.toilet",
  [ResourceType.Ventilation]: "resources.types.ventilation",
};

export const resourceStatusLabels: Record<ResourceStatus, string> = {
  [ResourceStatus.Unknown]: "resources.statuses.unknown",
  [ResourceStatus.Available]: "resources.statuses.available",
  [ResourceStatus.Limited]: "resources.statuses.limited",
  [ResourceStatus.Unavailable]: "resources.statuses.unavailable",
};

export type ShelterResourceDto = {
  id: number;
  type: ResourceType;
  status: ResourceStatus;
  amount: number;
};

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
