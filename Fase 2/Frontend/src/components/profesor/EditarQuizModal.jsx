import React, { useState, useEffect } from "react";

export default function EditarQuizModal({ open, onClose, onSubmit, initial }) {
  // initial: { id, pregunta, respuestas: [{texto, correcta}] }
  const [pregunta, setPregunta] = useState(initial?.pregunta || "");
  const [respuestas, setRespuestas] = useState(initial?.respuestas || []);
  const [error, setError] = useState("");

  useEffect(() => {
    if (open) {
      setPregunta(initial?.pregunta || "");
      setRespuestas(initial?.respuestas || []);
      setError("");
    }
  }, [open, initial]);

  if (!open) return null;

  const setTexto = (i, texto) => setRespuestas(rs => rs.map((r, idx) => idx === i ? { ...r, texto } : r));
  const marcarCorrecta = (i) => setRespuestas(rs => rs.map((r, idx) => ({ ...r, correcta: idx === i })));
  const agregarResp = () => setRespuestas(rs => [...rs, { texto: "", correcta: false }]);
  const borrarResp = (i) => setRespuestas(rs => rs.filter((_, idx) => idx !== i));

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!pregunta.trim()) return setError("La pregunta es obligatoria.");
    const val = respuestas.filter(r => r.texto.trim());
    if (val.length < 2) return setError("Debe haber al menos 2 respuestas.");
    if (!val.some(r => r.correcta)) return setError("Marca una respuesta correcta.");
    onSubmit?.({ id: initial.id, pregunta, respuestas: val });
    onClose();
  };

  return (
    <div className="fixed inset-0 z-50">
      <div className="absolute inset-0 bg-black/50" onClick={onClose} />
      <div className="relative mx-auto mt-10 w-full max-w-3xl">
        <div className="rounded-xl bg-white shadow-xl overflow-hidden">
          <div className="flex items-center justify-between px-6 py-4 border-b">
            <h3 className="text-xl font-bold">Editar quiz</h3>
            <button onClick={onClose} className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300">Cerrar</button>
          </div>

          <form onSubmit={handleSubmit} className="p-6 space-y-6">
            <div className="bg-gray-100 p-6 rounded-md">
              <label className="block text-sm font-semibold mb-1">Pregunta</label>
              <input
                value={pregunta}
                onChange={(e) => setPregunta(e.target.value)}
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                placeholder="Escribe la pregunta…"
              />

              <h4 className="mt-6 mb-2 font-semibold">Respuestas</h4>
              <div className="space-y-3">
                {respuestas.map((r, i) => (
                  <div key={i} className="grid grid-cols-[1fr,44px,44px] gap-2 items-center">
                    <input
                      value={r.texto}
                      onChange={(e) => setTexto(i, e.target.value)}
                      className="rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                      placeholder={`Respuesta ${i + 1}`}
                    />
                    <button
                      type="button"
                      onClick={() => marcarCorrecta(i)}
                      className={[
                        "h-10 rounded-md font-semibold",
                        r.correcta ? "bg-duocamarillo text-duocgris" : "bg-duocceleste text-white",
                      ].join(" ")}
                      title="Marcar como correcta"
                    >
                      {r.correcta ? "✔" : "✓"}
                    </button>
                    <button type="button" onClick={() => borrarResp(i)} className="h-10 rounded-md bg-gray-200 hover:bg-gray-300" title="Eliminar">
                      −
                    </button>
                  </div>
                ))}
              </div>

              <button type="button" onClick={agregarResp} className="mt-3 inline-flex items-center gap-2 px-3 py-2 rounded-md bg-white border hover:bg-gray-50">
                <span className="text-duocceleste text-xl leading-none">＋</span> Agregar respuesta
              </button>
            </div>

            {error && <p className="text-red-600">{error}</p>}

            <div className="flex items-center justify-end gap-3">
              <button type="button" onClick={onClose} className="px-4 py-2 rounded-md bg-gray-200 hover:bg-gray-300">Cancelar</button>
              <button type="submit" className="px-4 py-2 rounded-md bg-duocamarillo text-duocgris font-semibold hover:bg-duocceleste hover:text-white">Guardar cambios</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
