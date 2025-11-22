import { Outlet } from "react-router-dom";
import NavbarProfesor from "../components/NavbarProfesor";
import HeaderProfesor from "../components/HeaderProfesor";

export default function ProfesorLayout() {
  return (
    <div>
      {/* Sidebar profesor */}
      <NavbarProfesor />

      {/* Contenido desplazado por el sidebar (w-64) */}
      <div className="ml-64 min-h-screen">
        <HeaderProfesor />
        <main className="p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
