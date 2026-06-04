import { useTranslation } from "react-i18next";
import { useNavigate } from "react-router-dom";

interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
}

export default function Sidebar({ isOpen, onClose }: SidebarProps) {
  const { t } = useTranslation();
  const navigate = useNavigate();

  const handleProfileClick = () => {
    navigate("/profile");
    onClose();
  };

  const handleNotificationClick = () => {
    navigate("/notification");
    onClose();
  };

  return (
    <>
      <div
        className={`fixed top-0 left-0 h-full bg-[#2F3E46] w-64 p-5 transform ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        } transition-transform duration-300 z-50`}
      >
        <h2 className="text-xl mb-6">{t("menu")}</h2>

        <nav className="flex flex-col gap-4">
          <button className="text-left hover:text-[#84A98C]">
            {t("savedShelters")}
          </button>
          <button 
            onClick={handleNotificationClick}
            className="text-left hover:text-[#84A98C]">
            {t("notifications")}
          </button>
          <button
            onClick={handleProfileClick}
            className="text-left hover:text-[#84A98C]"
          >
            {t("profile")}
          </button>
        </nav>
      </div>

      {isOpen && (
        <div
          className="fixed inset-0 bg-black opacity-40 z-40"
          onClick={onClose}
        />
      )}
    </>
  );
}