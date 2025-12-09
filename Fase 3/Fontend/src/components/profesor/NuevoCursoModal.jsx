import React, { useState, useEffect } from "react";

export default function NuevoCursoModal({
  open,
  onClose,
  onSubmit,
  categorias = [],
  escuelas = [],
}) {
  const [titulo, setTitulo] = useState("");
  const [descripcion, setDescripcion] = useState("");
  const [categoryId, setCategoryId] = useState("");
  const [schoolId, setSchoolId] = useState("");
  const [file, setFile] = useState(null);
  const [over, setOver] = useState(false);

  // cuando se cierra, resetea el formulario
  useEffect(() => {
    if (!open) {
      setTitulo("");
      setDescripcion("");
      setCategoryId("");
      setSchoolId("");
      setFile(null);
      setOver(false);
    }
  }, [open]);

  if (!open) return null;

  const handleDrop = (e) => {
    e.preventDefault();
    setOver(false);
    const f = e.dataTransfer.files?.[0];
    if (f) setFile(f);
  };

  const handleBrowse = (e) => {
    const f = e.target.files?.[0];
    if (f) setFile(f);
  };

  const submit = (e) => {
    e.preventDefault();

    // armamos payload para el padre
    onSubmit?.({
      title: titulo,
      description: descripcion,
      categoryId: Number(categoryId),
      schoolId: Number(schoolId),
      file, // la imagen, el padre decide si la sube a /Files
    });

    onClose(); // cierra al enviar
  };

  return (
    <div className="fixed inset-0 z-50">
      {/* fondo */}
      <div className="absolute inset-0 bg-black/50" onClick={onClose} />

      {/* tarjeta */}
      <div className="relative mx-auto mt-10 w-full max-w-3xl">
        <div className="rounded-xl bg-white shadow-xl">
          <div className="flex items-center justify-between px-6 py-4 border-b">
            <h3 className="text-xl font-bold">Crear nuevo curso</h3>
            <button
              className="px-3 py-1 rounded-md bg-duocgris text-white hover:opacity-90"
              onClick={onClose}
            >
              Cerrar
            </button>
          </div>

          <form onSubmit={submit} className="p-6 space-y-5">
            {/* Título */}
            <div>
              <label className="block text-sm font-semibold mb-1">
                Título del curso
              </label>
              <input
                value={titulo}
                onChange={(e) => setTitulo(e.target.value)}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
                placeholder="Ej: Mecánica básica"
              />
            </div>

            {/* Descripción */}
            <div>
              <label className="block text-sm font-semibold mb-1">
                Descripción del curso
              </label>
              <textarea
                value={descripcion}
                onChange={(e) => setDescripcion(e.target.value)}
                rows={4}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
                placeholder="Breve descripción del contenido del curso…"
              />
            </div>

            {/* Categoría */}
            <div>
              <label className="block text-sm font-semibold mb-1">
                Categoría
              </label>
              <select
                value={categoryId}
                onChange={(e) => setCategoryId(e.target.value)}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
              >
                <option value="">Selecciona una categoría</option>
                {categorias.map((c) => (
                  <option key={c.id} value={c.id}>
                    {c.name}
                  </option>
                ))}
              </select>
            </div>

            {/* Escuela */}
            <div>
              <label className="block text-sm font-semibold mb-1">
                Escuela
              </label>
              <select
                value={schoolId}
                onChange={(e) => setSchoolId(e.target.value)}
                required
                className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
              >
                <option value="">Selecciona una escuela</option>
                {escuelas.map((s) => (
                  <option key={s.id} value={s.id}>
                    {s.name}
                  </option>
                ))}
              </select>
            </div>

            {/* Portada con drag & drop */}
            <div>
              <label className="block text-sm font-semibold mb-2">
                Portada del curso
              </label>

              <div
                onDragOver={(e) => e.preventDefault()}
                onDragEnter={() => setOver(true)}
                onDragLeave={() => setOver(false)}
                onDrop={handleDrop}
                className={[
                  "w-full rounded-md border-2 border-dashed p-6 text-center cursor-pointer",
                  over
                    ? "border-duocceleste bg-blue-50"
                    : "border-gray-300 bg-white",
                ].join(" ")}
              >
                {!file ? (
                  <>
                    <div className="text-5xl mb-2">🖼️</div>
                    <p className="text-sm text-gray-700">
                      Arrastra una imagen o{" "}
                      <label className="text-duocceleste underline cursor-pointer">
                        sube una imagen
                        <input
                          type="file"
                          accept="image/*"
                          onChange={handleBrowse}
                          className="hidden"
                        />
                      </label>
                    </p>
                  </>
                ) : (
                  <div className="space-y-2">
                    <img
                      src={URL.createObjectURL(file)}
                      alt="Preview portada"
                      className="mx-auto max-h-48 rounded-md object-contain"
                    />
                    <p className="text-xs text-gray-600">{file.name}</p>
                    <button
                      type="button"
                      onClick={() => setFile(null)}
                      className="text-sm text-duocceleste underline"
                    >
                      Quitar imagen
                    </button>
                  </div>
                )}
              </div>
            </div>

            {/* acciones */}
            <div className="flex items-center justify-end gap-3 pt-2 border-t">
              <button
                type="button"
                onClick={onClose}
                className="px-4 py-2 rounded-md bg-gray-200 text-gray-700 hover:bg-gray-300"
              >
                Cancelar
              </button>
              <button
                type="submit"
                className="px-4 py-2 rounded-md bg-duocamarillo text-duocgris font-semibold hover:bg-duocceleste hover:text-white"
              >
                Guardar curso
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
}
