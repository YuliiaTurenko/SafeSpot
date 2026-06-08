import { useEffect, useState } from "react";
import { deleteShelter, getShelters } from "../api/shelterApi";
import ShelterModal from "./ShelterModal";
import { useTranslation } from "react-i18next";
import { ShelterStatus, statusLabels } from "../api/models/Shelter";
import { useNavigate } from "react-router-dom";

interface Props {
  onSelectShelter: (id: number) => void;
}

export default function ShelterList({ onSelectShelter }: Props) {
  const { t } = useTranslation();
  const [shelters, setShelters] = useState<any[]>([]);
  const [open, setOpen] = useState(false);

  const [search, setSearch] = useState("");
  const [statusFilter, setStatusFilter] = useState<ShelterStatus | "all">(
    "all",
  );
  const [selectedShelterId, setSelectedShelterId] = useState<number | null>(
    null,
  );

  const navigate = useNavigate();

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

  const [editingShelter, setEditingShelter] = useState<any | null>(null);

  const filteredShelters = shelters.filter((shelter) => {
    const matchesSearch = shelter.address
      .toLowerCase()
      .includes(search.toLowerCase());

    const matchesStatus =
      statusFilter === "all" ? true : shelter.status === statusFilter;

    return matchesSearch && matchesStatus;
  });

  return (
    <div>
      <div className="flex justify-between mb-5">
        <h2 className="text-2xl font-semibold">{t("sheltersTitle")}</h2>

        <button
          onClick={() => setOpen(true)}
          className="bg-[#84A98C] px-4 py-2 rounded"
        >
          {t("addShelter")}
        </button>
      </div>

      <div className="flex flex-col md:flex-row gap-3 mb-6">
        <input
          type="text"
          placeholder={t("searchShelters")}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          className="bg-white text-black text-lg p-3 rounded-xl flex-1 border border-gray-200"
        />

        <select
          value={statusFilter}
          onChange={(e) =>
            setStatusFilter(
              e.target.value === "all" ? "all" : Number(e.target.value),
            )
          }
          className="bg-[#354F52] p-3 rounded-xl"
        >
          <option value="all">{t("allStatuses")}</option>

          {Object.entries(ShelterStatus)
            .filter(([k]) => isNaN(Number(k)))
            .map(([key, value]) => (
              <option key={value} value={value}>
                {key}
              </option>
            ))}
        </select>
      </div>

      <div className="space-y-4">
        {filteredShelters.map((s) => (
          <div
            key={s.id}
            className={`
              bg-[#2F3E46] p-4 rounded-xl border-2 transition-all
              ${
                selectedShelterId === s.id
                  ? "border-[#151A3C]"
                  : "border-transparent"
              }
            `}
          >
            <div className="flex justify-between items-center">
              <div>
                <h3>{s.address}</h3>
                <p>
                  {" "}
                  {t("capacity")}: {s.capacity}{" "}
                </p>
                <p>
                  {" "}
                  {t("status")}:{" "}
                  {t(statusLabels[s.status as ShelterStatus])}{" "}
                </p>
              </div>

              <div className="flex gap-2 flex-wrap">
                <button
                  onClick={() => {
                    setSelectedShelterId(s.id);
                    onSelectShelter(s.id);
                  }}
                  className="bg-[#354F52] hover:bg-[#52796F] text-white px-3 py-2 rounded transition-colors"
                >
                  {t("select")}
                </button>

                <button
                  onClick={() => navigate(`/shelters/${s.id}/posts`)}
                  className="bg-[#354F52] hover:bg-[#52796F] text-white px-3 py-2 rounded transition-colors"
                >
                  {t("posts")}
                </button>

                <button
                  onClick={() => navigate(`/admin/shelter/${s.id}/sensors`)}
                  className="bg-[#354F52] hover:bg-[#52796F] text-white px-3 py-2 rounded transition-colors"
                >
                  {t("sensorsTitle")}
                </button>

                <button
                  onClick={() => setEditingShelter(s)}
                  className="bg-[#678ABE] hover:bg-[#5C858D] text-white px-3 py-2 rounded transition-colors"
                >
                  {t("edit") || "Редагувати"}
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
      {editingShelter && (
        <ShelterModal
          open={true}
          shelter={editingShelter}
          onClose={() => setEditingShelter(null)}
          onCreated={load}
        />
      )}

      <ShelterModal
        open={open}
        onClose={() => setOpen(false)}
        onCreated={load}
      />
    </div>
  );
}
