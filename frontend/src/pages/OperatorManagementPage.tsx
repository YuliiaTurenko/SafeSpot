import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getUsers, getOperators, assignOperator, revokeOperator } from "../api/adminApi";
import { getShelters } from "../api/shelterApi";
import LanguageButton from "../components/LanguageButton";
import { useTranslation } from "react-i18next";

interface User {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
}

interface Operator {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  shelterIds: number[];
}

interface Shelter {
  id: number;
  address: string;
}

export default function OperatorManagementPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [operators, setOperators] = useState<Operator[]>([]);
  const [shelters, setShelters] = useState<Shelter[]>([]);
  const [userSearch, setUserSearch] = useState("");
  const [operatorSearch, setOperatorSearch] = useState("");
  const [selectedUser, setSelectedUser] = useState<User | null>(null);
  const [selectedShelter, setSelectedShelter] = useState<number | null>(null);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { t } = useTranslation();

  const load = async () => {
    try {
      setLoading(true);
      const [usersRes, operatorsRes, sheltersRes] = await Promise.all([
        getUsers(),
        getOperators(),
        getShelters(),
      ]);
      setUsers(usersRes.data);
      setOperators(operatorsRes.data);
      setShelters(sheltersRes.data);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    load();
  }, []);

  const handleAssignOperator = async () => {
    if (!selectedUser || selectedShelter === null) return;
    await assignOperator({
      targetUserId: selectedUser.id,
      shelterId: selectedShelter,
    });
    setSelectedUser(null);
    setSelectedShelter(null);
    load();
  };

  const handleRevokeOperator = async (userId: number) => {
    await revokeOperator({ targetUserId: userId });
    load();
  };

  const filteredUsers = users.filter((user) => {
    const searchLower = userSearch.toLowerCase();
    return (
      user.firstName.toLowerCase().includes(searchLower) ||
      user.lastName.toLowerCase().includes(searchLower)
    );
  });

  const filteredOperators = operators.filter((operator) => {
    const searchLower = operatorSearch.toLowerCase();
    return (
      operator.firstName.toLowerCase().includes(searchLower) ||
      operator.lastName.toLowerCase().includes(searchLower)
    );
  });

  return (
    <div className="flex min-h-screen bg-[#354F52] text-white">
      <div className="flex-1 p-6 overflow-y-auto">
        <div className="flex items-center justify-between border-b border-[#52796F] pb-4 mb-6">
          <button
            onClick={() => navigate("/admin")}
            className="bg-[#52796F] hover:bg-[#2F3E46] 
            text-white px-4 py-2 rounded-lg font-medium shadow-md transition-all"
          >
            ← {t("back")}
          </button>

          <div className="absolute top-8 right-12">
            <LanguageButton />
          </div>
        </div>

        <h1 className="text-3xl font-bold mb-6">
          {t("dashboard")} - {t("operators")}
        </h1>

        {loading ? (
          <p className="text-[#CAD2C5]">{t("loading")}</p>
        ) : (
          <div className="space-y-8">
            <div className="bg-[#2F3E46] border border-[#52796F]/30 p-6 rounded-xl shadow-lg">
              <h2 className="text-2xl font-bold mb-4">{t("assignOperators")}</h2>
              
              <div className="space-y-4">
                <input
                  type="text"
                  placeholder={t("searchUsersByName")}
                  value={userSearch}
                  onChange={(e) => setUserSearch(e.target.value)}
                  className="w-full bg-white text-black p-3 rounded-xl border border-gray-200"
                />

                <select
                  value={selectedShelter || ""}
                  onChange={(e) => setSelectedShelter(Number(e.target.value))}
                  className="w-full bg-[#354F52] p-3 rounded-xl border border-[#52796F]/30"
                >
                  <option value="">{t("selectShelter")}</option>
                  {shelters.map((shelter) => (
                    <option key={shelter.id} value={shelter.id}>
                      {shelter.address}
                    </option>
                  ))}
                </select>

                <div className="max-h-48 overflow-y-auto space-y-2">
                  {filteredUsers.map((user) => (
                    <div
                      key={user.id}
                      className={`p-3 rounded-lg cursor-pointer transition-colors ${
                        selectedUser?.id === user.id
                          ? "bg-[#84A98C]"
                          : "bg-[#354F52] hover:bg-[#52796F]"
                      }`}
                      onClick={() => setSelectedUser(user)}
                    >
                      <p className="font-medium">
                        {user.firstName} {user.lastName}
                      </p>
                      <p className="text-sm text-[#CAD2C5]">{user.email}</p>
                    </div>
                  ))}
                </div>

                <button
                  onClick={handleAssignOperator}
                  disabled={!selectedUser || selectedShelter === null}
                  className="w-full bg-[#84A98C] hover:bg-[#6B9080] disabled:bg-gray-500 
                  disabled:cursor-not-allowed text-white px-4 py-2 rounded-lg font-medium transition-all"
                >
                  {t("assignAsOperatorButton")}
                </button>
              </div>
            </div>

            <div className="bg-[#2F3E46] border border-[#52796F]/30 p-6 rounded-xl shadow-lg">
              <h2 className="text-2xl font-bold mb-4">{t("currentOperators")}</h2>
              
              <input
                type="text"
                placeholder={t("searchOperatorsByName")}
                value={operatorSearch}
                onChange={(e) => setOperatorSearch(e.target.value)}
                className="w-full bg-white text-black p-3 rounded-xl border border-gray-200 mb-4"
              />

              <div className="space-y-3">
                {filteredOperators.map((operator) => (
                  <div
                    key={operator.id}
                    className="bg-[#354F52] p-4 rounded-lg flex justify-between items-center"
                  >
                    <div>
                      <p className="font-medium">
                        {operator.firstName} {operator.lastName}
                      </p>
                      <p className="text-sm text-[#CAD2C5]">{operator.email}</p>
                      <p className="text-sm text-[#CAD2C5]">
                        Shelters: {operator.shelterIds.join(", ")}
                      </p>
                    </div>
                    <button
                      onClick={() => handleRevokeOperator(operator.id)}
                      className="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg transition-colors"
                    >
                      {t("revoke")}
                    </button>
                  </div>
                ))}
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
