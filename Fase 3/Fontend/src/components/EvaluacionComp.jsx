// src/components/EvaluacionComp.jsx
import React, { useEffect, useState } from "react";
import { useParams, useLocation } from "react-router-dom";

import {
  getEvaluationFull,
  createAttempt,
  submitAnswers,
  finishAttempt,
  getAttemptResult,
} from "../services/evaluationsService";

const FINAL_EXAM_ATTEMPTS_KEY = "finalExamAttempts";

// 🔹 Ahora la clave incluye evaluationId + studentId
function loadStoredAttempt(evaluationId, studentId) {
  try {
    const raw = localStorage.getItem(FINAL_EXAM_ATTEMPTS_KEY);
    if (!raw) return null;

    const parsed = JSON.parse(raw);
    if (!parsed || typeof parsed !== "object") return null;

    const key = `${evaluationId}_${studentId}`;
    return parsed[key] || null;
  } catch (err) {
    console.error("Error leyendo intentos de examen desde localStorage:", err);
    return null;
  }
}

function saveStoredAttempt(evaluationId, courseId, attemptId, studentId) {
  try {
    const raw = localStorage.getItem(FINAL_EXAM_ATTEMPTS_KEY);
    let parsed = {};
    if (raw) {
      parsed = JSON.parse(raw) || {};
    }

    const key = `${evaluationId}_${studentId}`;
    parsed[key] = { courseId, attemptId };

    localStorage.setItem(FINAL_EXAM_ATTEMPTS_KEY, JSON.stringify(parsed));
  } catch (err) {
    console.error("Error guardando intento de examen en localStorage:", err);
  }
}

