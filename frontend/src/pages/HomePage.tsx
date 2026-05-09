import { useState } from "react";
import { Menu } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageButton from "../components/LanguageButton";

export default function HomePage() {
  const [isOpen, setIsOpen] = useState(false);
  const navigate = useNavigate();
  const { t } = useTranslation();

  return (
    <div className="min-h-screen flex bg-[#354F52] text-white">
      <div
        className={`fixed top-0 left-0 h-full bg-[#2F3E46] w-64 p-5 transform ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        } transition-transform duration-300 z-50`}
      >
        <h2 className="text-xl mb-6">{t("menu")}</h2>

        <nav className="flex flex-col gap-4">
          <button className="text-left hover:text-[#84A98C]">
            {t("shelters")}
          </button>
          <button className="text-left hover:text-[#84A98C]">
            {t("saved")}
          </button>
          <button className="text-left hover:text-[#84A98C]">
            {t("notifications")}
          </button>
          <button
            onClick={() => navigate("/profile")}
            className="text-left hover:text-[#84A98C]"
          >
            {t("profile")}
          </button>
        </nav>
      </div>

      {isOpen && (
        <div
          className="fixed inset-0 bg-black opacity-40 z-40"
          onClick={() => setIsOpen(false)}
        />
      )}

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

        <div className="bg-[#2F3E46] rounded-2xl h-[75vh] flex items-center justify-center text-gray-400">
          Map will be here
        </div>
      </div>
    </div>
  );
}
