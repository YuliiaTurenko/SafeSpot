import { useEffect, useState } from "react";
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
import {
  getWeeklySensorReadings,
  getMontlySensorReadings,
} from "../api/sensorApi";

ChartJS.register(
  CategoryScale,
  LinearScale,
  PointElement,
  LineElement,
  Title,
  Tooltip,
  Legend,
);

type Period = "latest" | "weekly" | "monthly";

type Props = {
  open: boolean;
  onClose: () => void;
  sensor: any;
  initialReadings: any[];
};

export default function SensorChartModal({
  open,
  onClose,
  sensor,
  initialReadings,
}: Props) {
  const { t } = useTranslation();

  const [activePeriod, setActivePeriod] = useState<Period>("latest");
  const [readings, setReadings] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    if (open) {
      setReadings(initialReadings || []);
      setActivePeriod("latest");
    }
  }, [open, initialReadings, sensor?.id]);

  const handlePeriodChange = async (period: Period) => {
    if (!sensor?.id || period === activePeriod) return;

    setActivePeriod(period);
    setLoading(true);

    try {
      let res;
      if (period === "weekly") {
        res = await getWeeklySensorReadings(sensor.id);
        setReadings(res.data);
      } else if (period === "monthly") {
        res = await getMontlySensorReadings(sensor.id);
        setReadings(res.data);
      } else {
        setReadings(initialReadings);
      }
    } catch (error) {
      console.error("Error fetching readings:", error);
    } finally {
      setLoading(false);
    }
  };

  if (!open || !sensor) return null;

  const labels = readings.map((r) => {
    const date = new Date(r.timestamp);
    return date.toLocaleString("uk-UA", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  });

  const values = readings.map((r) => Number(r.value));

  const data = {
    labels,
    datasets: [
      {
        label: t(sensorTypeLabels[sensor.type as SensorType]),
        data: values,
        tension: 0.2,
        borderColor: "#354F52",
        backgroundColor: "rgba(53, 79, 82, 0.2)",
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
          font: { size: 14, weight: "bold" as const },
        },
      },
    },
    scales: {
      x: {
        grid: { color: "rgba(47, 62, 70, 0.12)" },
        ticks: { color: "#2F3E46" },
      },
      y: {
        grid: { color: "rgba(47, 62, 70, 0.12)" },
        ticks: { color: "#2F3E46" },
      },
    },
  };

  return (
    <div className="fixed inset-0 bg-black/60 flex items-center justify-center z-50 animate-fade-in">
      <div className="bg-[#CAD2C5] w-[1000px] max-w-[95vw] h-[670px] rounded-2xl p-6 text-[#2F3E46] flex flex-col justify-between shadow-2xl">
        <div className="flex justify-between items-start mb-2">
          <div>
            <h2 className="text-2xl font-bold">
              {t(sensorTypeLabels[sensor.type as SensorType])} {t("chart")}
            </h2>
            <p className="text-gray-600 text-sm">{t("historicalReadings")}</p>
          </div>

          <button
            onClick={onClose}
            className="bg-red-500 hover:bg-red-600 text-white px-5 py-2 rounded-xl font-medium transition-colors shadow-md"
          >
            {t("close")}
          </button>
        </div>

        <div className="flex bg-[#A3B19B]/40 p-1 rounded-xl gap-1 w-full max-w-md mx-auto my-2">
          <button
            disabled={activePeriod === "latest" || loading}
            onClick={() => handlePeriodChange("latest")}
            className={`flex-1 py-2 text-sm font-semibold rounded-lg transition-all ${
              activePeriod === "latest"
                ? "bg-[#354F52] text-white shadow"
                : "text-[#2F3E46] hover:bg-[#A3B19B]/60"
            } disabled:opacity-90`}
          >
            {t("latest")}
          </button>

          <button
            disabled={activePeriod === "weekly" || loading}
            onClick={() => handlePeriodChange("weekly")}
            className={`flex-1 py-2 text-sm font-semibold rounded-lg transition-all ${
              activePeriod === "weekly"
                ? "bg-[#354F52] text-white shadow"
                : "text-[#2F3E46] hover:bg-[#A3B19B]/60"
            } disabled:opacity-90`}
          >
            {t("weekly")} 
          </button>

          <button
            disabled={activePeriod === "monthly" || loading}
            onClick={() => handlePeriodChange("monthly")}
            className={`flex-1 py-2 text-sm font-semibold rounded-lg transition-all ${
              activePeriod === "monthly"
                ? "bg-[#354F52] text-white shadow"
                : "text-[#2F3E46] hover:bg-[#A3B19B]/60"
            } disabled:opacity-90`}
          >
            {t("monthly")}
          </button>
        </div>

        <div className="h-[480px] bg-white/40 border border-[#A3B19B]/30 rounded-2xl p-4 relative flex items-center justify-center">
          {loading ? (
            <div className="flex flex-col items-center gap-2">
              <div className="w-10 h-10 border-4 border-[#354F52] border-t-transparent rounded-full animate-spin"></div>
              <span className="text-sm font-medium">{t("loading")}...</span>
            </div>
          ) : readings.length > 0 ? (
            <Line data={data} options={lightOptions} />
          ) : (
            <span className="text-gray-500 font-medium">
              {t("noDataAvailable")}
            </span>
          )}
        </div>
      </div>
    </div>
  );
}
