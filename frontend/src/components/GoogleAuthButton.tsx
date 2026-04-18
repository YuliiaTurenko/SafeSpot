import { GoogleLogin } from "@react-oauth/google";
import { googleLogin } from "../api/authApi";
import { useAuth } from "../context/AuthContext";

export default function GoogleAuthButton() {
  const { setToken } = useAuth();

  return (
    <GoogleLogin
      onSuccess={async (credentialResponse) => {
        if (!credentialResponse.credential) return;

        const res = await googleLogin(credentialResponse.credential);

        setToken(res.data.token);
      }}
      onError={() => {
        console.log("Google Login Failed");
      }}
    />
  );
}