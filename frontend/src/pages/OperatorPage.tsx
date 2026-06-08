import { useState } from "react";
import { useNavigate } from "react-router-dom";
import OperatorShelterList from "../components/OperatorShelterList";
import AnnouncementList from "../components/AnnouncementList";
import ResourceList from "../components/ResourceList";
import NotificationBadge from "../components/NotificationBadge";
import LanguageButton from "../components/LanguageButton";
import NotificationToast from "../components/NotificationToast";
import { useTranslation } from "react-i18next";

export default function OperatorPage() {
  const [selectedShelterId, setSelectedShelterId] = useState<number | null>(
    null,
  );
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <div className="flex min-h-screen bg-[#354F52] text-white">
      <div className="flex-1 p-6 overflow-y-auto">
        <div className="flex justify-between items-center mb-6">
          <h1 className="text-3xl font-bold">{t("operatorDashboard")}</h1>

          <div className="flex items-center gap-4">
            <div className="relative inline-block">
              <button
                onClick={() => navigate("/notification")}
                className="bg-[#84A98C] hover:bg-[#6B9080] text-white px-4 py-2 rounded-lg font-medium transition-all"
              >
                {t("notifications")}
              </button>

              <NotificationBadge />
            </div>

            <div className="ml-1">
              <LanguageButton />
            </div>
          </div>
        </div>

        <OperatorShelterList
          onSelectShelter={(id) => setSelectedShelterId(id)}
        />

        {selectedShelterId && (
          <>
            <NotificationToast shelterId={selectedShelterId} />
            
            <ResourceList shelterId={selectedShelterId} />

            <AnnouncementList shelterId={selectedShelterId} />
          </>
        )}
      </div>
    </div>
  );
}
