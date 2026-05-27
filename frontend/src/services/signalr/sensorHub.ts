import * as signalR from "@microsoft/signalr";

const BASE_URL = "https://localhost:7165";

class SensorHubService {
  private connection: signalR.HubConnection | null = null;

  async start(shelterId?: number) {
    if (this.connection) {
      if (shelterId) {
        await this.connection.invoke("JoinShelter", shelterId);
      }

      return;
    }

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`${BASE_URL}/hubs/sensors`, {
        accessTokenFactory: () => localStorage.getItem("token") || "",
      })
      .withAutomaticReconnect()
      .build();

    await this.connection.start();

    if (shelterId) {
      await this.connection.invoke("JoinShelter", shelterId);
    }
  }

  async leaveShelter(shelterId: number) {
    if (!this.connection) return;

    await this.connection.invoke("LeaveShelter", shelterId);
  }

  onSensorReading(callback: (data: any) => void) {
    this.connection?.on("ReceiveSensorReading", callback);
  }

  onNotification(callback: (data: any) => void) {
    this.connection?.on("ReceiveNotification", callback);
  }

  stop() {
    this.connection?.stop();
    this.connection = null;
  }
}

export const sensorHub = new SensorHubService();
