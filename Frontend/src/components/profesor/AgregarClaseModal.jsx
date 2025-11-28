import React, { useState } from "react";

export default function AgregarClaseModal({ open, onClose, onSubmit }) {
  const [nombre, setNombre] = useState("");
  const [descripcion, setDescripcion] = useState("");   // ← NUEVO
  const [relacionada, setRelacionada] = useState("");
  const [file, setFile] = useState(null);
  const [over, setOver] = useState(false);

  if (!open) return null;

  const handleDrop = (e) => {
    e.preventDefault();
    setOver(false);
    const f = e.dataTransfer.files?.[0];
    if (f) setFile(f);
  };
  const handlePick = (e) => {
    const f = e.target.files?.[0];
    if (f) setFile(f);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const fd = new FormData();
    fd.append("nombre", nombre);
    fd.append("descripcion", descripcion);               // ← NUEVO
    fd.append("info_relacionada", relacionada);
    if (file) fd.append("archivo", file);
    onSubmit?.(fd, { nombre, descripcion, relacionada, file }); // ← NUEVO en el objeto plano
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
            <h3 className="text-xl font-bold">Agregar clase</h3>
            <button onClick={onClose} className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300">
              Cerrar
            </button>
          </div>

          {/* Form */}
          <form onSubmit={handleSubmit} className="p-6 space-y-6">
            <div className="bg-gray-200 p-6">
              {/* Nombre clase */}
              <label className="block text-sm font-semibold mb-1">Nombre clase</label>
              <input
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo mb-6 bg-white"
                placeholder="Ej: Clase 1 - Introducción"
              />

              {/* Descripción (NUEVO) */}
              <label className="block text-sm font-semibold mb-1">Descripción de la clase</label>
              <textarea
                value={descripcion}
                onChange={(e) => setDescripcion(e.target.value)}
                rows={4}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo mb-6 bg-white"
                placeholder="Breve detalle de objetivos, contenidos, materiales, etc."
              />

              {/* Fila: dropzone + botón “Subir archivo” */}
              <div className="grid grid-cols-1 sm:grid-cols-[1fr,200px] gap-4 mb-6">
                {/* Dropzone/preview */}
                <div
                  onDragOver={(e) => e.preventDefault()}
                  onDragEnter={() => setOver(true)}
                  onDragLeave={() => setOver(false)}
                  onDrop={handleDrop}
                  className={[
                    "h-32 rounded-md border-2 border-dashed bg-white flex items-center justify-center",
                    over ? "border-duocceleste bg-blue-50" : "border-gray-300",
                  ].join(" ")}
                >
                  {!file ? (
                    <div className="text-center select-none">
                      <svg className="mx-auto w-12 h-12 text-gray-500 mb-2" viewBox="0 0 24 24" fill="none" stroke="currentColor">
                        <rect x="3" y="3" width="18" height="14" rx="2" />
                        <circle cx="8" cy="8" r="2" />
                        <path d="M21 17l-5-5-4 4-2-2-5 5" />
                      </svg>
                      <span className="text-sm text-duocceleste underline">Arrastre o suba imagen</span>
                    </div>
                  ) : (
                    <img
                      src={URL.createObjectURL(file)}
                      alt="preview"
                      className="max-h-28 object-contain"
                    />
                  )}
                </div>

                {/* Botón subir */}
                <div className="flex items-center">
                  <label className="inline-flex w-full justify-center rounded-md bg-duocceleste text-white font-semibold px-4 py-3 cursor-pointer hover:opacity-90">
                    Subir archivo
                    <input type="file" accept="image/*" onChange={handlePick} className="hidden" />
                  </label>
                </div>
              </div>

              {/* Información relacionada */}
              <label className="block text-sm font-semibold mb-1">Agregar Información relacionada</label>
              <input
                value={relacionada}
                onChange={(e) => setRelacionada(e.target.value)}
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                placeholder="Links, materiales, notas, etc."
              />
            </div>

            {/* Acciones */}
            <div className="flex items-center justify-end gap-3">
              <button type="button" onClick={onClose} className="px-4 py-2 rounded-md bg-gray-200 hover:bg-gray-300">
                Cancelar
              </button>
              <button type="submit" className="px-4 py-2 rounded-md bg-duocamarillo text-duocgris font-semibold hover:bg-duocceleste hover:text-white">
                Guardar clase
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
