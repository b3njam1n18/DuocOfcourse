// src/components/Cursos.jsx
import React from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { NavLink } from "react-router-dom";

function Cursos({ course }) {
  const baseBtn =
    "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors";

  if (!course) return null;

  const title = (course.title || course.name || "Curso sin título").toUpperCase();
  const teacherName =
    course.teacherName ||
    course.teacher?.fullName ||
    course.teacher ||
    "Profesor no asignado";

  const lessonsCount =
    course.episodesCount ??
    course.lessonsCount ??
    course.totalLessons ??
    (Array.isArray(course.lessons) ? course.lessons.length : null);

  const episodesText =
    lessonsCount == null
      ? "Cantidad de episodios: N/D"
      : `Cantidad de episodios: ${lessonsCount}`;

  const nameLower = (course.title || course.name || "").toLowerCase();
  const imageSrc = nameLower.includes("construc")
    ? construccionImg
    : mecanicaImg;

  return (
    <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
      <img
        src={imageSrc}
        alt={title}
        className="rounded-md mb-3 h-36 w-full object-cover"
      />
      <div>
        <h3 className="text-base font-semibold mb-1">{title}</h3>
        <p className="text-xs mb-0.5">Profesor: {teacherName}</p>
        <p className="text-xs mb-3">{episodesText}</p>
      </div>
      <div className="flex justify-end">
        {/* 👉 pasamos el ID del curso en la URL */}
        <NavLink to={`/app/DetalleCurso/${course.id}`} className={baseBtn}>
          ACCEDER
        </NavLink>
      </div>
    </div>
  );
}

export default Cursos;
