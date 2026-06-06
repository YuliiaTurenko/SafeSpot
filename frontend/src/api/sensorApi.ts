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

export const getLatestSensorReadings = (sensorId: number) =>
  api.get(`/sensor-readings/${sensorId}`);

export const getWeeklySensorReadings = (sensorId: number) =>
  api.get(`/sensor-readings/${sensorId}/weekly`);

export const getMontlySensorReadings = (sensorId: number) =>
  api.get(`/sensor-readings/${sensorId}/monthly`);

export const getShelterAnalytics = (shelterId: number) =>
  api.get(`/sensor-analytics?shelterId=${shelterId}`);

export const disableSensor = (id: number) =>
  api.post(`/sensors/${id}/disable`);

export const enableSensor = (id: number) =>
  api.post(`/sensors/${id}/enable`);