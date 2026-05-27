import { useEffect, useState } from "react";
import { SensorStatus, SensorType } from "../api/models/Sensor";
import { createSensor, updateSensor } from "../api/sensorApi";
import { useTranslation } from "react-i18next";

type Props = {
  open: boolean;
  onClose: () => void;
  shelterId: number;
  onSuccess: () => void;
  sensor?: any;
};

export default function SensorModal({
  open,
  onClose,
  shelterId,
  onSuccess,
  sensor,
}: Props) {
  const isEdit = !!sensor;

  const [type, setType] = useState<SensorType>(SensorType.Temperature);
  const [status, setStatus] = useState<SensorStatus>(SensorStatus.Active);

  const [minValue, setMinValue] = useState(0);
  const [maxValue, setMaxValue] = useState(100);

  const { t } = useTranslation();

  useEffect(() => {
    if (!sensor) return;

    setType(sensor.type);
    setStatus(sensor.status);
    setMinValue(sensor.minValue);
    setMaxValue(sensor.maxValue);
  }, [sensor]);

  if (!open) return null;

  const handleSubmit = async () => {
    if (isEdit) {
      await updateSensor({
        id: sensor.id,
        minValue,
        maxValue,
        status,
      });
    } else {
      await createSensor({
        shelterId,
        type,
        minValue,
        maxValue,
      });
    }

    onSuccess();
    onClose();
  };

  return (
    <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50">
      <div className="bg-[#1F2937] w-[500px] rounded-2xl p-6 text-white">
        <h2 className="text-2xl font-semibold mb-6">
          {isEdit ? t("editSensor") : t("addSensor")}
        </h2>

        <div className="space-y-4">
          {!isEdit && (
            <div>
              <label className="block mb-2">
                {t("sensorType")}
              </label>

              <select
                value={type}
                onChange={(e) =>
                  setType(Number(e.target.value))
                }
                className="w-full bg-[#374151] p-3 rounded"
              >
                {Object.entries(SensorType)
                  .filter(([k]) => isNaN(Number(k)))
                  .map(([k, v]) => (
                    <option key={v} value={v}>
                      {k}
                    </option>
                  ))}
              </select>
            </div>
          )}

          <div>
            <label className="block mb-2">
              {t("minValue")}
            </label>

            <input
              type="number"
              value={minValue}
              onChange={(e) =>
                setMinValue(Number(e.target.value))
              }
              className="w-full bg-[#374151] p-3 rounded"
            />
          </div>

          <div>
            <label className="block mb-2">
              {t("maxValue")}
            </label>

            <input
              type="number"
              value={maxValue}
              onChange={(e) =>
                setMaxValue(Number(e.target.value))
              }
              className="w-full bg-[#374151] p-3 rounded"
            />
          </div>

          {isEdit && (
            <div>
              <label className="block mb-2">
                {t("status")}
              </label>

              <select
                value={status}
                onChange={(e) =>
                  setStatus(Number(e.target.value))
                }
                className="w-full bg-[#374151] p-3 rounded"
              >
                {Object.entries(SensorStatus)
                  .filter(([k]) => isNaN(Number(k)))
                  .map(([k, v]) => (
                    <option key={v} value={v}>
                      {k}
                    </option>
                  ))}
              </select>
            </div>
          )}

          <button
            onClick={handleSubmit}
            className="w-full bg-[#52796F] hover:bg-[#84A98C] py-3 rounded transition-colors"
          >
            {isEdit ? t("edit") : t("create")}
          </button>
        </div>
      </div>
    </div>
  );
}