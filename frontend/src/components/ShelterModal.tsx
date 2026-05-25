import { useEffect, useState } from "react";
import { createShelter, updateShelter } from "../api/shelterApi";
import { ShelterStatus } from "../api/models/Shelter";
import { useTranslation } from "react-i18next";

interface Props {
  open: boolean;
  onClose: () => void;
  onCreated: () => void;
  shelter?: any;
}

export default function ShelterModal({
  open,
  onClose,
  onCreated,
  shelter,
}: Props) {
  const [address, setAddress] = useState("");
  const [capacity, setCapacity] = useState(0);
  const [latitude, setLatitude] = useState(0);
  const [longitude, setLongitude] = useState(0);
  const [status, setStatus] = useState<ShelterStatus>(ShelterStatus.Available);
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");
  const { t } = useTranslation();

  useEffect(() => {
    if (shelter) {
      setAddress(shelter.address);
      setCapacity(shelter.capacity);
      setLatitude(shelter.latitude);
      setLongitude(shelter.longitude);
      setStatus(shelter.status);
      setDescription(shelter.description || "");
      setImageUrl(shelter.imageUrl || "");
    }
  }, [shelter]);

  if (!open) return null;

  const handleImageUpload = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];

    if (!file) return;

    const localUrl = URL.createObjectURL(file);

    setImageUrl(localUrl);
  };

  const handleSubmit = async () => {
    const data = {
      address,
      latitude: Number(latitude),
      longitude: Number(longitude),
      capacity: Number(capacity),
      status,
      description,
      imageUrl,
    };

    if (shelter) {
      await updateShelter({
        id: shelter.id,
        ...data,
      });
    } else {
      await createShelter(data);
    }

    onCreated();
    onClose();
  };

  return (
    <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50">
      <div className="bg-[#2F3E46] p-6 rounded-2xl w-[550px] max-h-[90vh] overflow-y-auto">
        <h2 className="text-2xl mb-5">
          {shelter ? t("editShelter") : t("createShelter")}
        </h2>

        <div className="space-y-4">
          <label className="block mb-2 text-sm text-gray-300">
            {t("address")}
          </label>
          <input
            value={address}
            onChange={(e) => setAddress(e.target.value)}
            className="w-full p-3 rounded bg-[#354F52]"
          />
          <label className="block mb-2 text-sm text-gray-300">
            {t("description")}
          </label>
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            className="w-full p-3 rounded bg-[#354F52]"
          />
          <label className="block mb-2 text-sm text-gray-300">
            {t("capacity")}
          </label>
          <input
            type="number"
            value={capacity}
            onChange={(e) => setCapacity(Number(e.target.value))}
            className="w-full p-3 rounded bg-[#354F52]"
          />

          <div className="grid grid-cols-2 gap-3">
            <label className="block mb-2 text-sm text-gray-300">
              {t("latitude")}
            </label>
            <input
              type="number"
              step="any"
              value={latitude}
              onChange={(e) => setLatitude(Number(e.target.value))}
              className="p-3 rounded bg-[#354F52]"
            />
            <label className="block mb-2 text-sm text-gray-300">
              {t("longitude")}
            </label>
            <input
              type="number"
              step="any"
              value={longitude}
              onChange={(e) => setLongitude(Number(e.target.value))}
              className="p-3 rounded bg-[#354F52]"
            />
          </div>
          <label className="block mb-2 text-sm text-gray-300">
            {t("status")}
          </label>
          <select
            value={status}
            onChange={(e) => setStatus(Number(e.target.value))}
            className="w-full p-3 rounded bg-[#354F52]"
          >
            {Object.entries(ShelterStatus)
              .filter(([k]) => isNaN(Number(k)))
              .map(([key, value]) => (
                <option key={value} value={value}>
                  {key}
                </option>
              ))}
          </select>

          <div>
            <label className="block mb-2 text-sm text-gray-300">
              Shelter image
            </label>

            <input
              type="file"
              accept="image/*"
              onChange={handleImageUpload}
              className="w-full"
            />
          </div>

          {imageUrl && (
            <img
              src={imageUrl}
              alt="Shelter"
              className="rounded-xl max-h-[250px]"
            />
          )}
        </div>

        <div className="flex gap-3 mt-5">
          <button
            onClick={handleSubmit}
            className="bg-[#84A98C] px-4 py-2 rounded"
          >
            {shelter ? t("save") : t("create")}
          </button>

          <button onClick={onClose} className="bg-gray-500 px-4 py-2 rounded">
            {t("cancel")}
          </button>
        </div>
      </div>
    </div>
  );
}
