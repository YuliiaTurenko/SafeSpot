import {
  MapContainer,
  Marker,
  Popup,
  TileLayer,
} from "react-leaflet";
import { Shelter } from "../api/models/Shelter";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";

interface Props {
  shelters: Shelter[];
}

export default function ShelterMap({
  shelters,
}: Props) {
  const navigate = useNavigate();
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
            <div className="text-black">
              <p>{shelter.address}</p>

              <button
              // !!!
                onClick={() =>
                  navigate(`/shelters/${shelter.id}`)
                }
                className="mt-2 bg-[#52796F] text-white px-3 py-1 rounded"
              >
                {t("open")}
              </button>
            </div>
          </Popup>
        </Marker>
      ))}
    </MapContainer>
  );
}