/* ---- Componente de progreso circular (SVG) ---- */
function ProgressCircle({ value = 10, size = 120, stroke = 12 }) {
  const radius = (size - stroke) / 2;
  const c = 2 * Math.PI * radius;
  const offset = c * (1 - value / 100);

  return (
    <svg width={size} height={size} className="block mx-auto">
      {/* Track (fondo) */}
      <circle
        cx={size / 2}
        cy={size / 2}
        r={radius}
        stroke="currentColor"
        className="text-duocazul"
        strokeWidth={stroke}
        fill="none"
        strokeLinecap="round"
        strokeDasharray={c}
        strokeDashoffset={0}
      />
      {/* Progreso */}
      <circle
        cx={size / 2}
        cy={size / 2}
        r={radius}
        stroke="currentColor"
        className="text-duocamarillo"
        strokeWidth={stroke}
        fill="none"
        strokeLinecap="round"
        strokeDasharray={c}
        strokeDashoffset={offset}
        style={{ transform: "rotate(-90deg)", transformOrigin: "50% 50%" }}
      />
    </svg>
  );
}


export default ProgressCircle;