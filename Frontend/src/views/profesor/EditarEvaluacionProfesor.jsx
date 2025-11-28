import React, { useState } from "react";
import GearButton from "../../components/ui/GearButton";
import EditarVideoModal from "../../components/profesor/EditarVideoModal";
import EditarQuizModal from "../../components/profesor/EditarQuizModal";
import AgregarVideoModal from "../../components/profesor/AgregarVideoModal";
import AgregarQuizModal from "../../components/profesor/AgregarQuizModal";

export default function EditarEvaluacionProfesor() {
  // Ejemplo de datos existentes
  const [video, setVideo] = useState({
    id: 1, titulo: "Clase 1 - Introducción", descripcion: "Descripción", src: "/media/videos/clase1.mp4"
  });
  const [quiz, setQuiz] = useState({
    id: 10,
    pregunta: "¿Cuál es la función principal de una llave combinada?",
    respuestas: [
      { texto: "Ajustar tornillos Allen", correcta: false },
      { texto: "Aflojar pernos de diferentes medidas", correcta: true },
      { texto: "Cortar cables", correcta: false },
    ],
  });

  // Modals de edición (tuercas)
  const [openEditVideo, setOpenEditVideo] = useState(false);
  const [openEditQuiz, setOpenEditQuiz] = useState(false);

  // ✅ Modals de creación (botones grandes)
  const [openNewVideo, setOpenNewVideo] = useState(false);
  const [openNewQuiz, setOpenNewQuiz] = useState(false);

  // Handlers
  const guardarVideoEdit = (fd, plano) => {
    console.log("Editar Video →", [...fd.entries()], plano);
    setVideo(v => ({ ...v, titulo: plano.titulo, descripcion: plano.descripcion }));
  };
  const guardarQuizEdit = (data) => {
    console.log("Editar Quiz →", data);
    setQuiz(data);
  };

  // ✅ Crear nuevos (aquí harías el POST real)
  const crearVideo = (fd /*, plano */) => {
    console.log("Nuevo Video →", [...fd.entries()]);
    setOpenNewVideo(false);
  };
  const crearQuiz = (data) => {
    console.log("Nuevo Quiz →", data);
    setOpenNewQuiz(false);
  };

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-8">Mecánica básica - Manuel Gallegos</h2>
    
      {/* BLOQUE QUIZ con tuerca */}
      <div className="relative bg-white border rounded-md p-4 mb-6">
        <div className="bg-gray-200 rounded p-3 text-sm text-gray-800 mb-3">
          {quiz.pregunta}
        </div>
        <div className="space-y-2">
          {quiz.respuestas.map((r, i) => (
            <button
              key={i}
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

        <GearButton onClick={() => setOpenEditQuiz(true)} className="absolute right-[-22px] top-1/2 -translate-y-1/2" />
      </div>

      {/* ✅ ZONA DE ACCIONES: AGREGAR VIDEO / AGREGAR QUIZ */}
      <div className="mt-6 bg-white border rounded-md p-6">
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-6">
          
          <button
            type="button"
            onClick={() => setOpenNewQuiz(true)}
            className="w-full rounded-md bg-white p-8 shadow hover:shadow-md border
                       flex items-center justify-center gap-3"
          >
            <span className="text-2xl text-duocceleste">＋</span>
            <span className="font-semibold text-duocceleste">AGREGAR QUIZ</span>
          </button>
        </div>
      </div>

      {/* Modals de edición */}
      <EditarQuizModal open={openEditQuiz} onClose={() => setOpenEditQuiz(false)} onSubmit={guardarQuizEdit} initial={quiz} />

      {/* ✅ Modals de creación */}
      <AgregarQuizModal open={openNewQuiz} onClose={() => setOpenNewQuiz(false)} onSubmit={crearQuiz} />
    </section>
  );
}
