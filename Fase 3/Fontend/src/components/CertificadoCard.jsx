// src/components/CertificadoCard.jsx
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
import api from "../services/api"; // 👈 para traer el usuario real

export default function CertificadoCard() {
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [approvedCourses, setApprovedCourses] = useState([]);
  const [studentName, setStudentName] = useState("Alumno");

  const normalizePercentageDecimal = (value) => {
    if (typeof value !== "number" || Number.isNaN(value)) return null;
    let p = value;
    if (p > 1) p = p / 100; // 80 -> 0.8
    if (p < 0) p = 0;
    if (p > 1) p = 1;
    return p;
  };

  const getImageForCourse = (title) => {
    const nameLower = (title || "").toLowerCase();
    return nameLower.includes("construc") ? construccionImg : mecanicaImg;
  };

const handleDownload = (courseId, finalGrade) => {
  const userRaw = localStorage.getItem("user");
  const user = JSON.parse(userRaw);
  const studentId = user.userId;

  // Usa la misma base de tu API (si ya usas api.defaults.baseURL, úsalo aquí)
  const baseUrl =
    api?.defaults?.baseURL || process.env.REACT_APP_API_URL || "http://localhost:7037/api";

  const url = `${baseUrl}/courses/${courseId}/certificate?studentId=${studentId}&finalGrade=${encodeURIComponent(
    finalGrade.toFixed(1)
  )}`;

  window.open(url, "_blank");
};



  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        // 1) Usuario logueado (desde localStorage)
        const userRaw = localStorage.getItem("user");
        if (!userRaw) {
          setError("Debes iniciar sesión como alumno para ver tus certificados.");
          setApprovedCourses([]);
          return;
        }

        const user = JSON.parse(userRaw);
        const studentId = user.userId;

        if (!studentId) {
          setError("No se encontró el identificador del alumno.");
          setApprovedCourses([]);
          return;
        }

        if (user.roleId !== 2) {
          setError("Esta sección es solo para alumnos.");
          setApprovedCourses([]);
          return;
        }

        // 2) Traer el usuario completo desde el backend para tener el nombre
        try {
          const { data: userDetail } = await api.get(`/Users/${studentId}`);

          const parts = [
            userDetail.firstName,
            userDetail.middleName,
            userDetail.lastName,
            userDetail.secondLastName,
          ].filter(Boolean);

          const builtName = parts.join(" ").trim();

          if (builtName) {
            setStudentName(builtName);
          } else {
            // Si por alguna razón viene todo vacío, dejamos "Alumno"
            setStudentName("Alumno");
          }
        } catch (err) {
          console.error("Error obteniendo datos del usuario:", err);
          // Si falla, dejamos el nombre por defecto
          setStudentName("Alumno");
        }

        // 3) Cursos donde está inscrito
        const enrollRes = await getEnrolledCoursesByStudent(studentId);
        const enrolledCourseIds = enrollRes.data || [];

        if (!Array.isArray(enrolledCourseIds) || enrolledCourseIds.length === 0) {
          setApprovedCourses([]);
          return;
        }

        // 4) Para cada curso, calculamos la nota final y filtramos aprobados
        const perCoursePromises = enrolledCourseIds.map(async (courseId) => {
          try {
            const [courseRes, evalsRes, lessonsRes] = await Promise.all([
              getCourseById(courseId),
              getEvaluationsByCourse(courseId),
              getLessonsByCourse(courseId),
            ]);

            const course = courseRes.data;
            const evaluations = evalsRes.data || [];
            const lessons = lessonsRes.data || [];

            const courseTitle =
              course.title || course.name || "Curso sin título";

            // ---- NOTAS POR CLASE (promedio 60%) ----
            const lessonGradePromises = lessons.map(async (lesson) => {
              try {
                const { data: attempts } = await getLessonAttempts(
                  courseId,
                  lesson.id,
                  studentId
                );

                if (!Array.isArray(attempts) || attempts.length === 0) {
                  return { grade: null };
                }

                const lastAttempt = attempts[0];

                const { data: evalsLesson } = await getEvaluationsByLesson(
                  courseId,
                  lesson.id
                );

                const correctByQuestion = {};
                (evalsLesson || []).forEach((ev) => {
                  (ev.questions || []).forEach((q) => {
                    const correctIds =
                      (q.options || [])
                        .filter((o) => o.isCorrect)
                        .map((o) => o.id) || [];
                    correctByQuestion[q.id] = correctIds;
                  });
                });

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
                  if (correctIds.length === 0) return;

                  totalQuestions++;
                  if (correctIds.includes(selectedOptId)) {
                    correctCount++;
                  }
                });

                if (totalQuestions === 0) {
                  return { grade: null };
                }

                const porcentajeDecimal = correctCount / totalQuestions;
                const grade = 1 + 6 * porcentajeDecimal;

                return { grade };
              } catch (err) {
                console.error(
                  `Error calculando nota de clase en curso ${courseId}`,
                  err.response?.data || err
                );
                return { grade: null };
              }
            });

            const lessonGrades = await Promise.all(lessonGradePromises);
            const numericLessonGrades = lessonGrades
              .map((lg) => lg.grade)
              .filter((g) => typeof g === "number");

            let lessonsAverageGrade = null;
            if (numericLessonGrades.length > 0) {
              const sum = numericLessonGrades.reduce((acc, g) => acc + g, 0);
              lessonsAverageGrade = sum / numericLessonGrades.length;
            }

            // ---- EXAMEN FINAL (40%) ----
            const finalEval = evaluations.find((ev) => ev.isFinalExam);
            let finalExamGrade = null;

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
                  finalExamGrade = 1 + 6 * porcentajeDecimal;
                }
              } catch (err) {
                if (err.response?.status !== 404) {
                  console.error(
                    `Error obteniendo intento de examen final para curso ${courseId}`,
                    err.response?.data || err
                  );
                }
              }
            }

            // ---- NOTA FINAL DEL CURSO ----
            let finalCourseGrade = null;
            if (lessonsAverageGrade != null && finalExamGrade != null) {
              finalCourseGrade =
                lessonsAverageGrade * 0.6 + finalExamGrade * 0.4;
            } else if (lessonsAverageGrade != null && finalExamGrade == null) {
              finalCourseGrade = lessonsAverageGrade;
            } else if (lessonsAverageGrade == null && finalExamGrade != null) {
              finalCourseGrade = finalExamGrade;
            }

            // Solo cursos aprobados
            if (finalCourseGrade != null && finalCourseGrade >= 4.0) {
              return {
                courseId,
                courseTitle,
                finalCourseGrade,
                imageSrc: getImageForCourse(courseTitle),
              };
            }

            return null;
          } catch (err) {
            console.error(
              `Error procesando curso ${courseId} para certificados`,
              err.response?.data || err
            );
            return null;
          }
        });

        const result = await Promise.all(perCoursePromises);
        const approved = result.filter(Boolean);
        setApprovedCourses(approved);
      } catch (err) {
        console.error(err);
        setError(
          err.response?.data?.message ||
          err.message ||
          "No se pudieron cargar los certificados."
        );
        setApprovedCourses([]);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <p className="text-sm text-gray-600">Cargando certificados...</p>;
  }

  if (error) {
    return <p className="text-sm text-red-500">{error}</p>;
  }

  if (approvedCourses.length === 0) {
    return (
      <p className="text-sm text-gray-600">
        Aún no tienes cursos aprobados para generar certificados.
      </p>
    );
  }

  const displayName = (studentName || "Alumno").toUpperCase();

  return (
    <div className="space-y-6">
      {approvedCourses.map((course) => (
        <div
          key={course.courseId}
          className="border-2 border-duocceleste bg-gray-200 p-6 md:p-8"
        >
          <div className="grid grid-cols-1 lg:grid-cols-[1fr,420px] gap-6 items-center">
            {/* Izquierda: mensaje + botón */}
            <div>
              <h3 className="text-2xl md:text-3xl font-extrabold tracking-tight">
                FELICIDADES, {displayName}
              </h3>
              <p className="mt-3 text-lg font-semibold">
                ¡Comparte tus logros con un certificado!
              </p>
              <p className="mt-2 text-sm text-gray-800">
                Curso aprobado con nota final:{" "}
                <span className="font-bold">
                  {course.finalCourseGrade.toFixed(1)}
                </span>
              </p>

              <button
                onClick={() => handleDownload(course.courseId, course.finalCourseGrade)}
                className="mt-8 inline-flex items-center justify-center rounded bg-duocamarillo
                          text-duocgris px-6 py-3 font-semibold hover:bg-duocceleste hover:text-white transition-colors"
              >
                Descargar
              </button>

            </div>

            {/* Derecha: tarjeta con imagen y título */}
            <div className="bg-white p-5 shadow-md">
              <img
                src={course.imageSrc}
                alt={course.courseTitle}
                className="w-full h-44 object-cover mb-4"
              />
              <h4 className="text-3xl md:text-4xl font-extrabold">
                {course.courseTitle}
              </h4>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}
