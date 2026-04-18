import { useEffect, useState } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";
import { confirmEmail } from "../api/authApi";
import { useTranslation } from "react-i18next";

export default function ConfirmEmailPage() {
  const [params] = useSearchParams();
  const navigate = useNavigate();
  const { t } = useTranslation();

  const [status, setStatus] = useState<"loading" | "success" | "error">(
    "loading",
  );

  useEffect(() => {
    const confirm = async () => {
      try {
        const userId = params.get("userId");
        const token = params.get("token");

        if (!userId || !token) throw new Error();

        await confirmEmail({userId, token});

        setStatus("success");

        setTimeout(() => {
          navigate("/");
        }, 4000);
      } catch {
        setStatus("error");
      }
    };

    confirm();
  }, []);

  return (
    <div className="min-h-screen flex items-center justify-center bg-[#354F52]">
      <div className="bg-[#2F3E46] p-8 rounded-2xl w-96 text-center shadow-lg">
        {status === "loading" && (
          <p className="text-[#CAD2C5]">{t("confirmingEmail")}</p>
        )}

        {status === "success" && (
          <>
            <p className="text-[#84A98C] text-lg mb-4">
              {t("emailConfirmed")}
            </p>
            <p className="text-[#CAD2C5] text-sm">{t("redirecting")}</p>

            {/* <button
              onClick={() => navigate("/")}
              className="mt-4 bg-[#84A98C] px-4 py-2 rounded text-[#151A3C]"
            >
              Go to Main page
            </button> */}
          </>
        )}

        {status === "error" && (
          <>
            <p className="text-red-400 text-lg mb-4">{t("confirmationFailed")}</p>

            <button
              onClick={() => navigate("/")}
              className="bg-[#CAD2C5] px-4 py-2 rounded text-[#151A3C]"
            >
              {t("backToLogin")}
            </button>
          </>
        )}
      </div>
    </div>
  );
}
