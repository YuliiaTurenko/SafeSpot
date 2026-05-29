import { api } from "./axios";
import {
  CreateSensorRequest,
  UpdateSensorRequest,
} from "./models/Sensor";

export const getSensorsByShelterId = (shelterId: number) =>
  api.get(`/sensors?shelterId=${shelterId}`);

export const createSensor = (data: CreateSensorRequest) =>
  api.post("/sensors", data);

export const updateSensor = (data: UpdateSensorRequest) =>
  api.put("/sensors", data);

export const deleteSensor = (id: number) =>
  api.delete(`/sensors/${id}`);

export const getSensorReadings = (sensorId: number) =>
  api.get(`/sensor-readings/${sensorId}`);

export const getShelterAnalytics = (shelterId: number) =>
  api.get(`/sensor-analytics?shelterId=${shelterId}`);

export const disableSensor = (id: number) =>
  api.post(`/sensors/${id}/disable`);

export const enableSensor = (id: number) =>
  api.post(`/sensors/${id}/enable`);