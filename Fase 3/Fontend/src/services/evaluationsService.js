// src/services/evaluationsService.js
import api from "./api";

// Crear evaluación (final o de clase, según body)
export const createEvaluation = (courseId, body) =>
  api.post(`/courses/${courseId}/evaluations`, body);

// Obtener evaluaciones de un curso (puedes filtrar por published)
export const getEvaluationsByCourse = (courseId, published) =>
  api.get(`/courses/${courseId}/evaluations`, {
    params: { published },
  });

// Obtener evaluación completa con preguntas y opciones
export const getEvaluationFull = (courseId, evaluationId) =>
  api.get(`/courses/${courseId}/evaluations/${evaluationId}`);

// Evaluaciones por clase/lección
export const getEvaluationsByLesson = (courseId, lessonId, published) =>
  api.get(`/courses/${courseId}/lessons/${lessonId}/evaluations`, {
    params: { published },
  });

// ───────── PREGUNTAS ─────────

export const createQuestion = (courseId, evaluationId, body) =>
  api.post(
    `/courses/${courseId}/evaluations/${evaluationId}/questions`,
    body
  );

export const updateQuestion = (courseId, questionId, body) =>
  api.put(
    `/courses/${courseId}/evaluations/questions/${questionId}`,
    body
  );

// ───────── OPCIONES ─────────

export const createOption = (courseId, questionId, body) =>
  api.post(
    `/courses/${courseId}/evaluations/CreateOption`,
    body,
    {
      params: { questionId },
    }
  );

export const updateOption = (courseId, optionId, body) =>
  api.put(
    `/courses/${courseId}/evaluations/options/${optionId}`,
    body
  );

// Publicar / despublicar evaluación
export const publishEvaluation = (courseId, evaluationId, publish) =>
  api.put(
    `/courses/${courseId}/evaluations/${evaluationId}/publish`,
    { publish }
  );

// ───────── INTENTOS DE EVALUACIÓN FINAL ─────────

// Crear intento para una evaluación final
export const createAttempt = (courseId, evaluationId, studentId) =>
  api.post(
    `/courses/${courseId}/evaluations/Crear_Intento_Evaluacion`,
    { studentId },
    { params: { evaluationId } }
  );

// Enviar respuestas de una evaluación (intento)
export const submitAnswers = (courseId, attemptId, answers) =>
  api.post(
    `/courses/${courseId}/evaluations/EntregarRespuesta`,
    { answers },
    { params: { attemptId } }
  );

// Finalizar intento y calcular resultado
export const finishAttempt = (courseId, attemptId) =>
  api.put(
    `/courses/${courseId}/evaluations/attempts/${attemptId}/finish`
  );

// ───────── QUIZZES POR LECCIÓN ─────────

// Guardar respuestas de todos los quizzes de una lección
export const submitLessonAttempts = (courseId, lessonId, body) =>
  api.post(`/courses/${courseId}/lessons/${lessonId}/attempts`, body);

// Obtener intentos de una lección por alumno
export const getLessonAttempts = (courseId, lessonId, studentId) =>
  api.get(`/courses/${courseId}/lessons/${lessonId}/attempts`, {
    params: { studentId },
  });

// Obtener detalle de un intento (incluye preguntas y opción marcada)
export const getAttemptResult = (courseId, attemptId) =>
  api.get(`/courses/${courseId}/evaluations/attempts/${attemptId}`);

// ⬅️ AQUÍ va corregido
export const getLastAttemptByStudent = (courseId, evaluationId, studentId) =>
  api.get(
    `/courses/${courseId}/evaluations/${evaluationId}/attempts/by-student`,
    { params: { studentId } }
  );
