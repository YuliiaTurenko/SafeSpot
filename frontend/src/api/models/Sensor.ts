export enum SensorStatus {
  Active = 0,
  Inactive = 1,
  Error = 2,
  Maintenance = 3,
  Offline = 4,
}

export enum SensorType {
  Occupancy = 0,
  Temperature = 1,
  Humidity = 2,
  CO2 = 3,
  AirQuality = 4,
}

export const sensorStatusLabels: Record<SensorStatus, string> = {
  [SensorStatus.Active]: "sensors.statuses.active",
  [SensorStatus.Inactive]: "sensors.statuses.inactive",
  [SensorStatus.Error]: "error",
  [SensorStatus.Maintenance]: "shelters.statuses.maintenance",
  [SensorStatus.Offline]: "sensors.statuses.offline",
};

export const sensorTypeLabels: Record<SensorType, string> = {
  [SensorType.Occupancy]: "sensors.types.occupancy",
  [SensorType.Temperature]: "sensors.types.temperature",
  [SensorType.Humidity]: "sensors.types.humidity",
  [SensorType.CO2]: "sensors.types.co2Level",
  [SensorType.AirQuality]: "sensors.types.airQuality",
};

export type SensorDto = {
  id: number;
  type: SensorType;
  status: SensorStatus;
  minValue: number;
  maxValue: number;
};