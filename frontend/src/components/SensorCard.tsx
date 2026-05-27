import {
  SensorStatus,
  SensorType,
  sensorStatusLabels,
  sensorTypeLabels,
} from "../api/models/Sensor";
import { useTranslation } from "react-i18next";

type Props = {
  sensor: any;
};

export default function SensorCard({ sensor }: Props) {
  const { t } = useTranslation();

  return (
    <div className="bg-[#2F3E46] p-4 rounded-xl text-white">
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
    </div>
  );
}
