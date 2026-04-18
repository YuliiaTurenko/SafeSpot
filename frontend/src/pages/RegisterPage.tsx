import { useState } from "react";
import { register } from "../api/authApi";
import GoogleAuthButton from "../components/GoogleAuthButton";
import { useTranslation } from "react-i18next";

export default function RegisterPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { t } = useTranslation();

  const handleRegister = async () => {
    await register({email, password});
    alert("Check your email to confirm");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#2F3E46] p-4">
      <div className="bg-[#354F52] p-12 rounded-3xl shadow-2xl w-[80vw] h-[90vh] flex flex-col justify-center">
        <div className="max-w-2xl mx-auto w-full">
          <h2 className="text-5xl font-bold text-[#CAD2C5] mb-2 text-center">
            {t("register")}
          </h2>
          <div className="grid grid-cols-1 gap-y-6 sm:grid-cols-6">
            <div className="sm:col-span-4 sm:col-start-2">
              <label className="block text-2xl font-medium text-[#CAD2C5] mb-1.5">
                {t("email")}
              </label>
              <div className="flex items-center rounded-md bg-white/5 px-3 outline-1 -outline-offset-1 outline-white/10 focus-within:outline-2 focus-within:-outline-offset-2 focus-within:outline-[#84A98C]">
                <input
                  type="email"
                  placeholder="email@example.com"
                  className="block w-full bg-transparent py-2.5 text-xl text-white placeholder:text-gray-500 focus:outline-none sm:text-sm"
                  onChange={(e) => setEmail(e.target.value)}
                />
              </div>
            </div>

            <div className="sm:col-span-4 sm:col-start-2">
              <label className="block text-2xl font-medium text-[#CAD2C5] mb-1.5">
                {t("password")}
              </label>
              <div className="flex items-center rounded-md bg-white/5 px-3 outline-1 -outline-offset-1 outline-white/10 focus-within:outline-2 focus-within:-outline-offset-2 focus-within:outline-[#84A98C]">
                <input
                  type="password"
                  className="block w-full bg-transparent py-2.5 text-xl text-white placeholder:text-gray-500 focus:outline-none sm:text-sm"
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
            </div>

            <div className="sm:col-span-4 sm:col-start-2 mt-4 space-y-4">
              <button
                onClick={handleRegister}
                className="w-full rounded-md bg-[#84A98C] px-4 py-2.5 text-l font-semibold text-[#2F3E46] shadow-sm hover:bg-[#678ABE] hover:text-white transition-colors"
              >
                {t("signUp")}
              </button>

              <div className="w-full justify-center rounded-md">
                <GoogleAuthButton />
              </div>

              <p className="text-center text-l text-[#CAD2C5]">
                {t("notNew")}{" "}
                <a
                  href="/"
                  className="font-semibold text-[#84A98C] hover:underline"
                >
                  {t("login")}
                </a>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
