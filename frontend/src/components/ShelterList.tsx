import { useEffect, useState } from "react";
import { deleteShelter, getShelters } from "../api/shelterApi";
import ShelterModal from "./ShelterModal";
import { useTranslation } from "react-i18next";

interface Props {
  onSelectShelter: (id: number) => void;
}

export default function ShelterList({ onSelectShelter }: Props) {
  const { t } = useTranslation();
  const [shelters, setShelters] = useState<any[]>([]);
  const [open, setOpen] = useState(false);

  const load = async () => {
    const res = await getShelters();
    setShelters(res.data);
  };

  useEffect(() => {
    load();
  }, []);

  const handleDelete = async (id: number) => {
    await deleteShelter(id);
    load();
  };

  return (
    <div>
      <div className="flex justify-between mb-5">
        <h2 className="text-2xl font-semibold">{t("shelters")}</h2>

        <button
          onClick={() => setOpen(true)}
          className="bg-[#84A98C] px-4 py-2 rounded"
        >
          {t("addShelter")}
        </button>
      </div>

      <div className="space-y-4">
        {shelters.map((s) => (
          <div key={s.id} className="bg-[#2F3E46] p-4 rounded-xl">
            <div className="flex justify-between items-center">
              <div>
                <h3 className="text-xl">{s.name}</h3>
                <p>{s.address}</p>
                <p>
                  {t("capacity")}: {s.capacity}
                </p>
              </div>

              <div className="flex gap-2">
                <button
                  onClick={() => onSelectShelter(s.id)}
                  className="bg-[#354F52] hover:bg-[#52796F] text-white px-3 py-2 rounded transition-colors"
                >
                  {t("select") || "Вибрати"}
                </button>

                <button
                  onClick={() => handleDelete(s.id)}
                  className="bg-red-500 hover:bg-red-600 px-3 py-2 rounded transition-colors"
                >
                  {t("delete")}
                </button>
              </div>
            </div>
          </div>
        ))}
      </div>

      <ShelterModal
        open={open}
        onClose={() => setOpen(false)}
        onCreated={load}
      />
    </div>
  );
}
