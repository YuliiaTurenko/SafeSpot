import { MapContainer, Marker, Popup, TileLayer, useMap } from "react-leaflet";
import { useEffect } from "react";
import { ShelterPreviewDto, ShelterStatus, statusLabels } from "../api/models/Shelter";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

const statusStyles: Record<ShelterStatus, string> = {
  [ShelterStatus.Available]: "bg-emerald-50 text-emerald-700 border-emerald-200",
  [ShelterStatus.Closed]: "bg-rose-50 text-rose-700 border-rose-200",
  [ShelterStatus.Full]: "bg-amber-50 text-amber-700 border-amber-200",
  [ShelterStatus.Maintenance]: "bg-blue-50 text-blue-700 border-blue-200",
};

interface Props {
  shelters: ShelterPreviewDto[];
  selectedShelter?: any;
}

function ChangeMapView({ center }: { center: [number, number] }) {
  const map = useMap();

  useEffect(() => {
    map.flyTo(center, 16, {
      duration: 1.5,
    });
  }, [center]);

  return null;
}

export default function ShelterMap({ shelters, selectedShelter }: Props) {
  const { t } = useTranslation();

  return (
    <MapContainer
      center={[49.068, 33.4204]}
      zoom={12}
      className="h-full w-full rounded-2xl z-0"
    >
      <TileLayer
        attribution="&copy; OpenStreetMap contributors"
        url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
      />
      {selectedShelter && (
        <ChangeMapView
          center={[
            selectedShelter.latitude,
            selectedShelter.longitude,
          ]}
        />
      )}
      {shelters.map((shelter) => (
        <Marker
          key={shelter.id}
          position={[shelter.latitude, shelter.longitude]}
        >
          <Popup>
            <div className="p-3 max-w-sm bg-white rounded-xl font-sans text-slate-800">
              <div className="mb-2.5">
                <p className="text-xs font-semibold uppercase tracking-wider text-slate-400 m-0 p-0">
                  {t("address")}
                </p>
                <p className="text-base font-bold leading-tight text-slate-900 mt-0.5">
                  {shelter.address}
                </p>
              </div>

              <div className="grid grid-cols-2 gap-2 my-3 py-2 border-y border-slate-100 text-sm">
                <div>
                  <span className="block text-xs text-slate-400">
                    {t("capacity")}
                  </span>
                  <span className="font-semibold text-slate-700">
                    {shelter.capacity}
                  </span>
                </div>
                <div>
                  <span className="block text-xs text-slate-400">
                    {t("status")}
                  </span>
                  <span className={`inline-flex items-center px-2 py-0.5 mt-0.5 rounded-full text-xs font-medium border ${
                    statusStyles[shelter.status as ShelterStatus] || "bg-gray-50 text-gray-700 border-gray-200"}`}>
                    ● {t(statusLabels[shelter.status as ShelterStatus])}
                  </span>
                </div>
              </div>

              <div className="mt-3">
                <Link
                  to={`/shelters/${shelter.id}`}
                  className="block w-full bg-[#678ABE] hover:bg-[#45557B] text-center !text-white font-medium text-sm py-2 px-4 rounded-lg shadow-sm transition-colors duration-200"
                >
                  {t("open")}
                </Link>
              </div>
            </div>
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
}
