// src/components/profesor/CursosProfesor.jsx
import React from "react";
import mecanicaImg from "../../assets/mecanica.png";
import { NavLink } from "react-router-dom";

export default function CursosProfesor({ cursos = [], onDelete }) {
  const baseBtn =
    "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors";

  // 👇 Obtenemos los datos del usuario desde localStorage
  const storedUser = JSON.parse(localStorage.getItem("user"));

  // Si en el futuro guardas fullName en el login, lo tomas de ahí.
  // Por ahora, uso email como fallback si no hay nombre.
  const teacherName =
    storedUser?.fullName || storedUser?.name || storedUser?.email || "Profesor";

  return (
    <div className="flex gap-3 flex-wrap">
      {cursos.map((course) => (
        <div
          key={course.id}
          className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80"
        >
          <img
            src={mecanicaImg}
            alt={`Curso ${course.title}`}
            className="rounded-md mb-3 h-36 w-full object-cover"
          />

          <div>
            <h3 className="text-base font-semibold mb-1">
              {course.title.toUpperCase()}
            </h3>

            {/* 👇 ahora se muestra el nombre del profesor */}
            <p className="text-xs mb-0.5">Profesor: {teacherName}</p>

            {/* Cuando tengas cantidad real de estudiantes, la reemplazas */}
            <p className="text-xs mb-3">Cantidad de estudiantes: —</p>
          </div>

          <div className="mt-4 flex items-center justify-between">
            {/* Eliminar curso */}
            <button
              type="button"
              aria-label="Eliminar curso"
              onClick={() => onDelete?.(course.id)}
              className="inline-flex items-center justify-center w-10 h-10 rounded-md
                 bg-duocamarillo text-duocgris hover:bg-duocceleste hover:text-white
                 transition-colors focus:outline-none focus:ring-2 focus:ring-white/50"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                strokeWidth="2"
                strokeLinecap="round"
                strokeLinejoin="round"
                className="w-6 h-6"
              >
                <path d="M3 6h18" />
                <path d="M8 6V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2" />
                <path d="M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6" />
                <path d="M10 11v6M14 11v6" />
              </svg>
            </button>

            {/* Ir al detalle del curso */}
            <NavLink
              to={`/profesor/detallecurso/${course.id}`}
              className={baseBtn + " inline-flex items-center justify-center"}
            >
              ACCEDER
            </NavLink>
          </div>
        </div>
      ))}
    </div>
  );
}
