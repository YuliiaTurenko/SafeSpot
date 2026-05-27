import { useEffect, useState } from "react";
import { sensorHub } from "../services/signalr/sensorHub";

type NotificationItem = {
  title: string;
  message: string;
};

export default function NotificationToast() {
  const [items, setItems] = useState<NotificationItem[]>([]);

  useEffect(() => {
    sensorHub.start();

    sensorHub.onNotification((data) => {
      setItems((prev) => [...prev, data]);

      setTimeout(() => {
        setItems((prev) => prev.slice(1));
      }, 5000);
    });
  }, []);

  return (
    <div className="fixed top-4 right-4 z-50 flex flex-col gap-3">
      {items.map((n, i) => (
        <div
          key={i}
          className="bg-[#354F52] text-white px-4 py-3 rounded shadow-lg w-[300px]"
        >
          <h3 className="font-semibold">{n.title}</h3>
          <p className="text-sm">{n.message}</p>
        </div>
      ))}
    </div>
  );
}
