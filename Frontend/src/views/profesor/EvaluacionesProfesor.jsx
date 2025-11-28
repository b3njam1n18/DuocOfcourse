import React, { useState } from "react";
import AddCardButton from "../../components/ui/AddCardButton";
import EvaluacionesComp from "../../components/profesor/EvaluacionesComp";
import CrearEvaluacionModal from "../../components/profesor/CrearEvaluacionModal";

export default function EvaluacionesProfesor() {
  const [open, setOpen] = useState(false);

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-8">Evaluaciones</h2>

      <div className="flex flex-wrap gap-4 items-stretch">
        {/* 👇 carta-botón igual a “Agregar clase” */}
        <AddCardButton
          title="Agregar evaluación"
          cta="Crear"
          onClick={() => setOpen(true)}
        />

        {/* tus cartas existentes */}
        <EvaluacionesComp />
        <EvaluacionesComp />
      </div>

      <CrearEvaluacionModal open={open} onClose={() => setOpen(false)} onSubmit={(fd, plano) => console.log([...fd.entries()], plano)} />
    </section>
  );
}
