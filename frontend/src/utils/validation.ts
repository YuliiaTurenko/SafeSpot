export const validateEmail = (email: string) => {
  if (!email) return "Email is required";

  const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!regex.test(email)) return "Invalid email";

  return "";
};

export const validatePassword = (password: string) => {
  if (!password) return "Password is required";

  if (password.length < 6)
    return "Min 6 characters";

  if (!/[0-9]/.test(password))
    return "At least one digit";

  if (!/[a-z]/.test(password))
    return "At least one lowercase letter";

  if (!/[A-Z]/.test(password))
    return "At least one uppercase letter";

  if (!/[^A-Za-z0-9]/.test(password))
    return "At least one special character";

  return "";
};