import { GoogleLogin } from "@react-oauth/google";
import { googleLogin } from "../api/authApi";
import { useAuth } from "../context/AuthContext";
import { useState } from "react";
import { useTranslation } from "react-i18next";
import { getRedirectByRole } from "../utils/roleRedirect";
import { useNavigate } from "react-router-dom";

export default function GoogleAuthButton() {
  const { setToken } = useAuth();
  const [error, setError] = useState("");
  const { t } = useTranslation();
  const navigate = useNavigate();

  return (
    <div className="w-full">
      <GoogleLogin
        onSuccess={async (credentialResponse) => {
          try {
            setError("");

            if (!credentialResponse.credential) {
              setError(t("noCredentialReceived"));
              return;
            }

            const res = await googleLogin(credentialResponse.credential);
            const token = res.data.token;

            setToken(token);
            
            if (res.data.requiresProfileCompletion) {
              navigate("/complete-profile");
            } else {
              const path = getRedirectByRole(token);
              navigate(path);
            }

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