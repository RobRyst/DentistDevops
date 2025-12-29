import { Navigate } from "react-router-dom";
import { tokenStorage } from "../api/Token";
import { jwtDecode } from "jwt-decode";
import type { JSX } from "react";

type JwtPayload = {
  role?: string | string[];
  roles?: string[];
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?:
    | string
    | string[];
};

export default function AdminRoute({ children }: { children: JSX.Element }) {
  const token = tokenStorage.get();

  if (!token) {
    return <Navigate to="/login" replace />;
  }

  let isAdmin = false;

  try {
    const decoded = jwtDecode<JwtPayload>(token);

    const rawRoles =
      decoded.roles ??
      decoded.role ??
      decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    const roles = Array.isArray(rawRoles)
      ? rawRoles.map(String)
      : typeof rawRoles === "string"
      ? [rawRoles]
      : [];

    isAdmin = roles.includes("Admin") || roles.includes("Provider");
  } catch {
    isAdmin = false;
  }

  if (!isAdmin) {
    return <Navigate to="/" replace />;
  }

  return children;
}
