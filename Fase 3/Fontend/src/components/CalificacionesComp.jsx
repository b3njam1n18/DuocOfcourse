// src/components/CalificacionesComp.jsx
import React, { useEffect, useState } from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";

import {
  getCourseById,
  getEnrolledCoursesByStudent,
  getLessonsByCourse,
} from "../services/coursesServices";
import {
  getEvaluationsByCourse,
  getEvaluationsByLesson,
  getLessonAttempts,
  getAttemptResult,
  getLastAttemptByStudent,
} from "../services/evaluationsService";

function CalificacionesComp() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [gradesByCourse, setGradesByCourse] = useState([]);

  // helper: normalizar porcentaje que pueda venir 0–1 o 0–100
  const normalizePercentageDecimal = (value) => {
    if (typeof value !== "number" || Number.isNaN(value)) return null;
    let p = value;
    if (p > 1) p = p / 100; // si viene 80 => 0.8
    if (p < 0) p = 0;
    if (p > 1) p = 1;
    return p;
  };

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        // 1) Usuario logueado
        const userRaw = localStorage.getItem("user");
        if (!userRaw) {
          setError("Debes iniciar sesión como alumno para ver tus calificaciones.");
          setGradesByCourse([]);
          return;
        }

        const user = JSON.parse(userRaw);
        const studentId = user.userId;
        if (!studentId) {
          setError("No se encontró el identificador del alumno.");
          setGradesByCourse([]);
          return;
        }

        if (user.roleId !== 2) {
          setError("Esta sección es solo para alumnos.");
          setGradesByCourse([]);
          return;
        }

        // 2) Cursos en los que el alumno está inscrito
        const enrollRes = await getEnrolledCoursesByStudent(studentId);
        const enrolledCourseIds = enrollRes.data || [];

        if (!Array.isArray(enrolledCourseIds) || enrolledCourseIds.length === 0) {
          setGradesByCourse([]);
          return;
        }

        // 3) Para cada curso, calcular notas
        const perCoursePromises = enrolledCourseIds.map(async (courseId) => {
          const [courseRes, evalsRes, lessonsRes] = await Promise.all([
            getCourseById(courseId),
            getEvaluationsByCourse(courseId),
            getLessonsByCourse(courseId),
          ]);

          const course = courseRes.data;
          const evaluations = evalsRes.data || [];
          const lessons = lessonsRes.data || [];

          const courseTitle = course.title || course.name || "Curso sin título";

          // ---- NOTAS POR CLASE (quizzes) ----
          const lessonGradePromises = lessons.map(async (lesson) => {
            const lessonTitle =
              lesson.title || lesson.name || "Clase sin título";

            try {
              // 1) Intentos de la clase
              const { data: attempts } = await getLessonAttempts(
                courseId,
                lesson.id,
                studentId
              );

              if (!Array.isArray(attempts) || attempts.length === 0) {
                // SIN intentos: solo mostramos "-"
                return {
                  lessonId: lesson.id,
                  lessonTitle,
                  hasAttempt: false,
                  percentage: null,
                  grade: null,
                };
              }

              const lastAttempt = attempts[0];

              // 2) Definición de evaluaciones de la clase (para saber qué opción es correcta)
              const { data: evalsLesson } = await getEvaluationsByLesson(
                courseId,
                lesson.id
              );

              const correctByQuestion = {};
              (evalsLesson || []).forEach((ev) => {
                (ev.questions || []).forEach((q) => {
                  const correctOptIds =
                    (q.options || [])
                      .filter((o) => o.isCorrect)
                      .map((o) => o.id) || [];
                  correctByQuestion[q.id] = correctOptIds;
                });
              });

              // 3) Detalle del intento (qué opciones marcó)
              const { data: attemptRes } = await getAttemptResult(
                courseId,
                lastAttempt.attemptId
              );

              let totalQuestions = 0;
              let correctCount = 0;

              (attemptRes.questions || []).forEach((qRes) => {
                const questionId = qRes.questionId ?? qRes.id;
                const selectedOptId =
                  qRes.selectedOptionId ??
                  qRes.optionId ??
                  qRes.chosenOptionId ??
                  null;

                if (!questionId || !selectedOptId) return;

                const correctIds = correctByQuestion[questionId] || [];
                if (correctIds.length === 0) return; // por si la pregunta no tiene correctas definidas

                totalQuestions++;
                if (correctIds.includes(selectedOptId)) {
                  correctCount++;
                }
              });

              if (totalQuestions === 0) {
                // Tiene intento pero no pudimos calcular nota
                return {
                  lessonId: lesson.id,
                  lessonTitle,
                  hasAttempt: true,
                  percentage: null,
                  grade: null,
                };
              }

              const porcentajeDecimal = correctCount / totalQuestions;
              const percentage = porcentajeDecimal * 100;
              const grade = 1 + 6 * porcentajeDecimal; // 0%->1.0, 100%->7.0

              return {
                lessonId: lesson.id,
                lessonTitle,
                hasAttempt: true,
                percentage,
                grade,
              };
            } catch (err) {
              console.error(
                `Error obteniendo nota de la clase ${lesson.id} del curso ${courseId}`,
                err.response?.data || err
              );
              return {
                lessonId: lesson.id,
                lessonTitle,
                hasAttempt: false,
                percentage: null,
                grade: null,
              };
            }
          });

          const lessonGrades = await Promise.all(lessonGradePromises);

          // Promedio SOLO de clases que tienen nota calculada
          const numericLessonGrades = lessonGrades
            .map((lg) => lg.grade)
            .filter((g) => typeof g === "number");

          let lessonsAverageGrade = null;
          if (numericLessonGrades.length > 0) {
            const sum = numericLessonGrades.reduce((acc, g) => acc + g, 0);
            lessonsAverageGrade = sum / numericLessonGrades.length;
          }

          // ---- NOTA EVALUACIÓN FINAL ----
          const finalEval = evaluations.find((ev) => ev.isFinalExam);
          let finalExamGrade = null;
          let finalExamPercentage = null;

          if (finalEval) {
            try {
              const { data: att } = await getLastAttemptByStudent(
                courseId,
                finalEval.id,
                studentId
              );

              let porcentajeDecimal = null;

              if (typeof att.percentage === "number") {
                porcentajeDecimal = normalizePercentageDecimal(att.percentage);
              } else if (
                typeof att.score === "number" &&
                typeof att.totalPoints === "number" &&
                att.totalPoints > 0
              ) {
                porcentajeDecimal = att.score / att.totalPoints;
              }

              if (porcentajeDecimal != null) {
                finalExamPercentage = porcentajeDecimal * 100;
                finalExamGrade = 1 + 6 * porcentajeDecimal;
              }
            } catch (err) {
              if (err.response?.status !== 404) {
                console.error(
                  `Error obteniendo último intento de evaluación final para curso ${courseId}`,
                  err.response?.data || err
                );
              }
            }
          }

          // ---- NOTA FINAL DEL CURSO (60% clases, 40% final) ----
          let finalCourseGrade = null;

          if (lessonsAverageGrade != null && finalExamGrade != null) {
            finalCourseGrade = lessonsAverageGrade * 0.6 + finalExamGrade * 0.4;
          } else if (lessonsAverageGrade != null && finalExamGrade == null) {
            finalCourseGrade = lessonsAverageGrade;
          } else if (lessonsAverageGrade == null && finalExamGrade != null) {
            finalCourseGrade = finalExamGrade;
          }

          let status = "EN CURSO";
          if (finalCourseGrade != null) {
            status = finalCourseGrade >= 4.0 ? "APROBADO" : "REPROBADO";
          }

          return {
            courseId,
            courseTitle,
            lessonGrades, // detalle por clase
            lessonsAverageGrade,
            finalExamGrade,
            finalExamPercentage,
            finalCourseGrade,
            status,
          };
        });

        const result = await Promise.all(perCoursePromises);
        setGradesByCourse(result);
      } catch (err) {
        console.error(err);
        setError(
          err.response?.data?.message ||
            err.message ||
            "No se pudieron cargar las calificaciones."
        );
        setGradesByCourse([]);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const getImageForCourse = (title) => {
    const nameLower = (title || "").toLowerCase();
    return nameLower.includes("construc") ? construccionImg : mecanicaImg;
  };

  if (loading) {
    return <p className="text-sm text-gray-600">Cargando calificaciones...</p>;
  }

  if (error) {
    return <p className="text-sm text-red-500">{error}</p>;
  }

  return (
    <section>
      <h2 className="text-3xl font-bold mb-2">Ver calificaciones</h2>
      <h2 className="text-xl font-semibold mb-8">
        Escuela de Ingeniería y Recursos Naturales
      </h2>

      {gradesByCourse.length === 0 ? (
        <p className="text-sm text-gray-600">
          No tienes calificaciones disponibles aún.
        </p>
      ) : (
        <div className="flex gap-3 flex-wrap">
          {gradesByCourse.map((course) => {
            const imgSrc = getImageForCourse(course.courseTitle);

            return (
              <div
                key={course.courseId}
                className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80"
              >
                <img
                  src={imgSrc}
                  alt={course.courseTitle}
                  className="rounded-md mb-3 h-36 w-full object-cover"
                />
                <div>
                  <h3 className="text-base font-semibold mb-2">
                    {course.courseTitle.toUpperCase()}
                  </h3>

                  {/* Detalle de notas de curso (60%) */}
                  <p className="text-xs font-semibold mb-1">
                    Notas curso (60%):
                  </p>
                  {course.lessonGrades && course.lessonGrades.length > 0 ? (
                    course.lessonGrades.map((lg, idx) => {
                      let labelNota = "-";

                      if (lg.hasAttempt && typeof lg.grade === "number") {
                        labelNota = lg.grade.toFixed(1);
                      } else {
                        // sin intento o sin nota calculable → "-"
                        labelNota = "-";
                      }

                      return (
                        <p key={lg.lessonId} className="text-xs">
                          Clase {idx + 1}: {labelNota} ({lg.lessonTitle})
                        </p>
                      );
                    })
                  ) : (
                    <p className="text-xs">Sin clases evaluadas.</p>
                  )}

                  <p className="text-xs mt-1 mb-2">
                    Promedio clases (60%):{" "}
                    {course.lessonsAverageGrade != null
                      ? course.lessonsAverageGrade.toFixed(1)
                      : "—"}
                  </p>

                  {/* Examen (40%) */}
                  <p className="text-xs font-semibold mb-1">Examen (40%):</p>
                  {course.finalExamGrade != null ? (
                    <p className="text-xs mb-2">
                      Nota evaluación final: {course.finalExamGrade.toFixed(1)}
                      {course.finalExamPercentage != null &&
                        ` (${course.finalExamPercentage.toFixed(1)}%)`}
                    </p>
                  ) : (
                    <p className="text-xs mb-2">
                      Aún no rindes la evaluación final.
                    </p>
                  )}

                  {/* Nota final y estado */}
                  <p className="text-xs mb-0.5">
                    Nota Final:{" "}
                    {course.finalCourseGrade != null
                      ? course.finalCourseGrade.toFixed(1)
                      : "—"}
                  </p>

                  <p className="text-xs mb-1">
                    Estado: {course.status}
                  </p>
                </div>
              </div>
            );
          })}
        </div>
      )}
    </section>
  );
}

export default CalificacionesComp;
