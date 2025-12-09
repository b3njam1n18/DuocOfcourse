// src/components/DetCurso.jsx
import React, { useEffect, useState } from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { getCourseById, getLessonsByCourse } from "../services/coursesServices";
import {
  getEvaluationsByCourse,
  getEvaluationsByLesson,
  getLessonAttempts,
  getAttemptResult,
} from "../services/evaluationsService";

import { NavLink, useParams } from "react-router-dom";

export default function DetCurso() {
  const { courseId } = useParams();
  const courseIdNum = Number(courseId);

  const [course, setCourse] = useState(null);
  const [lessons, setLessons] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const [finalExam, setFinalExam] = useState(null);

  // notas por clase: { [lessonId]: { hasAttempt, grade } }
  const [lessonGrades, setLessonGrades] = useState({});
  // promedio de clases (60% de la nota del curso)
  const [lessonsAverageGrade, setLessonsAverageGrade] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        if (!courseIdNum || Number.isNaN(courseIdNum)) {
          throw new Error("Curso inválido.");
        }

        const [courseRes, lessonsRes, evalsRes] = await Promise.all([
          getCourseById(courseIdNum),
          getLessonsByCourse(courseIdNum),
          getEvaluationsByCourse(courseIdNum),
        ]);

        setCourse(courseRes.data);
        const lessonsData = lessonsRes.data || [];
        setLessons(lessonsData);

        const evalsData = evalsRes.data || [];
        const final = evalsData.find((e) => e.isFinalExam);
        setFinalExam(final || null);

        // ------- Cálculo de notas solo si es alumno logueado -------
        const userRaw = localStorage.getItem("user");
        if (!userRaw) {
          setLessonGrades({});
          setLessonsAverageGrade(null);
          return;
        }

        const user = JSON.parse(userRaw);
        const studentId = user.userId;
        if (!studentId || user.roleId !== 2) {
          setLessonGrades({});
          setLessonsAverageGrade(null);
          return;
        }

        // ---- NOTAS POR CLASE (quizzes) ----
        const lessonGradePromises = lessonsData.map(async (lesson) => {
          const lessonTitle =
            lesson.title || lesson.name || "Clase sin título";

          try {
            const { data: attempts } = await getLessonAttempts(
              courseIdNum,
              lesson.id,
              studentId
            );

            if (!Array.isArray(attempts) || attempts.length === 0) {
              // sin intento: no mostramos nota aún
              return {
                lessonId: lesson.id,
                lessonTitle,
                hasAttempt: false,
                grade: null,
              };
            }

            const lastAttempt = attempts[0];

            // evaluaciones de la lección → para saber qué opción es correcta
            const { data: evalsLesson } = await getEvaluationsByLesson(
              courseIdNum,
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

            // detalle del intento (qué marcó)
            const { data: attemptRes } = await getAttemptResult(
              courseIdNum,
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
              return {
                lessonId: lesson.id,
                lessonTitle,
                hasAttempt: true,
                grade: null,
              };
            }

            const porcentajeDecimal = correctCount / totalQuestions;
            const grade = 1 + 6 * porcentajeDecimal;

            return {
              lessonId: lesson.id,
              lessonTitle,
              hasAttempt: true,
              grade,
            };
          } catch (err) {
            console.error(
              `Error obteniendo nota de la clase ${lesson.id} del curso ${courseIdNum}`,
              err.response?.data || err
            );
            return {
              lessonId: lesson.id,
              lessonTitle,
              hasAttempt: false,
              grade: null,
            };
          }
        });

        const lessonGradesArr = await Promise.all(lessonGradePromises);

        const mapGrades = {};
        const numericLessonGrades = [];

        lessonGradesArr.forEach((lg) => {
          mapGrades[lg.lessonId] = lg;
          if (typeof lg.grade === "number") {
            numericLessonGrades.push(lg.grade);
          }
        });

        setLessonGrades(mapGrades);

        // promedio de clases (60% de la nota del curso)
        let avgLessons = null;
        if (numericLessonGrades.length > 0) {
          const sum = numericLessonGrades.reduce((acc, g) => acc + g, 0);
          avgLessons = sum / numericLessonGrades.length;
        }
        setLessonsAverageGrade(avgLessons);
      } catch (err) {
        console.error(err);
        setError("No se pudo cargar el curso");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [courseIdNum]);

  if (loading) {
    return (
      <p className="text-sm text-gray-600">
        Cargando información del curso...
      </p>
    );
  }

  if (error) {
    return <p className="text-sm text-red-500">{error}</p>;
  }

  if (!course) {
    return (
      <p className="text-sm text-gray-600">
        No se encontró información del curso.
      </p>
    );
  }

  const title = (course.title || course.name || "Curso sin título").toUpperCase();
  const teacherName =
    course.teacherFullName ||
    course.teacherName ||
    course.teacher?.fullName ||
    "Profesor no asignado";

  const nameLower = (course.title || course.name || "").toLowerCase();
  const imageSrc = nameLower.includes("construc")
    ? construccionImg
    : mecanicaImg;

  // color del promedio de clases
  const gradeColor =
    lessonsAverageGrade == null
      ? "text-gray-700"
      : lessonsAverageGrade >= 4.0
      ? "text-green-600"
      : "text-red-500";

  return (
    <div className="max-w-5xl mx-auto space-y-6">
      {/* Header del curso */}
      <div className="bg-white rounded-xl shadow p-6 flex flex-col md:flex-row gap-6">
        <div className="md:w-1/3">
          <img
            src={imageSrc}
            alt={title}
            className="rounded-lg w-full h-48 object-cover"
          />
        </div>

        <div className="md:w-2/3">
          <h1 className="text-2xl font-bold mb-2">{title}</h1>
          <p className="text-sm text-gray-600 mb-1">
            Profesor: <span className="font-semibold">{teacherName}</span>
          </p>

          {course.description && (
            <p className="text-sm text-gray-700 mt-3">{course.description}</p>
          )}

          <p className="text-sm mt-2">
            Promedio de clases (60%):{" "}
            <span className={`font-semibold ${gradeColor}`}>
              {lessonsAverageGrade != null
                ? lessonsAverageGrade.toFixed(1)
                : "—"}
            </span>
          </p>
            {finalExam && (
          <div className="mt-6 bg-white border rounded-lg p-4 shadow-sm">
            <h3 className="text-lg font-semibold mb-1">
              Evaluación final del curso
            </h3>
            <p className="text-sm text-gray-600 mb-3">
              {finalExam.description ||
                "Rinde la evaluación final para cerrar el curso."}
            </p>

            <NavLink
              to={`/app/evaluacion/${courseId}/${finalExam.id}`}
              className="inline-flex items-center bg-duocazul text-white text-sm px-4 py-2 rounded hover:bg-duocceleste"
            >
              Rendir evaluación final
            </NavLink>
          </div>
        )}
        </div>
      </div>

      {/* Lista de lecciones */}
      <div className="bg-white rounded-xl shadow p-6">
        <h2 className="text-xl font-semibold mb-4">Contenido del curso</h2>

        {lessons.length === 0 ? (
          <p className="text-sm text-gray-600">
            Este curso aún no tiene clases publicadas.
          </p>
        ) : (
          <div className="flex flex-col gap-3">
            {lessons.map((lesson) => {
              const lg = lessonGrades[lesson.id];

              let labelNotaClase = "-";
              if (lg && lg.hasAttempt && typeof lg.grade === "number") {
                labelNotaClase = lg.grade.toFixed(1);
              }

              return (
                <div
                  key={lesson.id}
                  className="w-full bg-duocazul text-white p-3 rounded-md shadow-md flex items-center justify-between"
                >
                  <div>
                    <h3 className="font-semibold text-sm">
                      {lesson.position}. {lesson.title}
                    </h3>
                    <p className="text-xs mt-1">
                      Duración: {lesson.durationMinutes ?? 0} min
                    </p>
                    <p className="text-xs mt-1">
                      Nota de la clase: {labelNotaClase}
                    </p>
                  </div>

                  <NavLink
                    to={`/app/clase/${courseId}/${lesson.id}`}
                    className="bg-duocamarillo text-duocgris text-xs px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white"
                  >
                    Ver clase
                  </NavLink>
                </div>
              );
            })}
          </div>
        )}

      
      </div>
    </div>
  );
}

