import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./context/AuthContext";
import { ToastProvider } from "./context/ToastContext";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import ConfirmEmailPage from "./pages/ConfirmEmailPage";
import CompleteProfilePage from "./pages/CompleteProfilePage";
import HomePage from "./pages/HomePage";
import AdminPage from "./pages/AdminPage";
import OperatorPage from "./pages/OperatorPage";
import ProfilePage from "./pages/ProfilePage";
import ShelterDetailsPage from "./pages/ShelterDetailsPage";
import ShelterPostsPage from "./pages/ShelterPostsPage";
import AdminSensorsPage from "./pages/AdminSensorsPage";
import NotificationPage from "./pages/NotificationPage";
import OperatorManagementPage from "./pages/OperatorManagementPage";
import SavedSheltersPage from "./pages/SavedSheltersPage";

function App() {
  return (
    <AuthProvider>
      <ToastProvider>
        <BrowserRouter>
          <Routes>
            <Route path="/" element={<LoginPage />} />
            <Route path="/register" element={<RegisterPage />} />
            <Route path="/confirm-email" element={<ConfirmEmailPage />} />
            <Route path="/complete-profile" element={<CompleteProfilePage />} />
            <Route path="/home" element={<HomePage />} />
            <Route path="/admin" element={<AdminPage />} />
            <Route path="/admin/operators" element={<OperatorManagementPage />} />
            <Route path="/admin/shelter/:shelterId/sensors" element={<AdminSensorsPage />} />
            <Route path="/operator" element={<OperatorPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/notification" element={<NotificationPage />} />
            <Route path="/saved-shelters" element={<SavedSheltersPage />}/>
            <Route path="/shelters/:id" element={<ShelterDetailsPage />} />
            <Route path="/shelters/:id/posts" element={<ShelterPostsPage />} />
          </Routes>
        </BrowserRouter>
      </ToastProvider>
    </AuthProvider>
  );
}

export default App;
