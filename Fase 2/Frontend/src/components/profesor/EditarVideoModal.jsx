import React, { useState, useMemo } from "react";

export default function EditarVideoModal({ open, onClose, onSubmit, initial, maxMB = 200 }) {
  // initial: { id, titulo, descripcion, src }  (src = URL servida de tu backend)
  const [titulo, setTitulo] = useState(initial?.titulo || "");
  const [desc, setDesc] = useState(initial?.descripcion || "");
  const [file, setFile] = useState(null);
  const [over, setOver] = useState(false);
  const [error, setError] = useState("");

  const previewSrc = useMemo(
    () => (file ? URL.createObjectURL(file) : initial?.src || ""),
    [file, initial]
  );

  if (!open) return null;

  const accept = "video/*";
  const maxBytes = maxMB * 1024 * 1024;
  const validateFile = (f) => {
    if (!f) return "";
    if (!f.type.startsWith("video/")) return "El archivo debe ser de video.";
    if (f.size > maxBytes) return `Máximo ${maxMB} MB.`;
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
    const fd = new FormData();
    fd.append("id", initial.id);
    fd.append("titulo", titulo);
    fd.append("descripcion", desc);
    if (file) fd.append("video", file);      // solo si reemplaza video
    onSubmit?.(fd, { id: initial.id, titulo, descripcion: desc, replaceFile: !!file, file });
    onClose();
  };

  return (
    <div className="fixed inset-0 z-50">
      <div className="absolute inset-0 bg-black/50" onClick={onClose} />
      <div className="relative mx-auto mt-10 w-full max-w-3xl">
        <div className="rounded-xl bg-white shadow-xl overflow-hidden">
          <div className="flex items-center justify-between px-6 py-4 border-b">
            <h3 className="text-xl font-bold">Editar video</h3>
            <button onClick={onClose} className="px-3 py-1 rounded-md bg-gray-200 hover:bg-gray-300">Cerrar</button>
          </div>

          <form onSubmit={handleSubmit} className="p-6 space-y-6">
            <div className="bg-gray-100 p-6 rounded-md space-y-4">
              <div>
                <label className="block text-sm font-semibold mb-1">Título</label>
                <input value={titulo} onChange={(e)=>setTitulo(e.target.value)} required
                  className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"/>
              </div>

              <div>
                <label className="block text-sm font-semibold mb-1">Descripción</label>
                <textarea rows={3} value={desc} onChange={(e)=>setDesc(e.target.value)}
                  className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo bg-white"/>
              </div>

              <div className="grid grid-cols-1 sm:grid-cols-[1fr,200px] gap-4">
                <div
                  onDragOver={(e)=>e.preventDefault()}
                  onDragEnter={()=>setOver(true)}
                  onDragLeave={()=>setOver(false)}
                  onDrop={handleDrop}
                  className={[
                    "min-h-40 rounded-md border-2 border-dashed bg-white flex items-center justify-center p-3",
                    over ? "border-duocceleste bg-blue-50" : "border-gray-300",
                  ].join(" ")}
                >
                  {previewSrc ? (
                    <video className="w-full rounded-md" src={previewSrc} controls />
                  ) : (
                    <div className="text-center text-gray-500">Sin vista previa</div>
                  )}
                </div>
                <div className="flex items-center">
                  <label className="inline-flex w-full justify-center rounded-md bg-duocceleste text-white font-semibold px-4 py-3 cursor-pointer hover:opacity-90">
                    Reemplazar video
                    <input type="file" accept={accept} onChange={handlePick} className="hidden" />
                  </label>
                </div>
              </div>

              {error && <p className="text-red-600 text-sm">{error}</p>}
            </div>

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
