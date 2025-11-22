import React from "react";
import { NavLink } from "react-router-dom";
import logoDuoc from "../assets/DuocUCLogo.png";

function SidebarContent({ onClose }) {
  const baseBtn = "block w-full text-center rounded-md px-4 py-4 shadow-md font-semibold transition-colors";
  const activeBtn = "bg-duocamarillo text-duocgris";
  const inactiveBtn = "bg-duocgris text-white hover:bg-duocamarillo hover:text-duocgris";

  return (
    <div className="flex flex-col h-full p-4 text-white">
      <div className="flex justify-center mb-6">
        <img src={logoDuoc} alt="Logo DUOC" className="h-10" />
      </div>

      <nav className="flex flex-col gap-3">
        <NavLink to="/profesor" className={({isActive})=>`${baseBtn} ${isActive?activeBtn:inactiveBtn}`} onClick={onClose}>
          Mis cursos
        </NavLink>
        <NavLink to="/profesor/Evaluaciones" className={({isActive})=>`${baseBtn} ${isActive?activeBtn:inactiveBtn}`} onClick={onClose}>
          Evaluaciones
        </NavLink>
        <NavLink to="/profesor/Calificaciones" className={({isActive})=>`${baseBtn} ${isActive?activeBtn:inactiveBtn}`} onClick={onClose}>
          Calificaciones
        </NavLink>
      </nav>

      <div className="mt-auto pt-4 border-t border-white/10">
        <NavLink to="/Login" onClick={onClose}>
          <button className="w-full rounded-md bg-duocamarillo text-duocgris font-semibold px-3 py-4 hover:bg-duocceleste hover:text-white transition-colors">
            Cerrar sesión
          </button>
        </NavLink>
      </div>
    </div>
  );
}

export default function NavbarProfesor({ open, onClose }) {
  return (
    <>
      {/* Desktop fijo */}
      <aside className="hidden lg:flex fixed left-0 top-0 h-screen w-64 bg-duocazul z-40">
        <SidebarContent />
      </aside>

      {/* Mobile drawer + overlay */}
      <div className={`lg:hidden ${open ? "pointer-events-auto" : "pointer-events-none"}`}>
        {/* overlay para cerrar al tocar fuera */}
        <div
          className={`fixed inset-0 z-50 bg-black/50 transition-opacity ${open ? "opacity-100" : "opacity-0"}`}
          onClick={onClose}
        />
        {/* panel deslizante */}
        <aside
          className={`fixed left-0 top-0 z-50 h-screen w-64 bg-duocazul transform transition-transform duration-300 ${
            open ? "translate-x-0" : "-translate-x-full"
          }`}
        >
          <SidebarContent onClose={onClose} />
        </aside>
      </div>
    </>
  );
}
