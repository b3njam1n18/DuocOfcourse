import { Outlet } from "react-router-dom";
export default function AuthLayout() {
  return (
    <div className="min-h-screen bg-duocazul w-full flex items-center justify-center p-4">
      <Outlet />
    </div>
  );
}