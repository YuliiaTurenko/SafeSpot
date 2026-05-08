import axios from "axios";
import i18n from "i18next";

const BASE_URL = "https://localhost:7165/api";

export const api = axios.create({
  baseURL: BASE_URL,
  withCredentials: true,
  headers: { "Content-Type": "application/json" },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem("token");
  if (token) config.headers.Authorization = `Bearer ${token}`;

  const lang = i18n.language || "en";

  config.headers = config.headers || {};
  config.headers["Accept-Language"] = lang;
  return config;
});