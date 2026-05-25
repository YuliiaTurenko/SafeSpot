import { useParams, Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { getShelterById } from "../api/shelterApi";
import { getResourcesByShelterId } from "../api/shelterResourceApi";
import { getAnnouncementsByShelterId } from "../api/announcementApi";
import { ShelterDto, ShelterStatus } from "../api/models/Shelter";
import {
  ShelterResourceDto,
  ResourceType,
  ResourceStatus,
} from "../api/models/ShelterResource";
import { AnnouncementDto } from "../api/models/Announcement";
import LanguageButton from "../components/LanguageButton";

export default function ShelterDetailsPage() {
  const { id } = useParams();
  const [shelter, setShelter] = useState<ShelterDto | null>(null);
  const [resources, setResources] = useState<ShelterResourceDto[]>([]);
  const [announcements, setAnnouncements] = useState<AnnouncementDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (!id) return;

    loadData();
  }, [id]);

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
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-[#2F3E46] text-white p-10">
        Loading...
      </div>
    );
  }

  if (!shelter) {
    return (
      <div className="min-h-screen bg-[#2F3E46] text-white p-10">
        Shelter not found
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-[#2F3E46] text-white p-8">
      <div className="flex justify-between items-center mb-8">
        <h1 className="text-4xl font-bold">Shelter Details</h1>

        <LanguageButton />
      </div>

      <div className="grid lg:grid-cols-3 gap-8">
        <div className="lg:col-span-2 space-y-8">
          <div className="bg-[#354F52] rounded-2xl p-6">
            <h2 className="text-2xl font-semibold mb-5">Main Information</h2>

            {shelter.imageUrl && (
              <img
                src={shelter.imageUrl}
                alt="Shelter"
                className="w-full max-h-[350px] object-cover rounded-xl mb-5"
              />
            )}

            <div className="space-y-3">
              <p>
                <span className="font-semibold">Address:</span>{" "}
                {shelter.address}
              </p>

              <p>
                <span className="font-semibold">Capacity:</span>{" "}
                {shelter.capacity}
              </p>

              <p>
                <span className="font-semibold">Status:</span>{" "}
                {ShelterStatus[shelter.status]}
              </p>

              <p>
                <span className="font-semibold">Coordinates:</span>{" "}
                {shelter.latitude}, {shelter.longitude}
              </p>

              {shelter.description && (
                <p>
                  <span className="font-semibold">Description:</span>{" "}
                  {shelter.description}
                </p>
              )}
            </div>
          </div>

          <div className="bg-[#354F52] rounded-2xl p-6">
            <div className="flex justify-between items-center mb-5">
              <h2 className="text-2xl font-semibold">Announcements</h2>

              <Link
                to={`/shelters/${id}/posts`}
                className="bg-[#84A98C] px-4 py-2 rounded-lg"
              >
                Open Posts
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

              {announcements.length === 0 && <p>No announcements</p>}
            </div>
          </div>
        </div>

        <div className="space-y-8">
          <div className="bg-[#354F52] rounded-2xl p-6">
            <h2 className="text-2xl font-semibold mb-5">Resources</h2>

            <div className="space-y-3">
              {resources.map((resource) => (
                <div key={resource.id} className="bg-[#2F3E46] rounded-xl p-4">
                  <p>
                    <span className="font-semibold">Type:</span>{" "}
                    {ResourceType[resource.type]}
                  </p>

                  <p>
                    <span className="font-semibold">Status:</span>{" "}
                    {ResourceStatus[resource.status]}
                  </p>

                  <p>
                    <span className="font-semibold">Amount:</span>{" "}
                    {resource.amount}
                  </p>
                </div>
              ))}

              {resources.length === 0 && <p>No resources</p>}
            </div>
          </div>

          <div className="bg-[#354F52] rounded-2xl p-6">
            <h2 className="text-2xl font-semibold mb-5">Sensor Readings</h2>

            <div className="space-y-3">
              <div className="bg-[#2F3E46] p-4 rounded-xl">Temperature: --</div>

              <div className="bg-[#2F3E46] p-4 rounded-xl">Humidity: --</div>

              <div className="bg-[#2F3E46] p-4 rounded-xl">Air quality: --</div>

              <div className="bg-[#2F3E46] p-4 rounded-xl">Occupancy: --</div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
