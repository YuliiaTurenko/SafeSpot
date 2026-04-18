import {apiAuth} from "./axios";
import {
  RegisterRequest,
  LoginRequest,
  ConfirmEmailRequest,
} from "./models/requests/AuthRequests";


export const register = (request: RegisterRequest) =>
  apiAuth.post("/auth/register", { request });

export const login = (request: LoginRequest) =>
  apiAuth.post("/auth/login", { request });

export const googleLogin = (idToken: string) =>
  apiAuth.post("/auth/google", { idToken });

export const confirmEmail = (request: ConfirmEmailRequest) =>
  apiAuth.get(`/auth/confirm-email?userId=${request.userId}&token=${request.token}`);

export const resendConfirmation = (email: string) =>
  apiAuth.post("/auth/resend-confirmation", { email });

export default apiAuth;