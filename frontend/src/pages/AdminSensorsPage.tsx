import { useEffect, useState } from "react";
import {
  deleteSensor,
  getSensorsByShelterId,
  getShelterAnalytics,
  getSensorReadings
} from "../api/sensorApi";
import SensorCard from "../components/SensorCard";
import SensorModal from "../components/SensorModal";
import SensorChartModal from "../components/SensorChartModal";
import ShelterAnalyticsTable from "../components/ShelterAnalyticsTable";
import { useTranslation } from "react-i18next";

type Props = {
  shelterId: number;
};

export default function AdminSensorsPage({ shelterId }: Props) {
  const [sensors, setSensors] = useState<any[]>([]);
  const [analytics, setAnalytics] = useState<any[]>([]);

  const [modalOpen, setModalOpen] = useState(false);
  const [editingSensor, setEditingSensor] = useState<any>(null);
  const [chartOpen, setChartOpen] = useState(false);
  const [selectedSensor, setSelectedSensor] = useState<any>(null);
  const [chartReadings, setChartReadings] = useState<any[]>([]);

  const { t } = useTranslation();

  const load = async () => {
    const res = await getSensorsByShelterId(shelterId);
    setSensors(res.data);

    const analyticsRes =
      await getShelterAnalytics(shelterId);

    setAnalytics(analyticsRes.data);
  };

  useEffect(() => {
    load();
  }, [shelterId]);

  const handleDelete = async (id: number) => {
    await deleteSensor(id);
    load();
  };

  const handleChartOpen = async (sensor: any) => {
    const res = await getSensorReadings(sensor.id);

    setChartReadings(res.data);

    setSelectedSensor(sensor);

    setChartOpen(true);
  };

  return (
    <div className="space-y-4">
      <div className="flex justify-between">
        <h2 className="text-2xl font-semibold">{t("sensorsTitle")}</h2>
        <button 
          onClick={() => {
            setEditingSensor(null);
            setModalOpen(true);
          }}
          className="bg-[#52796F] px-4 py-2 rounded">
          {t("addSensor")}
        </button>
      </div>

      <div className="grid md:grid-cols-2 lg:grid-cols-3 gap-4">
        {sensors.map((s) => (
          <div key={s.id} className="space-y-2">
            <SensorCard sensor={s} onUpdated={load}/>

            <div className="flex gap-2">
                <button
                onClick={() =>
                  handleChartOpen(s)
                }
                className="bg-[#77A2A6] hover:bg-[#56869F] text-white px-3 py-2 rounded transition-colors"
              >
                {t("chart")}
              </button>

              <button className="bg-[#678ABE] hover:bg-[#5C858D] text-white px-3 py-2 rounded transition-colors">
                {t("edit")}
              </button>

              <button
                onClick={() => handleDelete(s.id)}
                className="bg-red-500 hover:bg-red-600 px-3 py-2 rounded transition-colors"
              >
                {t("delete")}
              </button>
            </div>
          </div>
        ))}
      </div>

      <div>
        <h2 className="text-2xl font-semibold mb-4">
          {t("shelterAnalytics")}
        </h2>

        <ShelterAnalyticsTable
          analytics={analytics}
        />
      </div>

      <SensorModal
        open={modalOpen}
        onClose={() => setModalOpen(false)}
        shelterId={shelterId}
        sensor={editingSensor}
        onSuccess={load}
      />

      <SensorChartModal
        open={chartOpen}
        onClose={() => setChartOpen(false)}
        sensor={selectedSensor}
        readings={chartReadings}
      />
    </div>
  );
}
