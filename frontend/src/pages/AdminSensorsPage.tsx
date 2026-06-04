import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import {
  deleteSensor,
  getSensorsByShelterId,
  getShelterAnalytics,
  getSensorReadings,
} from "../api/sensorApi";
import SensorCard from "../components/SensorCard";
import SensorModal from "../components/SensorModal";
import SensorChartModal from "../components/SensorChartModal";
import ShelterAnalyticsCards from "../components/ShelterAnalyticsCards";
import { sensorHub } from "../services/signalr/sensorHub";
import LanguageButton from "../components/LanguageButton";
import NotificationToast from "../components/NotificationToast";
import { useTranslation } from "react-i18next";

export default function AdminSensorsPage() {
  const { shelterId } = useParams();
  const shelterIdNumber = Number(shelterId);

  const [sensors, setSensors] = useState<any[]>([]);
  const [analytics, setAnalytics] = useState<any | null>(null);

  const [modalOpen, setModalOpen] = useState(false);
  const [editingSensor, setEditingSensor] = useState<any>(null);
  const [chartOpen, setChartOpen] = useState(false);
  const [selectedSensor, setSelectedSensor] = useState<any>(null);
  const [chartReadings, setChartReadings] = useState<any[]>([]);

  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { t } = useTranslation();

  useEffect(() => {
    if (!shelterIdNumber) return;
    load();
  }, [shelterIdNumber]);

  useEffect(() => {
    if (loading || !shelterIdNumber) return;

    sensorHub.start(shelterIdNumber);

    sensorHub.onSensorReading((reading) => {
      setSensors((prev) =>
        prev.map((s) =>
          s.id === reading.sensorId ? { ...s, currentValue: reading.value } : s,
        ),
      );
    });

    return () => {
      sensorHub.leaveShelter(shelterIdNumber);
    };
  }, [loading, shelterIdNumber]);

  const load = async () => {
    try {
      setLoading(true);
      const res = await getSensorsByShelterId(shelterIdNumber);
      setSensors(res.data);

      const analyticsRes = await getShelterAnalytics(shelterIdNumber);
      setAnalytics(analyticsRes.data);
    } finally {
      setLoading(false);
    }
  };

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
    <div className="flex min-h-screen bg-[#354F52] text-white">
      <div className="flex-1 p-6 overflow-y-auto space-y-8">
        <div className="flex items-center justify-between border-b border-[#52796F] pb-4">
          <button
            onClick={() => navigate("/admin")}
            className="bg-[#52796F] hover:bg-[#2F3E46] 
            text-white px-4 py-2 rounded-lg font-medium shadow-md transition-all"
          >
            ← {t("back")}
          </button>

          <div className="flex gap-3">
            <button
              onClick={() => navigate("/notification")}
              className="bg-[#84A98C] hover:bg-[#6B9080] text-white px-4 py-2 rounded-lg font-medium transition-all"
            >
              {t("notifications")}
            </button>
          </div>

          <div className="absolute top-8 right-12">
            <LanguageButton />
          </div>
        </div>

        <div className="space-y-4">
          <h2 className="text-2xl font-bold tracking-wide text-gray-100">
            {t("sensorsTitle")}
          </h2>

          <button
            onClick={() => {
              setEditingSensor(null);
              setModalOpen(true);
            }}
            className="bg-[#2F3E46] hover:bg-[#52796F] border border-[#52796F] 
            text-white px-5 py-2 rounded-lg font-medium shadow-md transition-all"
          >
            + {t("addSensor")}
          </button>

          {sensors.length === 0 ? (
            <p className="text-[#CAD2C5] italic text-sm">{t("noSensors")}</p>
          ) : (
            <div className="grid grid-cols-1 md:grid-cols-2 xl:grid-cols-3 gap-6">
              {sensors.map((s) => (
                <div
                  key={s.id}
                  className="bg-[#2F3E46] border border-[#52796F]/30 p-5 rounded-xl shadow-lg flex flex-col justify-between space-y-4 hover:border-[#52796F] transition-all duration-200"
                >
                  <div className="flex-1">
                    <SensorCard sensor={s} onUpdated={load} canManage={true}/>
                  </div>

                  <div className="flex gap-2 pt-2 border-t border-[#52796F]/20">
                    <button
                      onClick={() => handleChartOpen(s)}
                      className="flex-1 bg-[#4F772D] hover:bg-[#31572C] text-white text-sm font-medium py-2 rounded-lg transition-colors shadow"
                    >
                      {t("chart")}
                    </button>

                    <button className="flex-1 bg-[#52796F] hover:bg-[#354F52] text-white text-sm font-medium py-2 rounded-lg border border-[#CAD2C5]/20 transition-colors shadow">
                      {t("edit")}
                    </button>

                    <button
                      onClick={() => handleDelete(s.id)}
                      className="bg-red-600 hover:bg-red-700 text-white text-sm font-medium px-3 py-2 rounded-lg transition-colors shadow"
                    >
                      {t("delete")}
                    </button>
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>

        <div className="bg-[#2F3E46] border border-[#52796F]/30 p-6 rounded-xl shadow-lg space-y-5">
          <h2 className="text-2xl font-bold tracking-wide text-gray-100">
            {t("shelterAnalytics")}
          </h2>

          <ShelterAnalyticsCards analytics={analytics} />
        </div>

        <SensorModal
          open={modalOpen}
          onClose={() => setModalOpen(false)}
          shelterId={shelterIdNumber}
          sensor={editingSensor}
          onSuccess={load}
        />

        <SensorChartModal
          open={chartOpen}
          onClose={() => setChartOpen(false)}
          sensor={selectedSensor}
          readings={chartReadings}
        />

        <NotificationToast shelterId={shelterIdNumber} />
      </div>
    </div>
  );
}
