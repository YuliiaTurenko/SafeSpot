import { createContext, useContext, useState } from "react";
import Toast from "../components/Toast";

type ToastType = {
  message: string;
  type: "error" | "success";
};

const ToastContext = createContext<any>(null);

export const ToastProvider = ({ children }: any) => {
  const [toast, setToast] = useState<ToastType | null>(null);

  const showToast = (message: string, type: "error" | "success" = "error") => {
    setToast({ message, type });
  };

  return (
    <ToastContext.Provider value={{ showToast }}>
      {children}

      {toast && (
        <Toast
          message={toast.message}
          type={toast.type}
          onClose={() => setToast(null)}
        />
      )}
    </ToastContext.Provider>
  );
};

export const useToast = () => {
  const context = useContext(ToastContext);

  if (!context) {
    throw new Error("useToast must be used within ToastProvider");
  }

  return context;
};