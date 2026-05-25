import { useState, useEffect } from "react";
import { Menu } from "lucide-react";
import { useTranslation } from "react-i18next";
import LanguageButton from "../components/LanguageButton";
import Sidebar from "../components/Sidebar";
import ShelterMap from "../components/ShelterMap";
import { getShelters } from "../api/shelterApi";
import { ShelterPreviewDto } from "../api/models/Shelter";

export default function HomePage() {
  const [isOpen, setIsOpen] = useState(false);
  const { t } = useTranslation();
  const [shelters, setShelters] = useState([]);
  const [search, setSearch] = useState("");
  const [selectedShelter, setSelectedShelter] = useState<any | null>(null);

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const res = await getShelters();
    setShelters(res.data);
  };

  const filteredShelters = shelters.filter((shelter: ShelterPreviewDto) =>
    shelter.address.toLowerCase().includes(search.toLowerCase()),
  );

  return (
    <div className="min-h-screen flex bg-[#354F52] text-white">
      <Sidebar isOpen={isOpen} onClose={() => setIsOpen(false)} />

      <div className="flex-1 p-6 w-full">
        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>
        <div className="flex items-center gap-4 mb-6">
          <button onClick={() => setIsOpen(true)}>
            <Menu size={28} />
          </button>

          <div className="relative">
            <input
              type="text"
              placeholder={t("searchShelters")}
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="bg-white text-black p-3 rounded-xl flex-1 border border-gray-200"
            />
            {search.length > 0 && filteredShelters.length > 0 && (
              <div className="absolute mt-2 w-full md:w-[400px] bg-[#354F52] rounded-xl shadow-lg z-[1000] overflow-hidden">
                {filteredShelters.map((shelter: ShelterPreviewDto) => (
                  <button
                    key={shelter.id}
                    onClick={() => {
                      setSelectedShelter(shelter);

                      setSearch("");

                      setTimeout(() => {
                        const mapSection =
                          document.getElementById("map-section");

                        mapSection?.scrollIntoView({
                          behavior: "smooth",
                        });
                      }, 100);
                    }}
                    className="w-full text-left px-4 py-3 hover:bg-[#52796F] transition"
                  >
                    <p className="font-medium">{shelter.address}</p>

                    <p className="text-sm text-gray-300">
                      Capacity: {shelter.capacity}
                    </p>
                  </button>
                ))}
              </div>
            )}
          </div>
        </div>

        <div className="h-[90%] rounded-2xl overflow-hidden">
          <ShelterMap shelters={shelters} selectedShelter={selectedShelter} />
        </div>
      </div>
    </div>
  );
}
