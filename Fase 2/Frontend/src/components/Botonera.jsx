import React, { useState } from "react";

export default function BotoneraVertical({
  opciones = [],              // ["Opción 1", "Opción 2", ...]
  cantidad,                   // opcional: limita cuántos renderizar
  permitirDesmarcar = false,  // opcional
  onSelect,                   // opcional: (index, label) => void
  className = ""              // opcional: estilos extra
}) {
  const [activo, setActivo] = useState(-1);
  const items = Number.isFinite(cantidad) ? opciones.slice(0, cantidad) : opciones;

  const handleClick = (i) => {
    const next = permitirDesmarcar && activo === i ? -1 : i;
    setActivo(next);
    onSelect?.(next, next >= 0 ? items[next] : null);
  };

  return (
    <div className={`flex flex-col gap-2 w-64 ${className}`}>
      {items.map((label, i) => {
        const isActive = activo === i;
        return (
          <button
            key={`${label}-${i}`}
            aria-pressed={isActive}
            onClick={() => handleClick(i)}
            className={[
              "w-full px-4 py-3 rounded-md font-semibold transition-colors",
              isActive
                ? "bg-duocamarillo text-duocgris hover:bg-duocazul hover:text-duocamarillo"
                : "bg-duocgris text-white hover:bg-duocazul hover:text-duocamarillo",
            ].join(" ")}
          >
            {label}
          </button>
        );
      })}
    </div>
  );
}
