// src/components/ExpCursos.jsx
import React, { useEffect, useState } from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import {
  getAllCoursesWithSchool,
  enrollInCourse,
  getEnrolledCoursesByStudent,
} from "../services/coursesServices";

function ExpCursos() {
  const [courses, setCourses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [currentUser, setCurrentUser] = useState(null);
  const [enrolledCourseIds, setEnrolledCourseIds] = useState([]); // ⬅️ cursos inscritos

  // Leer usuario logueado desde localStorage
  useEffect(() => {
    try {
      const raw = localStorage.getItem("user");
      if (raw) {
        const parsed = JSON.parse(raw);
        setCurrentUser(parsed);
      }
    } catch (err) {
      console.error("Error leyendo usuario desde localStorage:", err);
    }
  }, []);

  // Cargar cursos desde el backend
  useEffect(() => {
    async function fetchCourses() {
      try {
        const res = await getAllCoursesWithSchool();

        const list = Array.isArray(res.data)
          ? res.data
          : res.data?.courses || [];

        setCourses(list);
      } catch (err) {
        console.error("Error obteniendo cursos para estudiante:", err);
      } finally {
        setLoading(false);
      }
    }

    fetchCourses();
  }, []);

  // Cargar cursos donde el alumno ya está inscrito
  useEffect(() => {
    async function fetchEnrollments() {
      if (!currentUser || !currentUser.userId || currentUser.roleId !== 2) {
        return;
      }

      try {
        const res = await getEnrolledCoursesByStudent(currentUser.userId);
        // res.data será algo como [1, 3, 5]
        setEnrolledCourseIds(res.data || []);
      } catch (err) {
        console.error("Error obteniendo inscripciones del alumno:", err);
      }
    }

    fetchEnrollments();
  }, [currentUser]);

  // Agrupar cursos por escuela
  const coursesBySchool = courses.reduce((acc, c) => {
    const schoolName =
      c.schoolName ||
      c.school?.name ||
      c.school?.schoolName ||
      "Sin escuela asignada";

    if (!acc[schoolName]) acc[schoolName] = [];
    acc[schoolName].push(c);
    return acc;
  }, {});

  // Imagen según curso
  const getImageForCourse = (course) => {
    const name = (course.title || course.name || "").toLowerCase();
    if (name.includes("construc")) return construccionImg;
    return mecanicaImg;
  };

  // Nombre profe
  const getTeacherName = (course) =>
    course.teacherName ||
    course.teacher?.fullName ||
    course.teacher ||
    "Profesor no asignado";

  // Cantidad de episodios
  const getEpisodesText = (course) => {
    const count =
      course.episodesCount ??
      course.lessonsCount ??
      course.totalLessons ??
      (Array.isArray(course.lessons) ? course.lessons.length : null);

    if (count == null) return "Cantidad de episodios: N/D";
    return `Cantidad de episodios: ${count}`;
  };

  // ¿Alumno ya inscrito en este curso?
  const isCourseEnrolled = (courseId) =>
    enrolledCourseIds.includes(courseId);

  // Inscribir con usuario logueado
  const handleEnroll = async (courseId) => {
    try {
      if (!currentUser) {
        alert("Debes iniciar sesión para inscribirte en un curso.");
        return;
      }

      if (currentUser.roleId && currentUser.roleId !== 2) {
        alert("Solo los estudiantes pueden inscribirse en cursos.");
        return;
      }

      const studentId = currentUser.userId;

      await enrollInCourse(courseId, studentId);

      // Actualizar estado local para que el botón cambie a "Ya inscrito"
      setEnrolledCourseIds((prev) =>
        prev.includes(courseId) ? prev : [...prev, courseId]
      );

      alert("Te has inscrito en el curso correctamente 😎");
    } catch (err) {
      console.error("Error al inscribirse:", err.response?.data || err);
      alert(
        err.response?.data?.message ||
          "No fue posible inscribirse en el curso."
      );
    }
  };

  if (loading) {
    return <p className="p-8 text-sm text-gray-600">Cargando cursos...</p>;
  }

  return (
    <section className="p-0 flex flex-col gap-8">
      {Object.entries(coursesBySchool).map(([schoolName, cursos]) => (
        <div key={schoolName}>
          <h2 className="text-3xl font-bold mb-4">{schoolName}</h2>

          <div className="flex gap-3 flex-wrap">
            {cursos.map((course) => {
              const enrolled = isCourseEnrolled(course.id);

              return (
                <div
                  key={course.id}
                  className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80"
                >
                  <img
                    src={getImageForCourse(course)}
                    alt={course.title || course.name || "Curso"}
                    className="rounded-md mb-3 h-36 w-full object-cover"
                  />

                  <div>
                    <h3 className="text-base font-semibold mb-1">
                      {(course.title ||
                        course.name ||
                        "Curso sin título"
                      ).toUpperCase()}
                    </h3>
                    <p className="text-xs mb-0.5">
                      Profesor: {getTeacherName(course)}
                    </p>
                    <p className="text-xs mb-3">{getEpisodesText(course)}</p>
                  </div>

                  <div className="flex justify-end items-center h-8">
                    {enrolled ? (
                      <span className="text-xs font-semibold px-3 py-1 rounded bg-green-500/80 text-white">
                        Ya inscrito
                      </span>
                    ) : (
                      <button
                        type="button"
                        className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors"
                        onClick={() => handleEnroll(course.id)}
                      >
                        Inscribir
                      </button>
                    )}
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      ))}
    </section>
  );
}

export default ExpCursos;
