import React, { useState, useEffect } from "react";
import { useParams, useNavigate, useSearchParams } from "react-router-dom";

import GearButton from "../../components/ui/GearButton";
import EditarVideoModal from "../../components/profesor/EditarVideoModal";
import EditarQuizModal from "../../components/profesor/EditarQuizModal";
import AgregarVideoModal from "../../components/profesor/AgregarVideoModal";
import AgregarQuizModal from "../../components/profesor/AgregarQuizModal";

// ✔ SOLO ESTA ES LA CORRECTA
import { getLesson, uploadLessonVideo } from "../../services/lessonsService";

import {
  createEvaluation,
  createQuestion,
  createOption,
  getEvaluationsByLesson,
  updateQuestion,
  updateOption,
} from "../../services/evaluationsService";

export default function ClaseProfesor() {
  // Ruta: /profesor/editarclase/:courseId/:lessonId
  const { courseId, lessonId } = useParams();
  const courseIdNum = Number(courseId);
  const lessonIdNum = Number(lessonId);

  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const evaluationIdFromUrl = searchParams.get("evaluationId");

  // Estado del video (ahora viene del backend)
  // { id, titulo, descripcion, src }
  const [video, setVideo] = useState(null);

  // Lista de quizzes de la clase
  // Cada item: { evaluationId, questionId, pregunta, respuestas: [{ id, texto, correcta }] }
  const [quizzes, setQuizzes] = useState([]);

  const [evaluationId, setEvaluationId] = useState(
    evaluationIdFromUrl ? Number(evaluationIdFromUrl) : null
  );
  const [questionId, setQuestionId] = useState(null);

  // Índice del quiz que se está editando
  const [quizEditingIndex, setQuizEditingIndex] = useState(null);

  // Modals
  const [openEditVideo, setOpenEditVideo] = useState(false);
  const [openEditQuiz, setOpenEditQuiz] = useState(false);
  const [openNewVideo, setOpenNewVideo] = useState(false);
  const [openNewQuiz, setOpenNewQuiz] = useState(false);

  // ========= CARGAR VIDEO DE LA CLASE DESDE EL BACKEND =========
  useEffect(() => {
    const cargarLeccion = async () => {
      if (!courseIdNum || !lessonIdNum) return;

      try {
        const { data: lesson } = await getLesson(courseIdNum, lessonIdNum);

        setVideo({
          id: lesson.id,
          titulo: lesson.title,
          // por ahora descripción solo en front (BD no la tiene)
          descripcion: "",
          src: lesson.contentUrl || "",
        });
      } catch (err) {
        console.error("Error cargando lección:", err);
      }
    };

    cargarLeccion();
  }, [courseIdNum, lessonIdNum]);

  // ========= CARGAR TODOS LOS QUIZ DE LA CLASE DESDE EL BACKEND =========
  useEffect(() => {
    const cargarQuizzesDeClase = async () => {
      if (!courseIdNum || !lessonIdNum) return;

      try {
        const { data: evals } = await getEvaluationsByLesson(
          courseIdNum,
          lessonIdNum
        );

        console.log("EVALUACIONES DE LA CLASE => ", evals);

        const quizzesTransformados = evals.map((ev) => {
          const firstQuestion =
            ev.questions && ev.questions.length > 0 ? ev.questions[0] : null;

          const respuestas = (firstQuestion?.options || []).map((o) => ({
            id: o.id,
            texto: o.text,
            correcta: o.isCorrect,
          }));

          return {
            evaluationId: ev.id,
            questionId: firstQuestion ? firstQuestion.id : null,
            pregunta: firstQuestion ? firstQuestion.prompt : "(Sin pregunta)",
            respuestas,
          };
        });

        setQuizzes(quizzesTransformados);
      } catch (err) {
        console.error("Error cargando quizzes de la clase:", err);
      }
    };

    cargarQuizzesDeClase();
  }, [courseIdNum, lessonIdNum]);

  // ==================== HANDLERS VIDEO ======================

  // Editar video existente (puede incluir nuevo archivo en fd)
  const guardarVideoEdit = async (fd, plano) => {
    try {
      const { data } = await uploadLessonVideo(courseIdNum, lessonIdNum, fd);

      setVideo((prev) => ({
        ...prev,
        id: data.lessonId,
        titulo: plano.titulo || data.lessonTitle,
        descripcion: plano.descripcion || "",
        src: data.contentUrl,
      }));

      setOpenEditVideo(false);
      alert("Video actualizado correctamente ✅");
    } catch (err) {
      console.error("Error actualizando el video:", err);
      alert("Ocurrió un error al actualizar el video");
    }
  };


const crearVideo = async (fd /*, plano */) => {
  try {
    const { data } = await uploadLessonVideo(courseIdNum, lessonIdNum, fd);
    console.log("Video subido →", data);

    // Actualizar estado local del video para que se vea inmediatamente
    setVideo((prev) => ({
      ...prev,
      src: data.contentUrl,
      titulo: data.lessonTitle ?? prev.titulo,
      descripcion: prev.descripcion,
    }));

    setOpenNewVideo(false);
  } catch (err) {
    console.error("Error subiendo el video:", err);
    alert("Ocurrió un error al subir el video");
  }
};


  // ==================== HANDLERS QUIZ =======================

  const guardarQuizEdit = async (data) => {
    console.log("Editar Quiz →", data);

    if (quizEditingIndex === null) return;

    const quizOriginal = quizzes[quizEditingIndex];

    try {
      // 1) Actualizar la pregunta
      if (quizOriginal.questionId) {
        const questionBody = {
          prompt: data.pregunta,
          type: "SINGLE",
          points: 1,
          position: 1,
        };

        await updateQuestion(
          courseIdNum,
          quizOriginal.questionId,
          questionBody
        );
      }

      // 2) Actualizar cada opción usando su id
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

      // 3) Actualizar el estado local (manteniendo los ids)
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
      alert("Quiz actualizado correctamente ✅");
    } catch (err) {
      console.error("Error actualizando el quiz:", err);
      alert("Ocurrió un error al actualizar el quiz");
    }
  };

  const crearQuiz = async (data) => {
    try {
      if (!courseIdNum || !lessonIdNum) {
        console.error("Faltan courseId o lessonId en la ruta.");
        alert("No se encontró el curso o la clase en la URL.");
        return;
      }

      console.log("Nuevo Quiz →", data);

      // 1) Crear la evaluación (quiz) para el curso y asociarla a la clase
      const evalBody = {
        title: data.pregunta,
        description: null,
        dueAt: null,
        type: "QUIZ",
        passThreshold: 0.6,
        lessonId: lessonIdNum,
        isFinalExam: false,   // ← YA NO QUEDA NULL
      };


      const { data: evaluation } = await createEvaluation(courseIdNum, evalBody);
      setEvaluationId(evaluation.id);

      navigate(
        `/profesor/editarclase/${courseIdNum}/${lessonIdNum}?evaluationId=${evaluation.id}`,
        { replace: true }
      );

      // 2) Crear UNA pregunta asociada a esa evaluación
      const questionBody = {
        prompt: data.pregunta,
        type: "SINGLE",
        points: 1,
        position: 1,
      };

      const { data: question } = await createQuestion(
        courseIdNum,
        evaluation.id,
        questionBody
      );

      setQuestionId(question.id);

      // 3) Crear las opciones y quedarnos con sus IDs
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
        evaluationId: evaluation.id,
        questionId: question.id,
        pregunta: data.pregunta,
        respuestas: respuestasConIds,
      };

      setQuizzes((prev) => [...prev, nuevoQuiz]);

      alert("Quiz creado correctamente ✅");
      setOpenNewQuiz(false);
    } catch (err) {
      console.error("Error creando el quiz:", err);
      alert("Ocurrió un error al crear el quiz");
    }
  };

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-8">
        Mecánica básica - Manuel Gallegos
      </h2>

      {/* BLOQUE VIDEO con tuerca */}
      <div className="relative bg-white border rounded-md p-4 mb-6">
        <div className="grid grid-cols-1 md:grid-cols-[240px,1fr] gap-4">
          <div className="flex items-center justify-center bg-gray-50 rounded-md p-2">
            {video && video.src ? (
              <video src={video.src} className="w-full rounded" controls />
            ) : (
              <div className="text-sm text-gray-500 text-center p-4">
                Aún no hay video para esta clase.
              </div>
            )}
          </div>
          <div className="bg-gray-200 p-4 rounded">
            <h3 className="font-bold">
              {video ? video.titulo : "Sin título"}
            </h3>
            <p className="text-sm mt-2">
              {video ? video.descripcion || "Sin descripción" : ""}
            </p>
          </div>
        </div>
        <GearButton
          onClick={() => setOpenEditVideo(true)}
          className="absolute right-[-22px] top-1/2 -translate-y-1/2"
        />
      </div>

      {/* BLOQUE LISTA DE QUIZZES DE LA CLASE */}
      {quizzes.length > 0 && (
        <div className="space-y-6 mb-6">
          {quizzes.map((quiz, index) => (
            <div
              key={quiz.evaluationId ?? index}
              className="relative bg-white border rounded-md p-4"
            >
              <div className="bg-gray-200 rounded p-3 text-sm text-gray-800 mb-3">
                {quiz.pregunta}
              </div>
              <div className="space-y-2">
                {quiz.respuestas.map((r, i) => (
                  <button
                    key={r.id ?? i}
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
                  setQuizEditingIndex(index);
                  setOpenEditQuiz(true);
                }}
                className="absolute right-[-22px] top-1/2 -translate-y-1/2"
              />
            </div>
          ))}
        </div>
      )}

      {/* ZONA DE ACCIONES: AGREGAR VIDEO / AGREGAR QUIZ */}
      <div className="mt-6 bg-white border rounded-md p-6">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
          <button
            type="button"
            onClick={() => setOpenNewVideo(true)}
            className="w-full rounded-md bg-white p-8 shadow hover:shadow-md border flex items-center justify-center gap-3"
          >
            <span className="text-2xl text-duocceleste">＋</span>
            <span className="font-semibold text-duocceleste">
              AGREGAR VIDEO
            </span>
          </button>

          <button
            type="button"
            onClick={() => setOpenNewQuiz(true)}
            className="w-full rounded-md bg-white p-8 shadow hover:shadow-md border flex items-center justify-center gap-3"
          >
            <span className="text-2xl text-duocceleste">＋</span>
            <span className="font-semibold text-duocceleste">
              AGREGAR QUIZ
            </span>
          </button>
        </div>
      </div>

      {/* Modals de edición */}
      <EditarVideoModal
        open={openEditVideo}
        onClose={() => setOpenEditVideo(false)}
        onSubmit={guardarVideoEdit}
        initial={video}
      />
      <EditarQuizModal
        open={openEditQuiz}
        onClose={() => setOpenEditQuiz(false)}
        onSubmit={guardarQuizEdit}
        initial={quizEditingIndex !== null ? quizzes[quizEditingIndex] : null}
      />

      {/* Modals de creación */}
      <AgregarVideoModal
        open={openNewVideo}
        onClose={() => setOpenNewVideo(false)}
        onSubmit={crearVideo}
      />
      <AgregarQuizModal
        open={openNewQuiz}
        onClose={() => setOpenNewQuiz(false)}
        onSubmit={crearQuiz}
      />
    </section>
  );
}