export default function EvaluacionComp() {
  // Puede venir por params o por state (nos interesa sobre todo state)
  const params = useParams();
  const location = useLocation();
  const state = location.state || {};

  const rawCourseId = params.courseId ?? state.courseId;
  const rawEvaluationId = params.evaluationId ?? state.evaluationId;

  const courseIdNum = rawCourseId ? Number(rawCourseId) : NaN;
  const evaluationIdNum = rawEvaluationId ? Number(rawEvaluationId) : NaN;

  const [evaluation, setEvaluation] = useState(null);
  const [attemptId, setAttemptId] = useState(null);
  const [answers, setAnswers] = useState({}); // { [questionId]: { optionId, openText } }
  const [loading, setLoading] = useState(true);
  const [sending, setSending] = useState(false);
  const [submitted, setSubmitted] = useState(false);
  const [result, setResult] = useState(null);
  const [error, setError] = useState(null);

  const hasValidIds =
    !Number.isNaN(courseIdNum) &&
    !Number.isNaN(evaluationIdNum) &&
    courseIdNum > 0 &&
    evaluationIdNum > 0;

  // Cargar evaluación y revisar si ya existe un intento finalizado
  useEffect(() => {
    const init = async () => {
      try {
        setLoading(true);
        setError(null);

        // Si no hay IDs válidos, mostramos error amigable
        if (!hasValidIds) {
          setError(
            "No se pudo identificar el curso o la evaluación. Intenta ingresar desde la lista de evaluaciones."
          );
          return;
        }

        // Usuario logueado
        const userRaw = localStorage.getItem("user");
        if (!userRaw) throw new Error("No se encontró el usuario en sesión.");
        const user = JSON.parse(userRaw);
        const studentId = user.userId || user.id;
        if (!studentId) throw new Error("El usuario no tiene userId.");

        // 1) Traer la evaluación completa (enunciados, opciones, umbral)
        const { data: evalData } = await getEvaluationFull(
          courseIdNum,
          evaluationIdNum
        );
        setEvaluation(evalData);

        // 2) Ver si ya existe un intento guardado para ESTE alumno
        const stored = loadStoredAttempt(evaluationIdNum, studentId);

        if (stored && stored.attemptId) {
          // Ya respondió este examen: solo mostramos su intento
          const { data: attemptRes } = await getAttemptResult(
            courseIdNum,
            stored.attemptId
          );

          setAttemptId(attemptRes.attemptId || stored.attemptId);
          setSubmitted(true);

          const initialAnswers = {};
          (attemptRes.questions || []).forEach((q) => {
            initialAnswers[q.questionId] = {
              optionId: q.selectedOptionId ?? null,
              openText: q.openText ?? "",
            };
          });
          setAnswers(initialAnswers);

          setResult({
            score: attemptRes.score,
            totalPoints: attemptRes.totalPoints,
            percentage: attemptRes.percentage,
            passed: attemptRes.passed,
          });

          return; // No creamos un nuevo intento
        }

        // 3) Si no hay intento previo para ESTE alumno, creamos uno nuevo
        const { data: attempt } = await createAttempt(
          courseIdNum,
          evaluationIdNum,
          studentId
        );
        setAttemptId(attempt.id);

        // Inicializar respuestas en blanco
        const initialAnswers = {};
        (evalData.questions || []).forEach((q) => {
          initialAnswers[q.id] = { optionId: null, openText: "" };
        });
        setAnswers(initialAnswers);
      } catch (err) {
        console.error(err);
        setError(
          err.response?.data?.message ||
            err.message ||
            "No se pudo cargar la evaluación."
        );
      } finally {
        setLoading(false);
      }
    };

    init();
  }, [hasValidIds, courseIdNum, evaluationIdNum]);

  const handleSelectOption = (questionId, optionId) => {
    if (submitted) return;
    setAnswers((prev) => ({
      ...prev,
      [questionId]: { ...(prev[questionId] || {}), optionId },
    }));
  };

  const handleOpenTextChange = (questionId, value) => {
    if (submitted) return;
    setAnswers((prev) => ({
      ...prev,
      [questionId]: { ...(prev[questionId] || {}), openText: value },
    }));
  };

  const handleSubmit = async () => {
    if (!attemptId || !evaluation || submitted) return;

    try {
      setSending(true);
      setError(null);

      // Transformar el diccionario en el arreglo que espera el backend
      const payloadAnswers = Object.entries(answers).map(
        ([questionId, obj]) => ({
          questionId: Number(questionId),
          optionId: obj.optionId || null,
          openText: obj.openText || null,
        })
      );

      // 1) Enviar respuestas
      await submitAnswers(courseIdNum, attemptId, payloadAnswers);

      // 2) Finalizar intento
      await finishAttempt(courseIdNum, attemptId);

      // 3) Traer resultado completo (incluye preguntas y opción marcada)
      const { data: attemptRes } = await getAttemptResult(
        courseIdNum,
        attemptId
      );

      // Guardar en localStorage que ESTE alumno respondió ESTE examen
      const userRaw = localStorage.getItem("user");
      const user = userRaw ? JSON.parse(userRaw) : null;
      const studentId = user?.userId || user?.id;
      if (studentId) {
        saveStoredAttempt(
          evaluationIdNum,
          courseIdNum,
          attemptRes.attemptId || attemptId,
          studentId
        );
      }

      const updatedAnswers = {};
      (attemptRes.questions || []).forEach((q) => {
        updatedAnswers[q.questionId] = {
          optionId: q.selectedOptionId ?? null,
          openText: q.openText ?? "",
        };
      });
      setAnswers(updatedAnswers);

      setResult({
        score: attemptRes.score,
        totalPoints: attemptRes.totalPoints,
        percentage: attemptRes.percentage,
        passed: attemptRes.passed,
      });

      setSubmitted(true);
    } catch (err) {
      console.error(err);
      setError(
        err.response?.data?.message ||
          err.message ||
          "No se pudo enviar la evaluación."
      );
    } finally {
      setSending(false);
    }
  };

  // -------------- RENDER -----------------

  if (!hasValidIds) {
    return (
      <section className="p-4 md:p-8">
        <p className="text-sm text-red-500">
          No se pudo identificar el curso o la evaluación. Intenta ingresar desde la lista de evaluaciones.
        </p>
      </section>
    );
  }

  if (loading) return <p>Cargando evaluación...</p>;

  if (error)
    return (
      <div className="p-4 bg-red-50 text-red-600 rounded">
        {error}
      </div>
    );

  if (!evaluation) return <p>No se encontró la evaluación.</p>;

  const questions = evaluation.questions || [];

  return (
    <section className="p-4 md:p-8">
      <h2 className="text-2xl md:text-3xl font-bold mb-2">
        {evaluation.title}
      </h2>
      <p className="text-gray-600 mb-2">
        {evaluation.description || "Evaluación final del curso."}
      </p>

      {submitted && (
        <p className="text-sm text-emerald-600 mb-4">
          Ya enviaste tus respuestas para este examen. Solo puedes revisarlas.
        </p>
      )}

      <div className="grid grid-cols-1 lg:grid-cols-[minmax(0,2fr)_minmax(260px,1fr)] gap-6">
        {/* Preguntas */}
        <div className="flex flex-col gap-4">
          {questions.length === 0 && (
            <p>Esta evaluación aún no tiene preguntas.</p>
          )}

          {questions.map((q, index) => {
            const selectedOptionId = answers[q.id]?.optionId ?? null;

            return (
              <div
                key={q.id}
                className="bg-white border rounded-md p-4 shadow-sm"
              >
                <p className="font-semibold mb-2">
                  {index + 1}. {q.prompt}
                </p>

                {/* Preguntas de alternativas */}
                {q.options && q.options.length > 0 && (
                  <div className="flex flex-col gap-2">
                    {q.options.map((opt) => {
                      const isSelected = selectedOptionId === opt.id;
                      const isCorrect = !!opt.isCorrect;

                      let highlightClass = "";
                      if (submitted) {
                        if (isCorrect) {
                          highlightClass =
                            "border border-green-500 bg-green-50 text-green-800";
                        } else if (isSelected && !isCorrect) {
                          highlightClass =
                            "border border-red-500 bg-red-50 text-red-800";
                        }
                      }

                      return (
                        <label
                          key={opt.id}
                          className={`flex items-center gap-2 cursor-pointer rounded px-2 py-1 ${highlightClass}`}
                        >
                          <input
                            type="radio"
                            name={`q-${q.id}`}
                            value={opt.id}
                            disabled={submitted}
                            checked={isSelected}
                            onChange={() =>
                              handleSelectOption(q.id, opt.id)
                            }
                          />
                          <span>{opt.text}</span>

                          {submitted && (
                            <span className="ml-2 text-xs font-semibold">
                              {isCorrect && "Correcta"}
                              {!isCorrect && isSelected && "Tu respuesta"}
                            </span>
                          )}
                        </label>
                      );
                    })}
                  </div>
                )}

                {/* Pregunta abierta */}
                {q.type === "OPEN" && (
                  <textarea
                    className="mt-2 w-full border rounded p-2 text-sm"
                    rows={3}
                    disabled={submitted}
                    value={answers[q.id]?.openText || ""}
                    onChange={(e) =>
                      handleOpenTextChange(q.id, e.target.value)
                    }
                  />
                )}
              </div>
            );
          })}
        </div>

        {/* Columna derecha: resumen y botón */}
        <aside className="bg-white border rounded-md p-4 shadow-sm h-fit">
          <h3 className="font-semibold mb-2">Resumen</h3>
          <p className="text-sm text-gray-600 mb-4">
            Umbral de aprobación:{" "}
            {evaluation.passThreshold != null ? (
              <strong>{(evaluation.passThreshold * 100).toFixed(0)}%</strong>
            ) : (
              "—"
            )}
          </p>

          {result && (
            <div className="mb-4 text-sm">
              <p>
                Puntaje:{" "}
                <strong>{result.score}</strong> / {result.totalPoints}
              </p>
              <p>
                Porcentaje:{" "}
                <strong>{(result.percentage * 100).toFixed(1)}%</strong>
              </p>
              <p>
                Estado:{" "}
                <strong
                  className={
                    result.passed ? "text-green-600" : "text-red-600"
                  }
                >
                  {result.passed ? "Aprobado" : "Reprobado"}
                </strong>
              </p>
            </div>
          )}

          <button
            onClick={handleSubmit}
            disabled={sending || submitted}
            className="w-full bg-duocazul text-white font-semibold text-sm px-4 py-2 rounded hover:bg-duocceleste disabled:bg-gray-300 disabled:cursor-not-allowed"
          >
            {submitted
              ? "Evaluación enviada"
              : sending
              ? "Enviando..."
              : "Enviar evaluación"}
          </button>
        </aside>
      </div>
    </section>
  );
}
