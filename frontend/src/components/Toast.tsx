import { useEffect } from "react";

type Props = {
  message: string;
  type?: "error" | "success";
  onClose: () => void;
};

export default function Toast({ message, type = "error", onClose }: Props) {
  useEffect(() => {
    const timer = setTimeout(onClose, 3000);
    return () => clearTimeout(timer);
  }, [onClose]);

  return (
    <div className="fixed top-5 left-1/2 -translate-x-1/2 z-50">
      <div
        className={`px-6 py-3 rounded-lg shadow-lg text-white
        ${type === "error" ? "bg-red-500" : "bg-green-500"}`}
      >
        {message}
      </div>
    </div>
  );
}