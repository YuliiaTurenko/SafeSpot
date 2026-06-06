import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Line } from "react-chartjs-2";
import { useTranslation } from "react-i18next";
import { SensorType, sensorTypeLabels } from "../api/models/Sensor";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
);

type Props = {
  open: boolean;
  onClose: () => void;
  sensor: any;
  readings: any[];
};

export default function SensorChartModal({
  open,
  onClose,
  sensor,
  readings,
}: Props) {
  const { t } = useTranslation();

  if (!open || !sensor) return null;

  const labels = readings.map((r) => new Date(r.timestamp).toLocaleString());

  const values = readings.map((r) => Number(r.value));

  const data = {
    labels,
    datasets: [
      {
        label: t(sensorTypeLabels[sensor.type as SensorType]),
        data: values,
        tension: 0.3,
      },
    ],
  };

  const lightOptions = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        labels: {
          color: "#2F3E46",
          font: { size: 14 },
        },
      },
      title: {
        display: true,
        color: "#2F3E46",
      },
    },
    scales: {
      x: {
        grid: {
          color: "rgba(47, 62, 70, 0.15)",
        },
        ticks: {
          color: "#2F3E46",
        },
      },
      y: {
        grid: {
          color: "rgba(47, 62, 70, 0.15)",
        },
        ticks: {
          color: "#2F3E46", 
        },
      },
    },
  };

  return (
    <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50">
      <div className="bg-[#CAD2C5] w-[1000px] max-w-[95vw] h-[650px] rounded-2xl p-6 text-[#2F3E46]">
        <div className="flex justify-between items-center mb-4">
          <div>
            <h2 className="text-2xl font-semibold">
              {t(sensorTypeLabels[sensor.type as SensorType])} {t("chart")}
            </h2>

            <p className="text-gray-600">{t("historicalReadings")}</p>
          </div>

          <button
            onClick={onClose}
            className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded transition-colors"
          >
            {t("close")}
          </button>
        </div>

        <div className="h-[520px]">
          <Line data={data} options={lightOptions} />
        </div>
      </div>
    </div>
  );
}
