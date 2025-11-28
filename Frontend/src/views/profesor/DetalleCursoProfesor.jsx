import React, { useState } from "react";
import DetCurso from "../../components/profesor/DetCurso";
import AgregarClaseModal from "../../components/profesor/AgregarClaseModal";

export default function DetalleCursoProfesor() {
  const [open, setOpen] = useState(false);

  const crearClase = (formData) => {
    console.log("Nueva clase →", [...formData.entries()]);
  };

  return (
    <section>
      <h2 className="text-3xl font-bold mb-8">
        Mecánica básica - Manuel Gallegos
      </h2>

      {/* Botón agregar cursos */}
      <div className="flex flex-wrap gap-3 items-stretch">
        <button
          onClick={() => setOpen(true)}
          className="w-80 min-h-[220px] rounded-md border-2 border-dashed border-duocceleste
                     bg-white hover:bg-blue-50 transition-colors shadow-sm
                     flex flex-col items-center justify-center gap-3 text-duocceleste"
          type="button"
        >
          <svg className="w-10 h-10" viewBox="0 0 24 24" fill="none"
               stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
            <rect x="3" y="3" width="18" height="14" rx="2" />
            <path d="M12 7v6M9 10h6" />
          </svg>
          <span className="text-sm font-medium">Agregar clase</span>
          <span className="mt-1 inline-flex rounded px-4 py-2 bg-duocamarillo text-duocgris font-semibold">
            Crear
          </span>
        </button>

        {/* Cartas clases */}
        <DetCurso />
        
      </div>

      <AgregarClaseModal
        open={open}
        onClose={() => setOpen(false)}
        onSubmit={crearClase}
      />
    </section>
  );
}
