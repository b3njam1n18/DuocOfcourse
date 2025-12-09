import React, { useState } from "react";

export default function AgregarVideoModal({
  open,
  onClose,
  onSubmit,
  maxMB = 200, // límite opcional (200 MB)
}) {
  const [titulo, setTitulo] = useState("");
  const [desc, setDesc] = useState("");
  const [file, setFile] = useState(null);
  const [over, setOver] = useState(false);
  const [error, setError] = useState("");

  if (!open) return null;

  const accept = "video/*";
  const maxBytes = maxMB * 1024 * 1024;

  const validateFile = (f) => {
    if (!f) return "Debes seleccionar un video.";
    if (!f.type.startsWith("video/")) return "El archivo debe ser de video.";
    if (f.size > maxBytes) return `El archivo no puede superar ${maxMB} MB.`;
    return "";
  };

  const handlePick = (e) => {
    const f = e.target.files?.[0];
    const err = validateFile(f);
    setError(err);
    setFile(err ? null : f);
  };

  const handleDrop = (e) => {
    e.preventDefault();
    setOver(false);
    const f = e.dataTransfer.files?.[0];
    const err = validateFile(f);
    setError(err);
    setFile(err ? null : f);
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const err = validateFile(file);
    if (err) {
      setError(err);
      return;
    }
    const fd = new FormData();
    fd.append("titulo", titulo);
    fd.append("descripcion", desc);
    fd.append("video", file); // 👈 esto ahora coincide con [FromForm(Name="video")]

    onSubmit?.(fd, { titulo, descripcion: desc, file });
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
            <h3 className="text-xl font-bold">Agregar video</h3>
            <button
              onClick={onClose}
              className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300"
            >
              Cerrar
            </button>
          </div>

          <form onSubmit={handleSubmit} className="p-6 space-y-6">
            <div className="bg-gray-100 p-6 rounded-md space-y-4">
              {/* Título */}
              <div>
                <label className="block text-sm font-semibold mb-1">
                  Título del video
                </label>
                <input
                  value={titulo}
                  onChange={(e) => setTitulo(e.target.value)}
                  required
                  className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                  placeholder="Ej: Clase 1 - Introducción"
                />
              </div>

              {/* Descripción */}
              <div>
                <label className="block text-sm font-semibold mb-1">
                  Descripción (opcional)
                </label>
                <textarea
                  rows={3}
                  value={desc}
                  onChange={(e) => setDesc(e.target.value)}
                  className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"
                  placeholder="Breve descripción del video…"
                />
              </div>

              {/* Dropzone + botón subir */}
              <div className="grid grid-cols-1 sm:grid-cols-[1fr,200px] gap-4">
                {/* Dropzone / Preview */}
                <div
                  onDragOver={(e) => e.preventDefault()}
                  onDragEnter={() => setOver(true)}
                  onDragLeave={() => setOver(false)}
                  onDrop={handleDrop}
                  className={[
                    "min-h-40 rounded-md border-2 border-dashed bg-white flex items-center justify-center p-3",
                    over ? "border-duocceleste bg-blue-50" : "border-gray-300",
                  ].join(" ")}
                >
                  {!file ? (
                    <div className="text-center select-none">
                      <div className="text-5xl mb-2">🎞️</div>
                      <p className="text-sm text-gray-700">
                        Arrastra tu video aquí o usa el botón de “Subir archivo”.
                      </p>
                      <p className="text-xs text-gray-500 mt-1">
                        Tipos: {accept} — Máx: {maxMB} MB
                      </p>
                    </div>
                  ) : (
                    <div className="w-full">
                      <video
                        className="w-full rounded-md"
                        src={URL.createObjectURL(file)}
                        controls
                      />
                      <p className="text-xs text-gray-600 mt-2">
                        {file.name} — {(file.size / (1024 * 1024)).toFixed(1)} MB
                      </p>
                      <button
                        type="button"
                        onClick={() => setFile(null)}
                        className="mt-2 text-sm text-duocceleste underline"
                      >
                        Quitar archivo
                      </button>
                    </div>
                  )}
                </div>

                {/* Botón subir */}
                <div className="flex items-center">
                  <label className="inline-flex w-full justify-center rounded-md bg-duocceleste text-white font-semibold px-4 py-3 cursor-pointer hover:opacity-90">
                    Subir archivo
                    <input
                      type="file"
                      accept={accept}
                      onChange={handlePick}
                      className="hidden"
                    />
                  </label>
                </div>
              </div>

              {error && <p className="text-red-600 text-sm">{error}</p>}
            </div>

            {/* Acciones */}
            <div className="flex items-center justify-end gap-3">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 rounded-md bg-gray-200 hover:bg-gray-300"
              >
                Cancelar
              </button>
              <button
                type="submit"
                className="px-4 py-2 rounded-md bg-duocamarillo text-duocgris font-semibold hover:bg-duocceleste hover:text-white"
              >
                Guardar video
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
