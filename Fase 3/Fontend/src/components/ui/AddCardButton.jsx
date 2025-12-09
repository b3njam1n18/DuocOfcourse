import React from "react";

export default function AddCardButton({
  title = "Agregar clase",
  cta = "Crear",
  onClick,
  className = "",
}) {
  const base =
    "w-80 h-64 rounded-xl border-2 border-dashed border-duocceleste/60 " +
    "bg-white/60 p-6 flex flex-col items-center justify-center gap-3 " +
    "shadow-sm hover:shadow transition-all cursor-pointer select-none " +
    "hover:border-duocceleste";

  return (
    <div
      role="button"
      tabIndex={0}
      onClick={onClick}
      onKeyDown={(e) => (e.key === "Enter" || e.key === " ") && onClick?.()}
      className={[base, className].join(" ")}
    >
      {/* ícono + */}
      <span className="inline-flex h-10 w-10 items-center justify-center rounded-md border text-duocceleste">
        <svg viewBox="0 0 24 24" className="w-6 h-6" fill="none" stroke="currentColor" strokeWidth="2">
          <path d="M12 5v14M5 12h14" />
        </svg>
      </span>

      <p className="text-duocazul font-semibold"> {title} </p>

      <button
        onClick={(e) => { e.stopPropagation(); onClick?.(); }}
        className="mt-2 rounded-md bg-duocamarillo text-duocgris font-semibold px-5 py-2
                   hover:bg-duocceleste hover:text-white transition-colors"
      >
        {cta}
      </button>
    </div>
  );
}
