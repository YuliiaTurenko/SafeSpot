import { useState } from "react";
import { createUser } from "../api/userApi";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageButton from "../components/LanguageButton";

export default function CompleteProfilePage() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const navigate = useNavigate();
  const { t } = useTranslation();

  const handleSubmit = async () => {
    try {
      await createUser({ firstName, lastName });
      navigate("/home");
    } catch (e: any) {
      alert(e.response?.data || "Error");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#2F3E46] p-4">
      <div className="bg-[#354F52] p-12 rounded-3xl w-[80vw] h-[90vh] flex flex-col justify-center">
        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>
        <div className="max-w-2xl mx-auto w-full">
          <h2 className="text-5xl font-bold text-[#CAD2C5] mb-10 text-center">
            {t("completeResitration")}
          </h2>
          <div className="grid grid-cols-1 gap-y-6 sm:grid-cols-6">
            <div className="sm:col-span-4 sm:col-start-2">
              <label className="block text-2xl font-medium text-[#CAD2C5] mb-1.5">
                {t("firstName")}
              </label>
              <div className="flex items-center rounded-md bg-white/5 px-3 outline-1 -outline-offset-1 outline-white/10 focus-within:outline-2 focus-within:-outline-offset-2 focus-within:outline-[#84A98C]">
                <input
                  value={firstName}
                  className="block w-full bg-transparent py-2.5 text-xl text-white placeholder:text-gray-300 focus:outline-none sm:text-sm"
                  placeholder={t("firstNameExample")}
                  onChange={(e) => setFirstName(e.target.value)}
                />
              </div>
            </div>
            <div className="sm:col-span-4 sm:col-start-2">
              <label className="block text-2xl font-medium text-[#CAD2C5] mb-1.5">
                {t("lastName")}
              </label>
              <div className="flex items-center rounded-md bg-white/5 px-3 outline-1 -outline-offset-1 outline-white/10 focus-within:outline-2 focus-within:-outline-offset-2 focus-within:outline-[#84A98C]">
                <input
                  value={lastName}
                  className="block w-full bg-transparent py-2.5 text-xl text-white placeholder:text-gray-300 focus:outline-none sm:text-sm"
                  placeholder={t("lastNameExample")}
                  onChange={(e) => setLastName(e.target.value)}
                />
              </div>
            </div>
            <div className="sm:col-span-4 sm:col-start-2 mt-4 space-y-4">
              <button
                onClick={handleSubmit}
                className="w-full bg-[#84A98C] text-black px-4 py-2.5 rounded font-semibold text-[#2F3E46] shadow-sm hover:bg-[#678ABE] hover:text-white transition-colors"
              >
                {t("save")}
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
