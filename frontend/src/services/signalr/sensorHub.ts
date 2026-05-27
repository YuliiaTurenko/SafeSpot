import * as signalR from "@microsoft/signalr";

const BASE_URL = "https://localhost:7165";

class SensorHubService {
  private connection: signalR.HubConnection | null = null;

  async start() {
    if (this.connection) return;

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${BASE_URL}/hubs/sensors`, {
        accessTokenFactory: () => localStorage.getItem("token") || "",
      })
      .withAutomaticReconnect()
      .build();

    await this.connection.start();
  }

  onSensorReading(callback: (data: any) => void) {
    this.connection?.on("ReceiveSensorReading", callback);
  }

  onNotification(callback: (data: any) => void) {
    this.connection?.on("ReceiveNotification", callback);
  }

  stop() {
    this.connection?.stop();
  }
}

export const sensorHub = new SensorHubService();
