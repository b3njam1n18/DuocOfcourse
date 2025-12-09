import React, { useState } from "react";

export default function BotoneraVertical({
  opciones = [],               // ["Opción 1", "Opción 2", ...]
  permitirDesmarcar = false,   // opcional
  onSelect,                    // opcional: (index) => void
  className = "",              // opcional: estilos extra
  disabled = false,            // ⬅️ importante para bloquear clics
  selectedIndex: controlledSelectedIndex, // índice marcado desde afuera
  correctIndexes = [],         // índices de opciones correctas
}) {
  const [internalSelected, setInternalSelected] = useState(-1);

  // Si viene controlado desde afuera, usamos ese. Si no, usamos el interno.
  const selected = 
    typeof controlledSelectedIndex === "number"
      ? controlledSelectedIndex
      : internalSelected;

  const handleClick = (i) => {
    if (disabled) return;

    const next =
      permitirDesmarcar && selected === i
        ? -1
        : i;

    if (typeof controlledSelectedIndex !== "number") {
      setInternalSelected(next);
    }

    onSelect?.(next);
  };

  return (
    <div className={`flex flex-col gap-2 w-64 ${className}`}>
      {opciones.map((label, i) => {
        const isSelected = selected === i;
        const isCorrect = correctIndexes.includes(i);

        let colorClasses;

        if (disabled && isCorrect) {
          // modo revisión → correctas en verde
          colorClasses = "bg-emerald-500 text-white";
        } else if (disabled && isSelected && !isCorrect) {
          // lo que el alumno marcó pero era incorrecto → rojo
          colorClasses = "bg-red-500 text-white";
        } else if (isSelected) {
          // selección mientras está contestando
          colorClasses =
            "bg-duocamarillo text-duocgris hover:bg-duocazul hover:text-duocamarillo";
        } else {
          colorClasses =
            "bg-duocgris text-white hover:bg-duocazul hover:text-duocamarillo";
        }

        return (
          <button
            key={`${label}-${i}`}
            type="button"
            disabled={disabled}
            aria-pressed={isSelected}
            onClick={() => handleClick(i)}
            className={[
              "w-full px-4 py-3 rounded-md font-semibold transition-colors",
              colorClasses,
              disabled ? "cursor-default" : "",
            ].join(" ")}
          >
            {label}
          </button>
        );
      })}
    </div>
  );
}
