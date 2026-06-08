import { useEffect, useState } from "react";
import { getMe, updateUser } from "../api/userApi";
import { sendAdminRequest } from "../api/adminApi";
import { useNavigate } from "react-router-dom";
import { useTranslation } from "react-i18next";
import LanguageButton from "../components/LanguageButton";

export default function ProfilePage() {
  const [user, setUser] = useState<any>(null);
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [message, setMessage] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [status, setStatus] = useState("");

  const { t } = useTranslation();
  const navigate = useNavigate();

  useEffect(() => {
    loadUser();
  }, []);

  const loadUser = async () => {
    try {
      const res = await getMe();
      setUser(res.data);
      setFirstName(res.data.firstName);
      setLastName(res.data.lastName);
    } catch {
      setStatus("Error loading user");
    }
  };

  const handleUpdate = async () => {
    try {
      await updateUser({ firstName, lastName });
      setStatus("Profile updated");
    } catch {
      setStatus("Error updating profile");
    }
  };

  const handleAdminRequest = async () => {
    try {
      await sendAdminRequest({ message });
      setShowModal(false);
      setMessage("");
      setStatus("Request sent");
    } catch {
      setStatus("Error sending request");
    }
  };

  if (!user) return <div className="text-white p-6">Loading...</div>;

  return (
    <div className="min-h-screen bg-[#354F52] text-white mx-auto p-6 text-white">
      <div className="flex justify-between items-center mb-8">
        <button
          onClick={() => navigate(-1)}
          className="bg-[#52796F] hover:bg-[#2F3E46] text-white px-4 py-2 rounded-lg font-medium shadow-md transition-all mb-4"
        >
          ← {t("back")}
        </button>

        <h1 className="text-3xl font-bold mb-10 text-center">
          {" "}
          {t("profile")}
        </h1>

        <LanguageButton />
      </div>

      <div className="max-w-xl mx-auto p-6">
        <input
          className="w-full mb-3 p-2 rounded bg-[#CAD2C5] text-black"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder={t("firstName")}
        />

        <input
          className="w-full mb-3 p-2 rounded bg-[#CAD2C5] text-black"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          placeholder={t("lastName")}
        />

        <button
          onClick={handleUpdate}
          className="w-full bg-[#84A98C] text-black p-2 rounded mb-3"
        >
          {t("save")}
        </button>

        <button
          onClick={() => setShowModal(true)}
          className="w-full bg-[#52796F] p-2 rounded"
        >
          {t("adminRequest")}
        </button>

        {status && (
          <p className="text-center mt-3 text-sm text-[#CAD2C5]">{status}</p>
        )}
      </div>

      {showModal && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
          <div className="bg-[#2F3E46] p-6 rounded-xl w-full max-w-sm">
            <h3 className="mb-3">{t("whyAdmin")}</h3>

            <textarea
              className="w-full p-2 rounded bg-[#CAD2C5] text-black mb-3"
              value={message}
              onChange={(e) => setMessage(e.target.value)}
            />

            <button
              onClick={handleAdminRequest}
              className="w-full bg-[#84A98C] text-black p-2 rounded mb-2"
            >
              {t("send")}
            </button>

            <button
              onClick={() => setShowModal(false)}
              className="w-full bg-[#52796F] p-2 rounded"
            >
              {t("cancel")}
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
