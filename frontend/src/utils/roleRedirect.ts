import { jwtDecode } from "jwt-decode";

type JwtPayload = {
  role?: string | string[];
};

export const getRedirectByRole = (token: string) => {
  const decoded = jwtDecode<JwtPayload>(token);

  const roles = decoded.role;

  if (!roles) return "/home";

  if (Array.isArray(roles)) {
    if (roles.includes("Admin")) return "/admin";
    if (roles.includes("Operator")) return "/operator";
  } else {
    if (roles === "Admin") return "/admin";
    if (roles === "Operator") return "/operator";
  }

  return "/";
};
