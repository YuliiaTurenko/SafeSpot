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
  canManage?: boolean;
};

export default function SensorCard({
  sensor,
  onUpdated,
  canManage = false,
}: Props) {
  const { t } = useTranslation();

  const handleDisable = async () => {
    try {
      await disableSensor(sensor.id);

      onUpdated?.();
    } catch {
      alert("Failed to disable sensor");
    }
  };

  const handleEnable = async () => {
    try {
      await enableSensor(sensor.id);

      onUpdated?.();
    } catch {
      alert("Failed to enable sensor");
    }
  };

  return (
    <>
      {canManage ? (
        <div className="bg-[#2F3E46] min-w-[350px] p-4 rounded-xl text-white shadow-lg w-full">
          <div className="space-y-3">
            <div className="flex justify-between items-center">
              <h3 className="font-semibold">
                {t(sensorTypeLabels[sensor.type as SensorType])}
              </h3>
              <span className="text-sm">
                {t(sensorStatusLabels[sensor.status as SensorStatus])}
              </span>
            </div>

            <div className="mt-3 text-3xl font-bold">
              {sensor.currentValue ?? "--"}
            </div>

            <div className="text-sm text-gray-300 mt-2">
              {t("range")}: {sensor.minValue} - {sensor.maxValue}
            </div>

            <div className="flex gap-2">
              {sensor.status !== SensorStatus.Offline &&
              sensor.status !== SensorStatus.Inactive ? (
                <button
                  onClick={handleDisable}
                  className="bg-red-500 hover:bg-red-600 px-3 py-2 rounded-lg"
                >
                  {t("disable")}
                </button>
              ) : (
                <button
                  onClick={handleEnable}
                  className="bg-green-600 hover:bg-green-700 px-3 py-2 rounded-lg"
                >
                  {t("enable")}
                </button>
              )}
            </div>
          </div>
        </div>
      ) : (
        <div className="bg-[#2F3E46] p-3 rounded-xl text-white shadow-md w-full flex flex-col justify-between min-h-[110px]">
          <div className="flex justify-between items-start gap-1">
            <h3 className="font-medium text-sm text-gray-200">
              {t(sensorTypeLabels[sensor.type as SensorType])}
            </h3>
            <span className="text-xs opacity-75 shrink-0 bg-black/20 px-1.5 py-0.5 rounded">
              {t(sensorStatusLabels[sensor.status as SensorStatus])}
            </span>
          </div>

          <div className="my-1.5 text-2xl font-bold tracking-tight">
            {sensor.currentValue ?? "--"}
          </div>

          <div className="text-xs text-gray-400">
            {t("range")}: {sensor.minValue} - {sensor.maxValue}
          </div>
        </div>
      )}
    </>
  );
}
