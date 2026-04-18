import "./index.css";
import "./i18n";
import App from './App.tsx'
import { createRoot } from 'react-dom/client'
import { GoogleOAuthProvider } from "@react-oauth/google";


createRoot(document.getElementById('root')!).render(
  <GoogleOAuthProvider clientId="20989808913-tr7ganbg9hlko81lmt3fod8fll01ee2k.apps.googleusercontent.com">
    <App />
  </GoogleOAuthProvider>
)
