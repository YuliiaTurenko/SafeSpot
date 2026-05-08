import { useEffect, useState } from "react";
import { getMe, updateUser } from "../api/userApi";
import { sendAdminRequest } from "../api/adminApi";
import { Menu } from "lucide-react";
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

  const [isOpen, setIsOpen] = useState(false);
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
    <div className="min-h-screen bg-[#354F52] text-white flex justify-center items-center">
      <div
        className={`fixed top-0 left-0 h-full bg-[#2F3E46] w-64 p-5 transform ${
          isOpen ? "translate-x-0" : "-translate-x-full"
        } transition-transform duration-300 z-50`}
      >
        <h2 className="text-xl mb-6">Menu</h2>

        <nav className="flex flex-col gap-4">
          <button className="text-left hover:text-[#84A98C]">Home</button>
          <button className="text-left hover:text-[#84A98C]">Shelters</button>
          <button className="text-left hover:text-[#84A98C]">Favorites</button>
          <button className="text-left hover:text-[#84A98C]">
            Notifications
          </button>
          <button
            onClick={() => navigate("/profile")}
            className="text-left hover:text-[#84A98C]"
          >
            Profile
          </button>
        </nav>
      </div>

      {isOpen && (
        <div
          className="fixed inset-0 bg-black opacity-40 z-40"
          onClick={() => setIsOpen(false)}
        />
      )}

      <div className="bg-[#2F3E46] p-8 rounded-2xl w-full max-w-md">
        <div className="flex items-center gap-4 mb-6">
          <button onClick={() => setIsOpen(true)}>
            <Menu size={28} />
          </button>
        </div>
        
        <div className="absolute top-8 right-12">
          <LanguageButton />
        </div>

        <h2 className="text-5xl font-bold text-[#CAD2C5] mb-10 text-center">
          Profile
        </h2>

        <input
          className="w-full mb-3 p-2 rounded bg-[#CAD2C5] text-black"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          placeholder="First name"
        />

        <input
          className="w-full mb-3 p-2 rounded bg-[#CAD2C5] text-black"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          placeholder="Last name"
        />

        <div className="text-sm mb-4 opacity-70">Email: {user.email}</div>

        <button
          onClick={handleUpdate}
          className="w-full bg-[#84A98C] text-black p-2 rounded mb-3"
        >
          Save changes
        </button>

        <button
          onClick={() => setShowModal(true)}
          className="w-full bg-[#52796F] p-2 rounded"
        >
          Request Admin Role
        </button>

        {status && (
          <p className="text-center mt-3 text-sm text-[#CAD2C5]">{status}</p>
        )}
      </div>

      {/* Modal */}
      {showModal && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex justify-center items-center">
          <div className="bg-[#2F3E46] p-6 rounded-xl w-full max-w-sm">
            <h3 className="mb-3">Why do you want to be admin?</h3>

            <textarea
              className="w-full p-2 rounded bg-[#CAD2C5] text-black mb-3"
              value={message}
              onChange={(e) => setMessage(e.target.value)}
            />

            <button
              onClick={handleAdminRequest}
              className="w-full bg-[#84A98C] text-black p-2 rounded mb-2"
            >
              Send
            </button>

            <button
              onClick={() => setShowModal(false)}
              className="w-full bg-[#52796F] p-2 rounded"
            >
              Cancel
            </button>
          </div>
        </div>
      )}
    </div>
  );
}
