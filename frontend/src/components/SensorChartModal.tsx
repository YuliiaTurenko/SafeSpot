import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  Tooltip,
  ResponsiveContainer,
  CartesianGrid,
} from "recharts";
import { SensorType, sensorTypeLabels } from "../api/models/Sensor";

type Props = {
  open: boolean;
  onClose: () => void;
  sensor: any;
  readings: any[];
};
import { useTranslation } from "react-i18next";

export default function SensorChartModal({
  open,
  onClose,
  sensor,
  readings,
}: Props) {
  if (!open) return null;

  const { t } = useTranslation();

  const chartData = readings.map((r) => ({
    value: r.value,
    time: new Date(r.timestamp).toLocaleTimeString(),
  }));

  return (
    <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50">
      <div className="bg-[#1F2937] w-[900px] max-w-[95vw] rounded-2xl p-6 text-white">
        <div className="flex justify-between items-center mb-6">
          <div>
            <h2 className="text-2xl font-semibold">{t(sensorTypeLabels[sensor.type as SensorType])} {t("chart")}</h2>
            <p className="text-gray-400 text-sm">{t("historicalReadings")}</p>
          </div>

          <button onClick={onClose} className="bg-red-500 px-4 py-2 rounded">
            {t("close")}
          </button>
        </div>

        <div className="h-[400px]">
          <ResponsiveContainer width="100%" height="100%">
            <LineChart data={chartData}>
              <CartesianGrid strokeDasharray="3 3" />

              <XAxis dataKey="time" />

              <YAxis />

              <Tooltip />

              <Line type="monotone" dataKey="value" strokeWidth={3} />
            </LineChart>
          </ResponsiveContainer>
        </div>
      </div>
    </div>
  );
}
