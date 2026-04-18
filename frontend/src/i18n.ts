import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    fallbackLng: "en",
    lng: localStorage.getItem("lang") || "en",
    interpolation: {
      escapeValue: false,
    },
    resources: {
      en: {
        translation: {
          login: "Login",
          register: "Register",
          email: "Email",
          password: "Password",
          signIn : "Sign In",
          signUp : "Sign Up",
          confirmingEmail: "Confirming your email...",
          emailConfirmed: "Email confirmed successfully!",
          confirmationFailed: "Confirmation failed",
          redirecting: "Redirecting to login...",
          resend: "Resend confirmation email",
          new: "New here?",
          notNew: "Already have an account?",
          backToLogin: "Back to Login",
        },
      },
      uk: {
        translation: {
          login: "Вхід",
          register: "Реєстрація",
          email: "Електронна пошта",
          password: "Пароль",
          signIn : "Увійти",
          signUp : "Зареєструватися",
          confirmingEmail: "Підтвердження пошти...",
          emailConfirmed: "Пошту успішно підтверджено!",
          confirmationFailed: "Помилка підтвердження",
          redirecting: "Перенаправлення...",
          resend: "Надіслати ще раз",
          new: "Не маєте акаунта?",
          notNew: "Вже маєте обліковий запис?",
          backToLogin: "Назад до входу",
        },
      },
    },
  });

export default i18n;