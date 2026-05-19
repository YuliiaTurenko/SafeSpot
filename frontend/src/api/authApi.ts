import { api } from "./axios";
import {
  RegisterRequest,
  LoginRequest,
  ConfirmEmailRequest,
} from "./models/AuthRequests";


export const register = (request: RegisterRequest) =>
  api.post("/auth/register", request );

export const login = (request: LoginRequest) =>
  api.post("/auth/login", request);

export const googleLogin = (idToken: string) =>
  api.post("/auth/google", {idToken});

export const confirmEmail = (request: ConfirmEmailRequest) =>
  api.get("/auth/confirm-email", {
    params: {
      userId: request.userId,
      token: request.token
    }
  });

export const resendConfirmation = (email: string) =>
  api.post("/auth/resend-confirmation", {email});
