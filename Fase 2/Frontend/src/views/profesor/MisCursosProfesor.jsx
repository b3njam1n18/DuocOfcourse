import React, { useState } from "react";
import Cursos from "../../components/profesor/CursosProfesor";
import NuevoCursoModal from "../../components/profesor/NuevoCursoModal";
import AddCardButton from "../../components/ui/AddCardButton";

export default function MisCursosProfesor() {
  const [open, setOpen] = useState(false);

  const crearCurso = (formData /*, plain */) => {
    console.log("Enviando curso…", [...formData.entries()]);
  };

  return (
    <main>
      <section className="p-8">
        {/* Título */}
        <h2 className="text-3xl font-bold mb-8">Mis cursos</h2>

        {/* Grilla de cartas + botón-carta para crear */}
        <div className="flex flex-wrap gap-4 items-stretch">
          <AddCardButton
            title="Agregar curso"
            cta="Crear"
            onClick={() => setOpen(true)}
          />

          {/* Tus cursos */}
          <Cursos />
        </div>

        {/* Modal de creación */}
        <NuevoCursoModal
          open={open}
          onClose={() => setOpen(false)}
          onSubmit={crearCurso}
        />
      </section>
    </main>
  );
}
 