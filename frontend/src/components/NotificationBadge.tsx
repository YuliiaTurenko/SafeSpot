import { useEffect, useState } from "react";
import { getUnreadCount } from "../api/notificationApi";

export default function NotificationBadge() {
  const [count, setCount] = useState(0);

  const load = async () => {
    try {
      const res = await getUnreadCount();
      setCount(res.data);
    } catch {
      setCount(0);
    }
  };

  useEffect(() => {
    load();

    const interval = setInterval(load, 30000);

    return () => clearInterval(interval);
  }, []);

  if (count <= 0) return null;

  return (
    <span
      className="
        absolute
        -top-2
        -right-2
        min-w-[20px]
        h-5
        px-1
        rounded-full
        bg-red-500
        text-white
        text-xs
        flex
        items-center
        justify-center
      "
    >
      {count > 99 ? "99+" : count}
    </span>
  );
}
