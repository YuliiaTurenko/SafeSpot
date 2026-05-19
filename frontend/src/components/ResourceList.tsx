import { useEffect, useState } from "react";
import {
  createShelterResource,
  deleteShelterResource,
  getResourcesByShelterId,
  updateShelterResource,
} from "../api/shelterResourceApi";
import {
  ResourceStatus,
  ResourceType,
} from "../api/models/ShelterResource";
import { useTranslation } from "react-i18next";

interface Props {
  shelterId: number;
}

export default function ResourceList({ shelterId }: Props) {
  const [resources, setResources] = useState<any[]>([]);
  const [editingId, setEditingId] = useState<number | null>(null);
  const [type, setType] = useState<ResourceType>(ResourceType.Water);
  const [status, setStatus] = useState<ResourceStatus>(
    ResourceStatus.Available,
  );
  const [amount, setAmount] = useState<number>(0);
  const { t } = useTranslation();

  const load = async () => {
    const res = await getResourcesByShelterId(shelterId);

    setResources(res.data);
  };

  useEffect(() => {
    load();
  }, [shelterId]);

  const clearForm = () => {
    setEditingId(null);
    setType(ResourceType.Water);
    setStatus(ResourceStatus.Available);
    setAmount(0);
  };

  const handleSubmit = async () => {
    if (editingId) {
      await updateShelterResource({
        id: editingId,
        status,
        amount,
      });
    } else {
      await createShelterResource({
        shelterId,
        type,
        status,
        amount,
      });
    }

    clearForm();
    load();
  };

  const handleEdit = (r: any) => {
    setEditingId(r.id);
    setType(r.type);
    setStatus(r.status);
    setAmount(r.amount);
  };

  const handleDelete = async (id: number) => {
    await deleteShelterResource(id);

    load();
  };

  return (
    <div className="mt-10">
      <h2 className="text-2xl font-bold mb-5">{t("resourcesTitle")}</h2>

      <div className="bg-[#2F3E46] p-5 rounded-2xl mb-5">
        <div className="grid grid-cols-3 gap-3">
          <select
            value={type}
            onChange={(e) => setType(Number(e.target.value))}
            className="p-3 rounded bg-[#354F52]"
          >
            {Object.entries(ResourceType)
              .filter(([k]) => isNaN(Number(k)))
              .map(([key, value]) => (
                <option key={value} value={value}>
                  {key}
                </option>
              ))}
          </select>

          <select
            value={status}
            onChange={(e) => setStatus(Number(e.target.value))}
            className="p-3 rounded bg-[#354F52]"
          >
            {Object.entries(ResourceStatus)
              .filter(([k]) => isNaN(Number(k)))
              .map(([key, value]) => (
                <option key={value} value={value}>
                  {key}
                </option>
              ))}
          </select>

          <input
            type="number"
            value={amount}
            onChange={(e) => setAmount(Number(e.target.value))}
            placeholder={t("amount")}
            className="p-3 rounded bg-[#354F52]"
          />
        </div>

        <div className="flex gap-3 mt-4">
          <button
            onClick={handleSubmit}
            className="bg-[#84A98C] px-4 py-2 rounded"
          >
            {editingId ? t("save") : t("create")}
          </button>

          {editingId && (
            <button
              onClick={clearForm}
              className="bg-gray-500 px-4 py-2 rounded"
            >
              {t("cancel")}
            </button>
          )}
        </div>
      </div>

      <div className="space-y-4">
        {resources.map((r) => (
          <div
            key={r.id}
            className="bg-[#2F3E46] p-4 rounded-xl flex justify-between"
          >
            <div>
              <p>
                {t("type")}: {ResourceType[r.type]}
              </p>
              <p>
                {t("status")}: {ResourceStatus[r.status]}
              </p>
              <p>
                {t("amount")}: {r.amount}
              </p>
            </div>

            <div className="flex gap-3">
              <button
                onClick={() => handleEdit(r)}
                className="bg-[#52796F] px-3 py-2 rounded"
              >
                {t("edit")}
              </button>

              <button
                onClick={() => handleDelete(r.id)}
                className="bg-red-500 px-3 py-2 rounded"
              >
                {t("delete")}
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
