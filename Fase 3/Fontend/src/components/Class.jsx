import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import ProgressCircle from "../components/Progreso";
import BotoneraVertical from "../components/Botonera";

import { getLesson } from "../services/lessonsService";
import {
  getEvaluationsByLesson,
  submitLessonAttempts,
  getLessonAttempts,
  getAttemptResult,
} from "../services/evaluationsService";

function Class() {
  const { courseId, lessonId } = useParams();
  const courseIdNum = Number(courseId);
  const lessonIdNum = Number(lessonId);

  const [lesson, setLesson] = useState(null);
  const [quizzes, setQuizzes] = useState([]);
  const [answersByEvaluation, setAnswersByEvaluation] = useState({});
  const [correctOptionIdsByEval, setCorrectOptionIdsByEval] = useState({});
  const [progress, setProgress] = useState(0);
  const [currentUser, setCurrentUser] = useState(null);
  const [sending, setSending] = useState(false);
  const [submitted, setSubmitted] = useState(false); // ⬅️ ya respondió

  // Leer usuario logueado
  useEffect(() => {
    try {
      const raw = localStorage.getItem("user");
      if (raw) {
        setCurrentUser(JSON.parse(raw));
      }
    } catch (err) {
      console.error("Error leyendo user desde localStorage:", err);
    }
  }, []);

  // Cargar datos de la lección
  useEffect(() => {
    if (!courseIdNum || !lessonIdNum) return;

    const loadLesson = async () => {
      try {
        const { data } = await getLesson(courseIdNum, lessonIdNum);
        setLesson(data);
      } catch (err) {
        console.error("Error cargando lección:", err);
      }
    };

    loadLesson();
  }, [courseIdNum, lessonIdNum]);

  // Cargar quizzes (evaluaciones) de la clase
  useEffect(() => {
    if (!courseIdNum || !lessonIdNum) return;

    const loadQuizzes = async () => {
      try {
        const { data: evals } = await getEvaluationsByLesson(
          courseIdNum,
          lessonIdNum
        );

        const correctMap = {};
        const transform = (evals || []).map((ev) => {
          const firstQuestion =
            ev.questions && ev.questions.length > 0 ? ev.questions[0] : null;

          const opciones =
            (firstQuestion?.options || []).map((o) => ({
              id: o.id,
              texto: o.text,
            })) || [];

          const correctIds =
            (firstQuestion?.options || [])
              .filter((o) => o.isCorrect)
              .map((o) => o.id) || [];

          correctMap[ev.id] = correctIds;

          return {
            evaluationId: ev.id,
            questionId: firstQuestion ? firstQuestion.id : null,
            pregunta: firstQuestion ? firstQuestion.prompt : "(Sin pregunta)",
            opciones,
          };
        });

        setCorrectOptionIdsByEval(correctMap);
        setQuizzes(transform);
      } catch (err) {
        console.error("Error cargando quizzes de la clase:", err);
      }
    };

    loadQuizzes();
  }, [courseIdNum, lessonIdNum]);

  // Revisar si el alumno YA respondió esta clase
  // y, si es así, traer lo que marcó en su último intento
  useEffect(() => {
    if (
      !currentUser ||
      !courseIdNum ||
      !lessonIdNum ||
      quizzes.length === 0
    )
      return;

    const checkAttempts = async () => {
      try {
        // 1) Resumen de intentos
        const { data: attempts } = await getLessonAttempts(
          courseIdNum,
          lessonIdNum,
          currentUser.userId
        );

        if (!Array.isArray(attempts) || attempts.length === 0) {
          return; // nunca ha respondido
        }

        setSubmitted(true);

        // Tomamos el último intento (ya viene ordenado desc)
        const lastAttempt = attempts[0];

        // 2) Detalle del intento (preguntas + opción marcada)
        const { data: attemptResult } = await getAttemptResult(
          courseIdNum,
          lastAttempt.attemptId
        );

        const map = {};

        (attemptResult.questions || []).forEach((qRes) => {
          const questionId = qRes.questionId ?? qRes.id;

          // nombre defensivo por si el DTO tiene otro naming
          const selectedOptId =
            qRes.selectedOptionId ??
            qRes.optionId ??
            qRes.chosenOptionId ??
            null;

          if (!selectedOptId) return;

          const quiz = quizzes.find((q) => q.questionId === questionId);
          if (!quiz) return;

          map[quiz.evaluationId] = selectedOptId;
        });

        setAnswersByEvaluation(map);
      } catch (err) {
        console.error("Error revisando intentos de la lección:", err);
      }
    };

    checkAttempts();
  }, [currentUser, courseIdNum, lessonIdNum, quizzes]);

  // Actualizar progreso según cantidad de preguntas respondidas
  useEffect(() => {
    const total = quizzes.length;
    if (!total) {
      setProgress(0);
      return;
    }

    const answered = quizzes.filter(
      (q) => answersByEvaluation[q.evaluationId]
    ).length;

    const pct = Math.round((answered / total) * 100);
    setProgress(pct);
  }, [answersByEvaluation, quizzes]);

  // Seleccionar opción para una evaluación
  const handleOptionSelect = (evaluationId, optionIdOrNull) => {
    if (submitted) return; // si ya envió, no dejamos cambiar nada

    setAnswersByEvaluation((prev) => ({
      ...prev,
      [evaluationId]: optionIdOrNull,
    }));
  };

  // Enviar resultados al backend
  const handleSubmitResults = async () => {
    if (!currentUser) {
      alert("Debes iniciar sesión para enviar tus resultados.");
      return;
    }

    if (!quizzes.length) {
      alert("Esta clase no tiene quizzes configurados.");
      return;
    }

    const studentId = currentUser.userId;

    const quizzesPayload = quizzes.map((q) => {
      const selectedOptionId = answersByEvaluation[q.evaluationId] || null;

      return {
        evaluationId: q.evaluationId,
        answers: [
          {
            questionId: q.questionId,
            optionId: selectedOptionId,
            openText: null,
          },
        ],
      };
    });

    const body = {
      studentId,
      quizzes: quizzesPayload,
    };

    try {
      setSending(true);
      const { data } = await submitLessonAttempts(
        courseIdNum,
        lessonIdNum,
        body
      );

      console.log("Resultados guardados:", data);
      setSubmitted(true);
      alert("Respuestas enviadas correctamente ✅");
    } catch (err) {
      console.error(
        "Error enviando resultados:",
        err.response?.data || err
      );
      alert(
        err.response?.data?.message ||
          "No se pudieron enviar los resultados."
      );
    } finally {
      setSending(false);
    }
  };

  const title = lesson?.title || lesson?.name || "Clase sin título";
  const teacherName = lesson?.teacherName || "Profesor/a";

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-2">{title}</h2>
      <p className="text-sm text-gray-600 mb-2">{teacherName}</p>

      {submitted && (
        <p className="text-sm text-emerald-600 mb-4">
          Ya enviaste tus respuestas para esta clase. Solo puedes revisarlas.
        </p>
      )}

      <div className="grid grid-cols-1 lg:grid-cols-[1fr,280px] gap-6">
        {/* Columna principal */}
        <div className="flex flex-col gap-6">
          {quizzes.length === 0 && (
            <p className="text-sm text-gray-600">
              Esta clase aún no tiene quizzes configurados.
            </p>
          )}

          {quizzes.map((quiz, index) => {
            const selectedOptionId = answersByEvaluation[quiz.evaluationId];
            const selectedIndex = quiz.opciones.findIndex(
              (o) => o.id === selectedOptionId
            );

            const correctIds = correctOptionIdsByEval[quiz.evaluationId] || [];
            const correctIndexes = quiz.opciones
              .map((o, idx) => (correctIds.includes(o.id) ? idx : -1))
              .filter((idx) => idx !== -1);

            return (
              <div
                key={quiz.evaluationId}
                className="bg-white rounded-md border p-4 shadow-sm"
              >
                <p className="font-bold mb-2">
                  {index + 1}. {quiz.pregunta}
                </p>

                <BotoneraVertical
                  opciones={quiz.opciones.map((o) => o.texto)}
                  permitirDesmarcar
                  disabled={submitted}
                  selectedIndex={selectedIndex >= 0 ? selectedIndex : undefined}
                  correctIndexes={correctIndexes}
                  onSelect={(idx) => {
                    const opt = quiz.opciones[idx];
                    handleOptionSelect(
                      quiz.evaluationId,
                      opt ? opt.id : null
                    );
                  }}
                />
              </div>
            );
          })}
        </div>

        {/* Aside con progreso */}
        <aside className="h-fit lg:sticky lg:top-20 bg-white rounded-md border p-4">
          <p className="text-xl font-semibold mb-3">
            Progreso: {progress}%
          </p>
          <ProgressCircle value={progress} size={120} stroke={12} />

          <button
            type="button"
            disabled={sending || submitted}
            onClick={handleSubmitResults}
            className={`w-full mt-4 rounded bg-duocamarillo text-duocgris font-bold py-2 hover:bg-duocazul hover:text-white transition-colors ${
              sending || submitted ? "opacity-70 cursor-not-allowed" : ""
            }`}
          >
            {submitted
              ? "Resultados enviados"
              : sending
              ? "Enviando..."
              : "Enviar resultados"}
          </button>
        </aside>
      </div>
    </section>
  );
}

export default Class;
