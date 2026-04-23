import { useState, useEffect } from "react";
import { login } from "../api/authApi";
import { resendConfirmation } from "../api/authApi";
import { useAuth } from "../context/AuthContext";
import { useTranslation } from "react-i18next";
import GoogleAuthButton from "../components/GoogleAuthButton";
import LanguageButton from "../components/LanguageButton";
import { Eye, EyeOff } from "lucide-react";
import { useToast } from "../context/ToastContext";
import { validateEmail } from "../utils/validation";

export default function LoginPage() {
  const { setToken } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { t } = useTranslation();
  const [showPassword, setShowPassword] = useState(false);
  const [errors, setErrors] = useState<{ email?: string; password?: string }>(
    {},
  );
  const { showToast } = useToast();
  const [cooldown, setCooldown] = useState(0);

  const handleLogin = async () => {
    const emailError = validateEmail(email);

    if (emailError) {
      setErrors({ email: emailError });
      return;
    }

    try {
      const res = await login({ email, password });
      setToken(res.data.token);
    } catch (e: any) {
      const message = e.response?.data || "Error";
      showToast(message, "error");
      setCooldown(30);
    }
  };

  const handleResend = async () => {
    try {
      await resendConfirmation(email);
      showToast(t("confirmEmail"), "success");
      setCooldown(30);
    } catch (e: any) {
      showToast(e.response?.data || "Error", "error");
    }
  };

  useEffect(() => {
    if (cooldown <= 0) return;

    const timer = setInterval(() => {
      setCooldown((prev) => prev - 1);
    }, 1000);

    return () => clearInterval(timer);
  }, [cooldown]);

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#2F3E46] p-4">
      <div className="bg-[#354F52] p-12 rounded-3xl shadow-2xl w-[80vw] h-[90vh] flex flex-col justify-center">
        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>
        <div className="max-w-2xl mx-auto w-full">
          <h2 className="text-5xl font-bold text-[#CAD2C5] mb-10 text-center">
            {t("login")}
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
              {errors.email && (
                <p className="text-red-400 text-s">{errors.email}</p>
              )}
            </div>

            <div className="sm:col-span-4 sm:col-start-2">
              <label className="block text-2xl font-medium text-[#CAD2C5] mb-1.5">
                {t("password")}
              </label>
              <div
                className="flex items-center rounded-md bg-white/5 px-3 outline-1 
                -outline-offset-1 outline-white/10 focus-within:outline-2 
                focus-within:-outline-offset-2 focus-within:outline-[#84A98C]"
              >
                <input
                  type={showPassword ? "text" : "password"}
                  className="block w-full bg-transparent py-2.5 text-xl text-white placeholder:text-gray-500 focus:outline-none sm:text-sm"
                  onChange={(e) => setPassword(e.target.value)}
                  placeholder="Введіть пароль"
                />
                <button
                  type="button"
                  onClick={togglePasswordVisibility}
                  className="text-gray-400 hover:text-white transition-colors focus:outline-none"
                >
                  {showPassword ? (
                    <EyeOff className="size-5" />
                  ) : (
                    <Eye className="size-5" />
                  )}
                </button>
              </div>
              {errors.password && (
                <p className="text-red-400 text-s">{errors.password}</p>
              )}
            </div>

            <div className="sm:col-span-4 sm:col-start-2 mt-4 space-y-4">
              <button
                onClick={handleLogin}
                className="w-full rounded-md bg-[#84A98C] px-4 py-2.5 text-l font-semibold text-[#2F3E46] shadow-sm hover:bg-[#678ABE] hover:text-white transition-colors"
              >
                {t("signIn")}
              </button>

              <button
                onClick={handleResend}
                disabled={cooldown > 0}
                className={`w-full mt-2 p-2 rounded text-[#151A3C]
                  ${cooldown > 0 ? "bg-gray-400 cursor-not-allowed" : "bg-[#CAD2C5]"}`}
              >
                {cooldown > 0 ? `${t("resend")} (${cooldown}s)` : t("resend")}
              </button>

              <div className="w-full justify-center rounded-md">
                <GoogleAuthButton />
              </div>

              <p className="text-center text-l text-[#CAD2C5]">
                {t("new")}{" "}
                <a
                  href="/register"
                  className="font-semibold text-[#84A98C] hover:underline"
                >
                  {t("register")}
                </a>
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
