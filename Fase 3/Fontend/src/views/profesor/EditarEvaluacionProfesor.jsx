import React, { useState, useEffect } from "react";
import { useSearchParams, useNavigate } from "react-router-dom";

import GearButton from "../../components/ui/GearButton";
import EditarQuizModal from "../../components/profesor/EditarQuizModal";
import AgregarQuizModal from "../../components/profesor/AgregarQuizModal";

import {
  getEvaluationFull,
  createQuestion,
  createOption,
  updateQuestion,
  updateOption,
} from "../../services/evaluationsService";

export default function EditarEvaluacionProfesor() {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();

  const courseIdParam = searchParams.get("courseId");
  const evaluationIdParam = searchParams.get("evaluationId");

  const courseIdNum = courseIdParam ? Number(courseIdParam) : null;
  const evaluationIdNum = evaluationIdParam ? Number(evaluationIdParam) : null;

  // Datos de la evaluación (título, descripción, etc.)
  const [evaluation, setEvaluation] = useState(null);

  // Lista de "quizzes" = preguntas con sus alternativas
  // cada item: { questionId, pregunta, respuestas: [{ id, texto, correcta }] }
  const [quizzes, setQuizzes] = useState([]);

  // Estado para edición / creación
  const [quizEditingIndex, setQuizEditingIndex] = useState(null);
  const [openEditQuiz, setOpenEditQuiz] = useState(false);
  const [openNewQuiz, setOpenNewQuiz] = useState(false);

  // =============== CARGAR EVALUACIÓN COMPLETA ===============
  useEffect(() => {
    if (!courseIdNum || !evaluationIdNum) {
      console.error("Faltan courseId o evaluationId en la URL");
      return;
    }

    const cargarEvaluacion = async () => {
      try {
        const { data } = await getEvaluationFull(courseIdNum, evaluationIdNum);
        console.log("EVALUACIÓN COMPLETA (final) =>", data);
        setEvaluation(data);

        const quizzesTransformados = (data.questions || []).map((q) => ({
          questionId: q.id,
          pregunta: q.prompt,
          respuestas: (q.options || []).map((o) => ({
            id: o.id,
            texto: o.text,
            correcta: o.isCorrect,
          })),
        }));

        setQuizzes(quizzesTransformados);
      } catch (err) {
        console.error("Error cargando evaluación completa:", err);
        alert("No se pudo cargar la evaluación");
      }
    };

    cargarEvaluacion();
  }, [courseIdNum, evaluationIdNum]);

  // =============== EDITAR QUIZ (PREGUNTA) ===============
  const guardarQuizEdit = async (data) => {
    console.log("Editar Quiz (evaluación final) →", data);

    if (quizEditingIndex === null) return;

    const quizOriginal = quizzes[quizEditingIndex];

    try {
      // 1) Actualizar la pregunta
      if (quizOriginal.questionId) {
        const questionBody = {
          prompt: data.pregunta,
          type: "SINGLE",
          points: 1,
          position: quizEditingIndex + 1, // opcional, orden
        };

        await updateQuestion(courseIdNum, quizOriginal.questionId, questionBody);
      }

      // 2) Actualizar cada opción usando su id original
      await Promise.all(
        data.respuestas.map((r, idx) => {
          const opcionOriginal = quizOriginal.respuestas[idx];
          if (!opcionOriginal || !opcionOriginal.id) return null;

          return updateOption(courseIdNum, opcionOriginal.id, {
            text: r.texto,
            isCorrect: r.correcta,
          });
        })
      );

      // 3) Actualizar estado local
      const respuestasActualizadas = data.respuestas.map((r, idx) => ({
        ...quizOriginal.respuestas[idx],
        texto: r.texto,
        correcta: r.correcta,
      }));

      const quizActualizado = {
        ...quizOriginal,
        pregunta: data.pregunta,
        respuestas: respuestasActualizadas,
      };

      setQuizzes((prev) =>
        prev.map((q, idx) => (idx === quizEditingIndex ? quizActualizado : q))
      );

      setOpenEditQuiz(false);
      alert("Pregunta/quiz actualizado correctamente ✅");
    } catch (err) {
      console.error("Error actualizando el quiz:", err);
      alert("Ocurrió un error al actualizar el quiz");
    }
  };

  // =============== CREAR NUEVO QUIZ (PREGUNTA) ===============
  const crearQuiz = async (data) => {
    console.log("Nuevo Quiz (evaluación final) →", data);

    if (!courseIdNum || !evaluationIdNum) {
      alert("No se encontró el curso o la evaluación en la URL.");
      return;
    }

    try {
      // 1) Crear la pregunta asociada a esta evaluación final
      const questionBody = {
        prompt: data.pregunta,
        type: "SINGLE",
        points: 1,
        position: quizzes.length + 1,
      };

      const { data: question } = await createQuestion(
        courseIdNum,
        evaluationIdNum,
        questionBody
      );

      // 2) Crear las opciones para esa pregunta
      const opcionesCreadas = await Promise.all(
        data.respuestas.map((r) =>
          createOption(courseIdNum, question.id, {
            text: r.texto,
            isCorrect: r.correcta,
          })
        )
      );

      const respuestasConIds = opcionesCreadas.map((resp) => {
        const opt = resp.data;
        return {
          id: opt.id,
          texto: opt.text,
          correcta: opt.isCorrect,
        };
      });

      const nuevoQuiz = {
        questionId: question.id,
        pregunta: data.pregunta,
        respuestas: respuestasConIds,
      };

      setQuizzes((prev) => [...prev, nuevoQuiz]);

      setOpenNewQuiz(false);
      alert("Pregunta agregada correctamente ✅");
    } catch (err) {
      console.error("Error creando la pregunta:", err);
      alert("Ocurrió un error al crear la pregunta");
    }
  };

  // =============== RENDER ===============
  if (!courseIdNum || !evaluationIdNum) {
    return (
      <section className="p-8">
        <h2 className="text-2xl font-bold mb-4">Editar evaluación</h2>
        <p className="text-sm text-red-600">
          Falta <code>courseId</code> o <code>evaluationId</code> en la URL.
        </p>
      </section>
    );
  }

  return (
    <section className="p-8">
      <button
        type="button"
        onClick={() => navigate(-1)}
        className="mb-4 text-sm text-duocazul underline"
      >
        ← Volver
      </button>

      <h2 className="text-3xl font-bold mb-1">
        {evaluation?.title || "Evaluación final"}
      </h2>
      <p className="text-sm text-gray-600 mb-8">
        {evaluation?.description || "Sin descripción"}
      </p>

      {/* BLOQUE QUIZZES (preguntas) */}
      {quizzes.length > 0 ? (
        quizzes.map((quiz, i) => (
          <div
            key={quiz.questionId ?? i}
            className="relative bg-white border rounded-md p-4 mb-6"
          >
            <div className="bg-gray-200 rounded p-3 text-sm text-gray-800 mb-3">
              {quiz.pregunta}
            </div>
            <div className="space-y-2">
              {quiz.respuestas.map((r, idx) => (
                <button
                  key={r.id ?? idx}
                  type="button"
                  disabled
                  className={[
                    "w-full md:w-4/5 px-3 py-2 rounded-md font-semibold text-left transition-colors",
                    r.correcta
                      ? "bg-duocamarillo text-duocgris hover:bg-duocazul hover:text-duocamarillo"
                      : "bg-duocgris text-white hover:bg-duocazul hover:text-duocamarillo",
                  ].join(" ")}
                >
                  {r.texto}
                </button>
              ))}
            </div>

            <GearButton
              onClick={() => {
                setQuizEditingIndex(i);
                setOpenEditQuiz(true);
              }}
              className="absolute right-[-22px] top-1/2 -translate-y-1/2"
            />
          </div>
        ))
      ) : (
        <p className="text-sm text-gray-600 mb-6">
          Aún no hay preguntas/quiz creados para esta evaluación.
        </p>
      )}

      {/* ZONA DE ACCIONES: AGREGAR QUIZ */}
      <div className="mt-6 bg-white border rounded-md p-6">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
          <button
            type="button"
            onClick={() => setOpenNewQuiz(true)}
            className="w-full rounded-md bg-white p-8 shadow hover:shadow-md border
                       flex items-center justify-center gap-3"
          >
            <span className="text-2xl text-duocceleste">＋</span>
            <span className="font-semibold text-duocceleste">
              AGREGAR QUIZ
            </span>
          </button>
        </div>
      </div>

      {/* Modals de edición */}
      <EditarQuizModal
        open={openEditQuiz}
        onClose={() => setOpenEditQuiz(false)}
        onSubmit={guardarQuizEdit}
        initial={quizEditingIndex !== null ? quizzes[quizEditingIndex] : null}
      />

      {/* Modals de creación */}
      <AgregarQuizModal
        open={openNewQuiz}
        onClose={() => setOpenNewQuiz(false)}
        onSubmit={crearQuiz}
      />
    </section>
  );
}
