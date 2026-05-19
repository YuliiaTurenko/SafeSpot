import { useState } from "react";
import ShelterList from "../components/ShelterList";
import AnnouncementList from "../components/AnnouncementList";
import ResourceList from "../components/ResourceList";
import LanguageButton from "../components/LanguageButton";
import { useTranslation } from "react-i18next";

export default function AdminPage() {
  const [selectedShelterId, setSelectedShelterId] = useState<number | null>(null);
  const { t } = useTranslation();

  return (
    <div className="flex min-h-screen bg-[#354F52] text-white">
      <div className="flex-1 p-6 overflow-y-auto">
        <h1 className="text-3xl font-bold mb-6">
          {t("dashboard")}
        </h1>

        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>

        <ShelterList
          onSelectShelter={(id) =>
            setSelectedShelterId(id)
          }
        />

        {selectedShelterId && (
          <>
            <AnnouncementList
              shelterId={selectedShelterId}
            />

            <ResourceList
              shelterId={selectedShelterId}
            />
          </>
        )}
      </div>
    </div>
  );
}