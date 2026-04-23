// 📁 src/components/GoogleAuthButton.tsx
import { GoogleLogin } from "@react-oauth/google";
import { googleLogin } from "../api/authApi";
import { useAuth } from "../context/AuthContext";
import { useState } from "react";
import { useTranslation } from "react-i18next";

export default function GoogleAuthButton() {
  const { setToken } = useAuth();
  const [error, setError] = useState("");
  const { t } = useTranslation();

  return (
    <div className="w-full flex flex-col items-center">
      <GoogleLogin
        onSuccess={async (credentialResponse) => {
          try {
            setError("");

            if (!credentialResponse.credential) {
              setError(t("noCredentialReceived"));
              return;
            }

            const res = await googleLogin(credentialResponse.credential);

            setToken(res.data.token);
          } catch (e: any) {
            const message = e.response?.data || "Google login failed";
            setError(message);
          }
        }}
        onError={() => {
          setError(t("googleLoginFailed"));
        }}
      />

      {error && (
        <p className="text-red-400 text-sm mt-2 text-center">
          {error}
        </p>
      )}
    </div>
  );
}