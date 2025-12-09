import React, { useEffect, useState } from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { NavLink } from "react-router-dom";

import {
  getCourseById,
  getEnrolledCoursesByStudent,
  getLessonsByCourse,
} from "../services/coursesServices";
import {
  getEvaluationsByCourse,
  getLastAttemptByStudent,
  getLessonAttempts,
} from "../services/evaluationsService";

function EvaluacionesComp() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [finalEvaluations, setFinalEvaluations] = useState([]);

  const baseBtn =
    "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors";

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        // 1) Usuario logueado
        const userRaw = localStorage.getItem("user");
        if (!userRaw) {
          setError("Debes iniciar sesión como alumno para ver tus evaluaciones.");
          setFinalEvaluations([]);
          return;
        }

        const user = JSON.parse(userRaw);
        const studentId = user.userId;

        if (!studentId) {
          setError("No se encontró el identificador del alumno.");
          setFinalEvaluations([]);
          return;
        }

        // Opcional: solo rol alumno
        if (user.roleId !== 2) {
          setError("Esta sección es solo para alumnos.");
          setFinalEvaluations([]);
          return;
        }

        // 2) Cursos en los que el alumno está inscrito
        const enrollRes = await getEnrolledCoursesByStudent(studentId);
        const enrolledCourseIds = enrollRes.data || [];

        if (!Array.isArray(enrolledCourseIds) || enrolledCourseIds.length === 0) {
          setFinalEvaluations([]);
          return;
        }

        // 3) Para cada curso: curso + lecciones + evaluaciones finales + stats
        const perCoursePromises = enrolledCourseIds.map(async (courseId) => {
          const [courseRes, evalsRes, lessonsRes] = await Promise.all([
            getCourseById(courseId),
            getEvaluationsByCourse(courseId),
            getLessonsByCourse(courseId),
          ]);

          const course = courseRes.data;
          const evaluations = evalsRes.data || [];
          const lessons = lessonsRes.data || [];

          const finals = evaluations.filter((ev) => ev.isFinalExam);
          const courseTitle = course.title || course.name || "Curso sin título";

          // ---- Cálculo de "curso completado" ----
          // Consideramos completada una lección si el alumno tiene al menos 1 intento en esa clase
          const lessonAttemptsPromises = lessons.map(async (lesson) => {
            try {
              const { data: attempts } = await getLessonAttempts(
                courseId,
                lesson.id,
                studentId
              );
              const hasAttempts = Array.isArray(attempts) && attempts.length > 0;
              return { lessonId: lesson.id, done: hasAttempts };
            } catch (err) {
              console.error(
                `Error obteniendo intentos para lesson ${lesson.id} del curso ${courseId}`,
                err.response?.data || err
              );
              return { lessonId: lesson.id, done: false };
            }
          });

          const lessonAttempts = await Promise.all(lessonAttemptsPromises);
          const totalLessons = lessons.length;
          const completedLessons = lessonAttempts.filter((l) => l.done).length;

          // Si no hay lecciones, lo consideramos completado por defecto
          const courseCompleted =
            totalLessons === 0 || completedLessons === totalLessons;

          // ---- Datos por evaluación final (intentos, porcentaje, nota) ----
          const finalsWithStats = await Promise.all(
            finals.map(async (ev) => {
              let attemptsDone = 0;
              let percentage = null; // en %
              let grade = null; // nota 1.0 - 7.0

              try {
                const { data: att } = await getLastAttemptByStudent(
                  courseId,
                  ev.id,
                  studentId
                );

                // Intentos realizados (usamos el campo disponible, o 1 por defecto)
                attemptsDone =
                  att.totalAttempts ??
                  att.attemptsCount ??
                  att.attemptNumber ??
                  1;

                // porcentajeDecimal en rango [0,1]
                let porcentajeDecimal = null;

                if (typeof att.percentage === "number") {
                  // Si el backend ya manda percentage 0–1
                  porcentajeDecimal = att.percentage;
                } else if (
                  typeof att.score === "number" &&
                  typeof att.totalPoints === "number" &&
                  att.totalPoints > 0
                ) {
                  porcentajeDecimal = att.score / att.totalPoints;
                }

                if (porcentajeDecimal != null) {
                  percentage = porcentajeDecimal * 100;
                  // Nota lineal: 0% -> 1.0, 100% -> 7.0
                  grade = 1 + 6 * porcentajeDecimal;
                }
              } catch (err) {
                // Si no hay intento (404), dejamos en 0 / null
                if (err.response?.status === 404) {
                  attemptsDone = 0;
                } else {
                  console.error(
                    `Error obteniendo último intento para eval ${ev.id} del curso ${courseId}`,
                    err.response?.data || err
                  );
                }
              }

              return {
                courseId,
                courseTitle,
                evaluationId: ev.id,
                evaluationTitle: ev.title,
                description: ev.description,
                dueAt: ev.dueAt,
                attemptsDone,
                percentage,
                grade,
                courseCompleted,
              };
            })
          );

          return finalsWithStats;
        });

        const nested = await Promise.all(perCoursePromises);
        const merged = nested.flat(); // [[...],[...]] → [...]

        setFinalEvaluations(merged);
      } catch (err) {
        console.error(err);
        setError(
          err.response?.data?.message ||
            err.message ||
            "No se pudo cargar las evaluaciones."
        );
        setFinalEvaluations([]);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const formatDate = (iso) => {
    if (!iso) return "Sin fecha límite";
    try {
      const d = new Date(iso);
      if (Number.isNaN(d.getTime())) return "Sin fecha límite";
      return d.toLocaleDateString("es-CL");
    } catch {
      return "Sin fecha límite";
    }
  };

  const getImageForCourse = (title) => {
    const nameLower = (title || "").toLowerCase();
    return nameLower.includes("construc") ? construccionImg : mecanicaImg;
  };

  if (loading) {
    return <p className="text-sm text-gray-600">Cargando evaluaciones...</p>;
  }

  if (error) {
    return <p className="text-sm text-red-500">{error}</p>;
  }

  return (
    <section>
      <h2 className="text-3xl font-bold mb-8">Realizar evaluaciones</h2>

      {finalEvaluations.length === 0 ? (
        <p className="text-sm text-gray-600">
          No tienes evaluaciones finales disponibles en tus cursos inscritos.
        </p>
      ) : (
        <div className="flex gap-3 flex-wrap">
          {finalEvaluations.map((item) => {
            const imgSrc = getImageForCourse(item.courseTitle);

            return (
              <div
                key={`${item.courseId}-${item.evaluationId}`}
                className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80"
              >
                <img
                  src={imgSrc}
                  alt={item.courseTitle}
                  className="rounded-md mb-3 h-36 w-full object-cover"
                />
                <div>
                  <h3 className="text-base font-semibold mb-1">
                    {item.courseTitle.toUpperCase()}
                  </h3>
                  <p className="text-xs font-semibold mb-0.5">
                    {item.evaluationTitle}
                  </p>
                  <p className="text-xs mb-0.5">
                    Fecha límite: {formatDate(item.dueAt)}
                  </p>
                  <p className="text-xs">
                    Cantidad de intentos: 1 (no podrás repetirla)
                  </p>

                  {/* Stats del alumno */}
                  <p className="text-xs">
                    Intentos realizados: {item.attemptsDone ?? 0}
                  </p>
                  <p className="text-xs">
                    Porcentaje de aprobación:{" "}
                    {item.percentage != null
                      ? `${item.percentage.toFixed(1)}%`
                      : "—"}
                  </p>
                  <p className="text-xs">
                    Nota:{" "}
                    {item.grade != null ? item.grade.toFixed(1) : "—"}
                  </p>
                  <p className="text-xs mb-3">
                    Estado del curso:{" "}
                    {item.courseCompleted
                      ? "Completado"
                      : "Incompleto (debes completar todas las clases)"}
                  </p>
                </div>

                <div className="flex justify-end">
                  {item.courseCompleted ? (
                    <NavLink
                      to={`/app/evaluacion/${item.courseId}/${item.evaluationId}`}
                      className={baseBtn}
                    >
                      ACCEDER
                    </NavLink>
                  ) : (
                    <button
                      type="button"
                      disabled
                      className={
                        baseBtn + " opacity-60 cursor-not-allowed"
                      }
                      title="Debes completar todas las clases del curso para rendir la evaluación final."
                    >
                      COMPLETA EL CURSO
                    </button>
                  )}
                </div>
              </div>
            );
          })}
        </div>
      )}
    </section>
  );
}

export default EvaluacionesComp;
