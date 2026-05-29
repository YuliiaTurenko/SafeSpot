import {
  SensorStatus,
  SensorType,
  sensorStatusLabels,
  sensorTypeLabels,
} from "../api/models/Sensor";
import { disableSensor, enableSensor } from "../api/sensorApi";
import { useTranslation } from "react-i18next";

type Props = {
  sensor: any;
  onUpdated: () => void;
};

export default function SensorCard({ sensor, onUpdated }: Props) {
  const { t } = useTranslation();

  const handleDisable = async () => {
    try {
      await disableSensor(sensor.id);

      onUpdated();
    } catch {
      alert("Failed to disable sensor");
    }
  };

  const handleEnable = async () => {
    try {
      await enableSensor(sensor.id);

      onUpdated();
    } catch {
      alert("Failed to enable sensor");
    }
  };

  return (
    <div className="bg-[#2F3E46] p-4 rounded-xl text-white">
      <div className="flex justify-between items-center">
        <h3 className="font-semibold">
          {t(sensorTypeLabels[sensor.type as SensorType])}
        </h3>
        <span className="text-sm">
          {t(sensorStatusLabels[sensor.status as SensorStatus])}
        </span>

        <div className="mt-3 text-3xl font-bold">
          {sensor.currentValue ?? "--"}
        </div>

        <div className="text-sm text-gray-300 mt-2">
          {t("range")}: {sensor.minValue} - {sensor.maxValue}
        </div>

        <div className="flex gap-2">
          {sensor.status !== SensorStatus.Inactive ? (
            <button
              onClick={handleDisable}
              className="bg-red-500 px-3 py-2 rounded"
            >
              {t("disable")}
            </button>
          ) : (
            <button
              onClick={handleEnable}
              className="bg-green-600 px-3 py-2 rounded"
            >
              {t("enable")}
            </button>
          )}
        </div>
      </div>
    </div>
  );
}
