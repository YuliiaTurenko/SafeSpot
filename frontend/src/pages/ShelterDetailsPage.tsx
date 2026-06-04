import { useParams, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { getShelterById } from "../api/shelterApi";
import { getResourcesByShelterId } from "../api/shelterResourceApi";
import { getAnnouncementsByShelterId } from "../api/announcementApi";
import { getSensorsByShelterId } from "../api/sensorApi";
import { ShelterDto, ShelterStatus, statusLabels } from "../api/models/Shelter";
import {
  ShelterResourceDto,
  ResourceType,
  ResourceStatus,
  resourceTypeLabels,
  resourceStatusLabels,
} from "../api/models/ShelterResource";
import { AnnouncementDto } from "../api/models/Announcement";
import { sensorHub } from "../services/signalr/sensorHub";
import SensorCard from "../components/SensorCard";
import LanguageButton from "../components/LanguageButton";
import { useTranslation } from "react-i18next";

export default function ShelterDetailsPage() {
  const { id } = useParams();
  const [shelter, setShelter] = useState<ShelterDto | null>(null);
  const [resources, setResources] = useState<ShelterResourceDto[]>([]);
  const [announcements, setAnnouncements] = useState<AnnouncementDto[]>([]);
  const [sensors, setSensors] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const { t } = useTranslation();

  useEffect(() => {
    if (!id) return;
    loadData();
  }, [id]);

  useEffect(() => {
    if (loading || !id) return;

    sensorHub.start(Number(id));

    sensorHub.onSensorReading((reading) => {
      setSensors((prev) =>
        prev.map((s) =>
          s.id === reading.sensorId ? { ...s, currentValue: reading.value } : s,
        ),
      );
    });

    return () => {
      sensorHub.leaveShelter(Number(id));
    };
  }, [loading, id]);

  const loadData = async () => {
    try {
      setLoading(true);
      const [shelterRes, resourcesRes, announcementsRes] = await Promise.all([
        getShelterById(Number(id)),
        getResourcesByShelterId(Number(id)),
        getAnnouncementsByShelterId(Number(id)),
      ]);

      setShelter(shelterRes.data);
      setResources(resourcesRes.data);
      setAnnouncements(announcementsRes.data);

      const res = await getSensorsByShelterId(Number(id));
      setSensors(res.data);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-[#2F3E46] text-white p-10">
        {t("loading")}
      </div>
    );
  }

  if (!shelter) {
    return (
      <div className="min-h-screen bg-[#2F3E46] text-white p-10">
        {t("shelterNotFound")}
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#2F3E46] text-white p-8">
      <div className="flex justify-between items-center mb-8">
        <h1 className="text-4xl font-bold">{t("shelterDetails")}</h1>

        <LanguageButton />
      </div>

      <div className="grid lg:grid-cols-3 gap-8">
        <div className="lg:col-span-2 space-y-8">
          <div className="bg-[#354F52] rounded-2xl p-6">
            <h2 className="text-2xl font-semibold mb-5">
              {t("mainInformation")}
            </h2>

            {shelter.imageUrl && (
              <img
                src={shelter.imageUrl}
                alt="Shelter"
                className="w-full max-h-[350px] object-cover rounded-xl mb-5"
              />
            )}

            <div className="space-y-3">
              <p>
                <span className="font-semibold">{t("address")}:</span>{" "}
                {shelter.address}
              </p>

              <p>
                <span className="font-semibold">{t("capacity")}:</span>{" "}
                {shelter.capacity}
              </p>

              <p>
                <span className="font-semibold">{t("status")}:</span>{" "}
                {t(statusLabels[shelter.status as ShelterStatus])}
              </p>

              <p>
                <span className="font-semibold">
                  {t("latitude")}, {t("longitude")}:
                </span>{" "}
                {shelter.latitude}, {shelter.longitude}
              </p>

              {shelter.description && (
                <p>
                  <span className="font-semibold">{t("description")}:</span>{" "}
                  {shelter.description}
                </p>
              )}
            </div>
          </div>

          <div className="bg-[#354F52] rounded-2xl p-6">
            <div className="flex justify-between items-center mb-5">
              <h2 className="text-2xl font-semibold">{t("announcements")}</h2>

              <Link
                to={`/shelters/${id}/posts`}
                className="bg-[#84A98C] hover:bg-[#678ABE] text-center !text-white px-4 py-2 rounded-lg"
              >
                {t("openPosts")}
              </Link>
            </div>

            <div className="space-y-4">
              {announcements.map((announcement) => (
                <div
                  key={announcement.id}
                  className="bg-[#2F3E46] p-4 rounded-xl"
                >
                  {announcement.imageUrl && (
                    <img
                      src={announcement.imageUrl}
                      alt=""
                      className="w-full max-h-[220px] object-cover rounded-lg mb-3"
                    />
                  )}

                  <h3 className="text-xl font-semibold mb-2">
                    {announcement.title}
                  </h3>

                  <p>{announcement.text}</p>
                </div>
              ))}

              {announcements.length === 0 && <p>{t("noAnnouncements")}</p>}
            </div>
          </div>
        </div>

        <div className="space-y-8">
          <div className="bg-[#354F52] rounded-2xl p-6">
            <h2 className="text-2xl font-semibold mb-5">
              {t("resourcesTitle")}
            </h2>

            <div className="space-y-3">
              {resources.map((resource) => (
                <div key={resource.id} className="bg-[#2F3E46] rounded-xl p-4">
                  <p>
                    <span className="font-semibold">{t("type")}:</span>{" "}
                    {t(resourceTypeLabels[resource.type as ResourceType])}
                  </p>

                  <p>
                    <span className="font-semibold">{t("status")}:</span>{" "}
                    {t(resourceStatusLabels[resource.status as ResourceStatus])}
                  </p>

                  <p>
                    <span className="font-semibold">{t("amount")}:</span>{" "}
                    {resource.amount}
                  </p>
                </div>
              ))}

              {resources.length === 0 && <p>{t("noResources")}</p>}
            </div>
          </div>

          <div className="bg-[#354F52] rounded-2xl p-6 mt-8">
            <h2 className="text-2xl font-semibold mb-4">{t("sensorsTitle")}</h2>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
              {sensors.map((s) => (
                <SensorCard key={s.id} sensor={s} onUpdated={loadData} canManage={false}/>
              ))}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
