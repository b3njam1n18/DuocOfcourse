import React from "react";
import mecanicaImg from "../../assets/mecanica.png";
import { NavLink } from "react-router-dom";

function Cursos() {
  const baseBtn = "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors"
  return (

    <div className="flex gap-3 flex-wrap">
      <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
        <img
          src={mecanicaImg}
          alt="Curso de Mecánica"
          className="rounded-md mb-3 h-36 w-full object-cover"
        />
        <div>
          <h3 className="text-base font-semibold mb-1">MECÁNICA BÁSICA</h3>
          <p className="text-xs mb-0.5">Profesor: Manuel Gallegos</p>
          <p className="text-xs mb-3">Cantidad de estudiantes: 10</p>
        </div>
        {/* Fila de acciones: basurero (izq) + acceder (der) */}
        <div className="mt-4 flex items-center justify-between">
          {/* Botón basurero duocamarillo */}
          <button
            type="button"
            aria-label="Eliminar curso"
            onClick={() => console.log("eliminar curso")}
            className="inline-flex items-center justify-center w-10 h-10 rounded-md
               bg-duocamarillo text-duocgris hover:bg-duocceleste hover:text-white
               transition-colors focus:outline-none focus:ring-2 focus:ring-white/50"
          >
            {/* Icono trash (SVG) usa currentColor */}
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24"
              fill="none" stroke="currentColor" strokeWidth="2"
              strokeLinecap="round" strokeLinejoin="round"
              className="w-6 h-6">
              <path d="M3 6h18" />
              <path d="M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
              <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6" />
              <path d="M10 11v6M14 11v6" />
            </svg>
          </button>

          {/* Botón ACCEDER (usa tu baseBtn) */}
          <NavLink
            to="/profesor/DetalleCurso"
            className={baseBtn + " inline-flex items-center justify-center"}
          >
            ACCEDER
          </NavLink>
        </div>

      </div>


    </div>

  );
}

export default Cursos;