import { apiAuth } from "./axios";
import {
  RegisterRequest,
  LoginRequest,
  ConfirmEmailRequest,
} from "./models/requests/AuthRequests";
import i18n from "i18next";

apiAuth.interceptors.request.use((config) => {
  const lang = i18n.language || "en";

  config.headers["Accept-Language"] = lang;

  return config;
});

export const register = (request: RegisterRequest) =>
  apiAuth.post("/auth/register", { request });

export const login = (request: LoginRequest) =>
  apiAuth.post("/auth/login", { request });

export const googleLogin = (idToken: string) =>
  apiAuth.post("/auth/google", { idToken });

export const confirmEmail = (request: ConfirmEmailRequest) =>
  apiAuth.get("/auth/confirm-email", {
    params: {
      userId: request.userId,
      token: request.token
    }
  });

export const resendConfirmation = (email: string) =>
  apiAuth.post("/auth/resend-confirmation", { email });

export default apiAuth;
