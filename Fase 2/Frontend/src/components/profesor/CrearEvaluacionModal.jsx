import React, { useMemo, useState } from "react";

export default function CrearEvaluacionModal({
  open,
  onClose,
  onSubmit,
  escuelas = [],                         // ["Escuela Ingeniería", "Escuela Construcción", ...]
  carrerasPorEscuela = {},              // { "Escuela Ingeniería": ["Informática","Industrial"], ... }
}) {
  const [escuela, setEscuela] = useState("");
  const [carrera, setCarrera] = useState("");
  const [instrucciones, setInstrucciones] = useState("");

  const carreras = useMemo(
    () => (escuela ? carrerasPorEscuela[escuela] || [] : []),
    [escuela, carrerasPorEscuela]
  );

  if (!open) return null;

  const handleSubmit = (e) => {
    e.preventDefault();
    const fd = new FormData();
    fd.append("escuela", escuela);
    fd.append("carrera", carrera);
    fd.append("instrucciones", instrucciones);
    onSubmit?.(fd, { escuela, carrera, instrucciones });
    onClose();
  };

  return (
    <div className="fixed inset-0 z-50">
      {/* overlay */}
      <div className="absolute inset-0 bg-black/50" onClick={onClose} />

      {/* card */}
      <div className="relative mx-auto mt-10 w-full max-w-3xl">
        <div className="rounded-xl bg-white shadow-xl overflow-hidden">
          <div className="flex items-center justify-between px-6 py-4 border-b">
            <h3 className="text-xl font-bold">Crear evaluación</h3>
            <button
              onClick={onClose}
              className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300"
            >
              Cerrar
            </button>
          </div>

          <form onSubmit={handleSubmit} className="p-6">
            {/* bloque gris como la referencia */}
            <div className="bg-gray-200 p-6 rounded-md space-y-6">
              {/* Escuela */}
              <div>
                <label className="block text-sm font-semibold mb-1">
                  Seleccione la escuela
                </label>
                <select
                  required
                  value={escuela}
                  onChange={(e) => {
                    setEscuela(e.target.value);
                    setCarrera("");
                  }}
                  className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
                >
                  <option value="" disabled>— Seleccione —</option>
                  {escuelas.map((e) => (
                    <option key={e} value={e}>{e}</option>
                  ))}
                </select>
              </div>

              {/* Carrera relacionada */}
              <div>
                <label className="block text-sm font-semibold mb-1">
                  Seleccione carrera relacionada
                </label>
                <select
                  required
                  disabled={!escuela}
                  value={carrera}
                  onChange={(e) => setCarrera(e.target.value)}
                  className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo disabled:bg-gray-100"
                >
                  <option value="" disabled>— Seleccione —</option>
                  {carreras.map((c) => (
                    <option key={c} value={c}>{c}</option>
                  ))}
                </select>
              </div>

              {/* Instrucciones */}
              <div>
                <label className="block text-sm font-semibold mb-1">
                  Añadir instrucciones
                </label>
                <textarea
                  rows={3}
                  value={instrucciones}
                  onChange={(e) => setInstrucciones(e.target.value)}
                  className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
                  placeholder="Escribe detalles, reglas, materiales, etc."
                />
              </div>

              {/* Botón Siguiente centrado */}
              <div className="flex justify-center pt-2">
                <button
                  type="submit"
                  className="min-w-40 rounded-md bg-duocamarillo text-duocgris font-semibold px-6 py-2 hover:bg-duocceleste hover:text-white transition-colors"
                >
                  Siguiente
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
