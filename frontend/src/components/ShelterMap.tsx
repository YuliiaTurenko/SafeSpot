import {
  MapContainer,
  Marker,
  Popup,
  TileLayer,
} from "react-leaflet";
import { ShelterPreviewDto } from "../api/models/Shelter";
import { Link } from "react-router-dom";
import { useTranslation } from "react-i18next";

interface Props {
  shelters: ShelterPreviewDto[];
}

export default function ShelterMap({
  shelters,
}: Props) {
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

      {shelters.map((shelter) => (
        <Marker
          key={shelter.id}
          position={[
            shelter.latitude,
            shelter.longitude,
          ]}
        >
          <Popup>
            <div className="p-3 max-w-sm bg-white rounded-xl font-sans text-slate-800">
              <div className="mb-2.5">
                <p className="text-xs font-semibold uppercase tracking-wider text-slate-400 mb-0.5">
                  {t("address")}
                </p>
                <p className="text-base font-bold leading-snug text-slate-900">
                  {shelter.address}
                </p>
              </div>

              <div className="grid grid-cols-2 gap-2 my-3 py-2 border-y border-slate-100 text-sm">
                <div>
                  <span className="block text-xs text-slate-400">{t("capacity")}</span>
                  <span className="font-semibold text-slate-700">{shelter.capacity}</span>
                </div>
                <div>
                  <span className="block text-xs text-slate-400">{t("status")}</span>
                  <span className="inline-flex items-center px-2 py-0.5 mt-0.5 rounded-full text-xs font-medium bg-emerald-50 text-emerald-700 border border-emerald-200">
                    ● {shelter.status}
                  </span>
                </div>
              </div>

              <div className="mt-3">
                <Link
                  to={`/shelters/${shelter.id}`}
                  className="block w-full text-center bg-[#52796F] hover:bg-[#415A52] text-white font-medium text-sm py-2 px-4 rounded-lg shadow-sm transition-colors duration-200"
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