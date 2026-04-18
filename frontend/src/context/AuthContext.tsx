import { createContext, useContext, useState } from "react";

type AuthContextType = {
  token: string | null;
  setToken: (token: string | null) => void;
};

const AuthContext = createContext<AuthContextType | null>(null);

export const AuthProvider = ({ children }: any) => {
  const [token, setTokenState] = useState<string | null>(
    localStorage.getItem("token"),
  );

  const setToken = (token: string | null) => {
    if (token) localStorage.setItem("token", token);
    else localStorage.removeItem("token");
    setTokenState(token);
  };

  return (
    <AuthContext.Provider value={{ token, setToken }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext)!;
