import React, { useState } from "react";

export default function CrearEvaluacionModal({ open, onClose, cursos = [], onSubmit }) {
  if (!open) return null;

  const [form, setForm] = useState({
    courseId: "",
    title: "",
    instrucciones: "",
  });

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!form.courseId) {
      alert("Debes seleccionar un curso");
      return;
    }

    onSubmit(form);
    onClose();
  };

  return (
    <div className="fixed inset-0 bg-black/40 flex justify-center items-center p-4 z-50">
      <div className="bg-white w-full max-w-lg rounded-md p-6 shadow-xl">
        <div className="flex justify-between items-center mb-4">
          <h2 className="text-xl font-bold">Crear evaluación final</h2>
          <button onClick={onClose} className="text-gray-600">✕</button>
        </div>

        <form onSubmit={handleSubmit} className="space-y-5">

          {/* SELECT DE CURSOS */}
          <div>
            <label className="block mb-1 font-medium">Curso</label>

            <select
              className="w-full border rounded-md p-2"
              value={form.courseId}
              onChange={(e) => setForm({ ...form, courseId: e.target.value })}
            >
              <option value="">Selecciona un curso</option>

              {cursos.length > 0 ? (
                cursos.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.title}
                  </option>
                ))
              ) : (
                <option value="">No tienes cursos disponibles</option>
              )}
            </select>
          </div>

          {/* TÍTULO DE LA EVALUACIÓN */}
          <div>
            <label className="block mb-1 font-medium">Nombre de la evaluación</label>
            <input
              className="w-full border rounded-md p-2"
              placeholder="Ej: Evaluación final - Unidad 1"
              value={form.title}
              onChange={(e) => setForm({ ...form, title: e.target.value })}
            />
          </div>

          {/* INSTRUCCIONES */}
          <div>
            <label className="block mb-1 font-medium">Instrucciones para el estudiante</label>
            <textarea
              className="w-full border rounded-md p-2"
              rows={4}
              placeholder="Describe las instrucciones generales de la evaluación"
              value={form.instrucciones}
              onChange={(e) => setForm({ ...form, instrucciones: e.target.value })}
            />
          </div>

          {/* BOTONES */}
          <div className="flex justify-end gap-3 mt-6">
            <button
              type="button"
              onClick={onClose}
              className="px-4 py-2 border rounded-md"
            >
              Cancelar
            </button>

            <button
              type="submit"
              className="px-4 py-2 bg-duocamarillo text-duocgris font-semibold rounded-md"
            >
              Crear evaluación
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}
