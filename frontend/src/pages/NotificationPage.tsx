import { useEffect, useState } from "react";
import { getNotifications, markNotificationAsRead } from "../api/notificationApi";
import { useTranslation } from "react-i18next";

export default function NotificationPage() {
  const [notifications, setNotifications] = useState<any[]>([]);
  const { t } = useTranslation();

  const load = async () => {
    const res = await getNotifications();

    setNotifications(res.data);
  };

  useEffect(() => {
    load();
  }, []);

  const handleRead = async (id: number) => {
    await markNotificationAsRead(id);

    setNotifications((prev) =>
      prev.map((n) => (n.id === id ? { ...n, isRead: true } : n)),
    );
  };

  return (
    <div className="max-w-5xl mx-auto p-6 text-white">
      <h1 className="text-4xl font-bold mb-8">{t("notifications")}</h1>

      <div className="space-y-4">
        {notifications.map((n) => (
          <div
            key={n.id}
            className={`rounded-2xl p-5 border ${
              n.isRead
                ? "bg-[#1F2937] border-gray-700"
                : "bg-[#354F52] border-[#84A98C]"
            }`}
          >
            <div className="flex justify-between items-start">
              <div>
                <h2 className="text-xl font-semibold">{n.title}</h2>

                <p className="mt-2 text-gray-300">{n.message}</p>

                <p className="text-sm text-gray-500 mt-3">
                  {new Date(n.sentAt).toLocaleString()}
                </p>
              </div>

              {!n.isRead && (
                <button
                  onClick={() => handleRead(n.id)}
                  className="bg-[#52796F] px-3 py-2 rounded"
                >
                  {t("markRead")}
                </button>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
