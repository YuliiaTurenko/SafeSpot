import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ShelterStatus, statusLabels } from "../api/models/Shelter";
import LanguageButton from "../components/LanguageButton";
import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";
import { getSavedShelters, removeSavedShelter } from "../api/savedShelterApi";

export default function SavedSheltersPage() {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const [shelters, setShelters] = useState<any[]>([]);

  const load = async () => {
    const res = await getSavedShelters();

    setShelters(res.data);
  };

  useEffect(() => {
    load();
  }, []);

  const handleDelete = async (shelterId: number) => {
    await removeSavedShelter(shelterId);

    load();
  };

  return (
    <div className="min-h-screen bg-[#2F3E46] text-white mx-auto p-6 text-white">
      <div className="flex justify-between items-center mb-8">
        <button
          onClick={() => navigate(-1)}
          className="bg-[#52796F] hover:bg-[#2F3E46] text-white px-4 py-2 rounded-lg font-medium shadow-md transition-all mb-4"
        >
          ← {t("back")}
        </button>

        <h1 className="text-4xl font-bold">{t("savedShelters")}</h1>

        <LanguageButton />
      </div>

      {shelters.length === 0 && (
        <div className="bg-[#354F52] rounded-xl p-6">
          {t("noSavedShelters")}
        </div>
      )}

      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-5">
        {shelters.map((shelter) => (
          <div
            key={shelter.shelterId}
            className="bg-[#354F52] rounded-2xl overflow-hidden"
          >
            {shelter.imageUrl && (
              <img
                src={shelter.imageUrl}
                alt=""
                className="h-48 w-full object-cover"
              />
            )}

            <div className="p-5">
              <h3 className="font-semibold text-lg mb-3">{shelter.address}</h3>

              <div className="space-y-2 text-sm">
                <p>
                  {t("capacity")}: {shelter.capacity}
                </p>

                <p>
                  {t("status")}:{" "}
                  {t(statusLabels[shelter.status as ShelterStatus])}
                </p>
              </div>

              <div className="flex gap-2 mt-5">
                <Link
                  to={`/shelters/${shelter.shelterId}`}
                  className="flex-1 text-center bg-[#52796F] hover:bg-[#354F52] px-3 py-2 rounded"
                >
                  {t("details")}
                </Link>

                <button
                  onClick={() => handleDelete(shelter.shelterId)}
                  className="bg-red-500 hover:bg-red-600 px-3 py-2 rounded"
                >
                  {t("remove")}
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
