import React from 'react';
import userImage from '../assets/DuocOfCourseLogo.png';

export default function Header({ onOpenMenu }) {
  return (
    <header className="sticky top-0 z-40 w-full bg-white border-b-2 border-black">
      <div className="flex items-center justify-between px-4 sm:px-6 py-3">
        {/* Botón hamburguesa (solo mobile) */}
        <button
          type="button"
          onClick={onOpenMenu}
          className="lg:hidden mr-2 inline-flex items-center justify-center w-10 h-10 rounded-md border border-gray-300"
          aria-label="Abrir menú"
        >
          <svg viewBox="0 0 24 24" className="w-6 h-6" fill="none" stroke="currentColor" strokeWidth="2">
            <path d="M3 6h18M3 12h18M3 18h18" />
          </svg>
        </button>

        <h1 className="text-xl font-bold text-gray-800">
          Bienvenido, <span className="text-black">Alumno</span>
        </h1>

        <img src={userImage} alt="OfCourseLogo" className="h-6 sm:h-8" />
      </div>
    </header>
  );
}
