import React, { useState } from "react";

export default function AgregarQuizModal({ open, onClose, onSubmit }) {
  const [pregunta, setPregunta] = useState("");
  const [respuestas, setRespuestas] = useState([
    { texto: "", correcta: false },
    { texto: "", correcta: false },
  ]);
  const [error, setError] = useState("");

  if (!open) return null;

  const setTexto = (i, texto) => {
    setRespuestas(prev => prev.map((r, idx) => idx === i ? { ...r, texto } : r));
  };
  const marcarCorrecta = (i) => {
    setRespuestas(prev => prev.map((r, idx) => ({ ...r, correcta: idx === i })));
  };
  const agregarRespuesta = () => setRespuestas(prev => [...prev, { texto: "", correcta: false }]);
  const eliminarRespuesta = (i) => setRespuestas(prev => prev.filter((_, idx) => idx !== i));

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!pregunta.trim()) return setError("La pregunta es obligatoria.");
    if (respuestas.filter(r => r.texto.trim()).length < 2)
      return setError("Debe haber al menos 2 respuestas.");
    if (!respuestas.some(r => r.correcta))
      return setError("Marca una respuesta correcta.");

    setError("");
    onSubmit?.({
      pregunta,
      respuestas: respuestas.filter(r => r.texto.trim()),
    });
    onClose();
  };

  return (
    <div className="fixed inset-0 z-50">
      <div className="absolute inset-0 bg-black/50" onClick={onClose} />
      <div className="relative mx-auto mt-10 w-full max-w-3xl">
        <div className="rounded-xl bg-white shadow-xl overflow-hidden">
          <div className="flex items-center justify-between px-6 py-4 border-b">
            <h3 className="text-xl font-bold">Agregar quiz</h3>
            <button onClick={onClose} className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300">Cerrar</button>
          </div>

          <form onSubmit={handleSubmit} className="p-6 space-y-6">
            <div className="bg-gray-100 p-6 rounded-md">
              <label className="block text-sm font-semibold mb-1">Agregar pregunta principal</label>
              <input
                value={pregunta}
                onChange={(e) => setPregunta(e.target.value)}
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                placeholder="Escribe la pregunta…"
              />

              <h4 className="mt-6 mb-2 font-semibold">Agregar respuestas</h4>
              <div className="space-y-3">
                {respuestas.map((r, i) => {
                  const isCorrect = r.correcta;
                  return (
                    <div key={i} className="grid grid-cols-[1fr,44px,44px] gap-2 items-center">
                      <input
                        value={r.texto}
                        onChange={(e) => setTexto(i, e.target.value)}
                        className="rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                        placeholder={`Respuesta ${i + 1}`}
                      />
                      {/* cuadrado para marcar correcta (amarillo) o normal (azul) */}
                      <button
                        type="button"
                        onClick={() => marcarCorrecta(i)}
                        className={[
                          "h-10 rounded-md font-semibold",
                          isCorrect
                            ? "bg-duocamarillo text-duocgris"
                            : "bg-duocceleste text-white",
                        ].join(" ")}
                        title="Marcar como correcta"
                      >
                        {isCorrect ? "✔" : "✓"}
                      </button>
                      <button
                        type="button"
                        onClick={() => eliminarRespuesta(i)}
                        className="h-10 rounded-md bg-gray-200 hover:bg-gray-300"
                        title="Eliminar respuesta"
                      >
                        −
                      </button>
                    </div>
                  );
                })}
              </div>

              <button
                type="button"
                onClick={agregarRespuesta}
                className="mt-3 inline-flex items-center gap-2 px-3 py-2 rounded-md bg-white border hover:bg-gray-50"
              >
                <span className="text-duocceleste text-xl leading-none">＋</span>
                Agregar respuesta
              </button>
            </div>

            {error && <p className="text-red-600">{error}</p>}

            <div className="flex items-center justify-end gap-3">
              <button type="button" onClick={onClose} className="px-4 py-2 rounded-md bg-gray-200 hover:bg-gray-300">
                Cancelar
              </button>
              <button type="submit" className="px-4 py-2 rounded-md bg-duocamarillo text-duocgris font-semibold hover:bg-duocceleste hover:text-white">
                Guardar quiz
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
