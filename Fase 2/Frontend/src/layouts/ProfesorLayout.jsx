import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import NavbarProfesor from "../components/NavbarProfesor";   // ajusta el path si difiere
import HeaderProfesor from "../components/HeaderProfesor";

export default function ProfesorLayout() {
  const [menuOpen, setMenuOpen] = useState(false);

  // (opcional) bloquear scroll del body cuando el drawer está abierto
  useEffect(() => {
    document.body.style.overflow = menuOpen ? "hidden" : "";
    return () => { document.body.style.overflow = ""; };
  }, [menuOpen]);

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Sidebar: desktop fijo + mobile drawer */}
      <NavbarProfesor open={menuOpen} onClose={() => setMenuOpen(false)} />

      {/* Contenido: en desktop deja espacio al sidebar (64 = 16rem) */}
      <div className="lg:pl-64">
        <HeaderProfesor onOpenMenu={() => setMenuOpen(true)} />
        <main className="p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
}
