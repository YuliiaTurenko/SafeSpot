import { useState, useEffect } from "react";
import { Menu } from "lucide-react";
import { useTranslation } from "react-i18next";
import LanguageButton from "../components/LanguageButton";
import Sidebar from "../components/Sidebar";
import ShelterMap from "../components/ShelterMap";
import { getShelters } from "../api/shelterApi";

export default function HomePage() {
  const [isOpen, setIsOpen] = useState(false);
  const { t } = useTranslation();
  const [shelters, setShelters] = useState([]);

  useEffect(() => {
    load();
  }, []);

  const load = async () => {
    const res = await getShelters();
    setShelters(res.data);
  };

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

          <input
            type="text"
            placeholder={t("searchShelters")}
            className="bg-[#2F3E46] px-4 py-2 rounded-lg w-full max-w-xl outline-none"
          />
        </div>

        <div className="h-[90%] rounded-2xl overflow-hidden">
          <ShelterMap shelters={shelters} />
        </div>
      </div>
    </div>
  );
}
