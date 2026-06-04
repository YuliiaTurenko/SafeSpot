import { useEffect, useState } from "react";
import { sensorHub } from "../services/signalr/sensorHub";

type NotificationItem = {
  title: string;
  message: string;
};

interface Props {
  shelterId?: number;
}

export default function NotificationToast({ shelterId }: Props) {
  const [items, setItems] = useState<NotificationItem[]>([]);

  useEffect(() => {
    if (shelterId) {
      sensorHub.start(shelterId);
    }

    const handleNotification = (data: any) => {
      setItems((prev) => [...prev, data]);

      setTimeout(() => {
        setItems((prev) => prev.slice(1));
      }, 5000);
    };

    sensorHub.onNotification(handleNotification);

    return () => {
      if (shelterId) {
        sensorHub.leaveShelter(shelterId);
      }
    };
  }, [shelterId]);

  return (
    <div className="fixed top-4 right-4 z-50 flex flex-col gap-3">
      {items.map((n, i) => (
        <div
          key={i}
          className="bg-[#354F52] text-white px-4 py-3 rounded shadow-lg w-[300px] border border-[#84A98C]"
        >
          <h3 className="font-semibold">{n.title}</h3>
          <p className="text-sm">{n.message}</p>
        </div>
      ))}
    </div>
  );
}
