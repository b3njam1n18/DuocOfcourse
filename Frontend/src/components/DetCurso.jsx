import React from "react";
import mecanicaImg from "../assets/mecanica.png";
import { NavLink } from "react-router-dom";
import ProgressCircle from "../components/Progreso"


function DetCurso() {
  const baseBtn =
    "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors";
  const progress = 10; // ← aquí seteas tu porcentaje real

  return (
    <section className="p-8">
      {/* Título */}
      <h2 className="text-3xl font-bold mb-8">
        Mecánica básica - Manuel Gallegos
      </h2>

      {/* Layout con columna lateral derecha */}
      <div className="grid grid-cols-1 lg:grid-cols-[1fr,280px] gap-6">
        {/* Columna izquierda (tus cartas) */}
        <div className="flex gap-3 flex-wrap">
          {/* Carta 1 */}
          <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
            <img
              src={mecanicaImg}
              alt="Curso de Mecánica"
              className="rounded-md mb-3 h-36 w-full object-cover"
            />
            <div>
              <h3 className="text-base font-semibold mb-1">
                1. Conociendo tu vehículo
              </h3>
              <p className="text-xs mb-3">
                Descripción: Introducción a las partes principales de un
                automóvil: motor, transmisión, sistema eléctrico y suspensión.
                Ideal para familiarizarse con los nombres y funciones básicas.
              </p>
              <p className="text-xs mb-3">Duración: 12 minutos</p>
              <p className="text-xs mb-3">Visto: Sí</p>
              <p className="text-xs mb-3">Progreso: 75%</p>
            </div>
            <div className="flex justify-end">
              <NavLink to="/Clase" className={baseBtn}>
                ACCEDER
              </NavLink>
            </div>
          </div>

          {/* Duplica/ajusta las demás cartas como ya las tenías... */}
          {/* Carta 2 */}
          <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
            <img
              src={mecanicaImg}
              alt="Curso de Mecánica"
              className="rounded-md mb-3 h-36 w-full object-cover"
            />
            <div>
              <h3 className="text-base font-semibold mb-1">
                2. Conociendo tu vehículo
              </h3>
              <p className="text-xs mb-3">
                Descripción: Introducción a las partes principales de un
                automóvil: motor, transmisión, sistema eléctrico y suspensión.
                Ideal para familiarizarse con los nombres y funciones básicas.
              </p>
              <p className="text-xs mb-3">Duración: 12 minutos</p>
              <p className="text-xs mb-3">Visto: Sí</p>
              <p className="text-xs mb-3">Progreso: 75%</p>
            </div>
            <div className="flex justify-end">
              <NavLink to="/DetalleCurso" className={baseBtn}>
                ACCEDER
              </NavLink>
            </div>
          </div>

          {/* Carta 3 */}
          <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
            <img
              src={mecanicaImg}
              alt="Curso de Mecánica"
              className="rounded-md mb-3 h-36 w-full object-cover"
            />
            <div>
              <h3 className="text-base font-semibold mb-1">
                3. Conociendo tu vehículo
              </h3>
              <p className="text-xs mb-3">
                Descripción: Introducción a las partes principales de un
                automóvil: motor, transmisión, sistema eléctrico y suspensión.
                Ideal para familiarizarse con los nombres y funciones básicas.
              </p>
              <p className="text-xs mb-3">Duración: 12 minutos</p>
              <p className="text-xs mb-3">Visto: Sí</p>
              <p className="text-xs mb-3">Progreso: 75%</p>
            </div>
            <div className="flex justify-end">
              <NavLink to="/DetalleCurso" className={baseBtn}>
                ACCEDER
              </NavLink>
            </div>
          </div>

          {/* Carta 4 */}
          <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
            <img
              src={mecanicaImg}
              alt="Curso de Mecánica"
              className="rounded-md mb-3 h-36 w-full object-cover"
            />
            <div>
              <h3 className="text-base font-semibold mb-1">
                4. Conociendo tu vehículo
              </h3>
              <p className="text-xs mb-3">
                Descripción: Introducción a las partes principales de un
                automóvil: motor, transmisión, sistema eléctrico y suspensión.
                Ideal para familiarizarse con los nombres y funciones básicas.
              </p>
              <p className="text-xs mb-3">Duración: 12 minutos</p>
              <p className="text-xs mb-3">Visto: Sí</p>
              <p className="text-xs mb-3">Progreso: 75%</p>
            </div>
            <div className="flex justify-end">
              <NavLink to="/DetalleCurso" className={baseBtn}>
                ACCEDER
              </NavLink>
            </div>
          </div>
        </div>

        {/* Columna lateral derecha */}
        <aside className="h-fit lg:sticky lg:top-20 bg-white rounded-md border p-4">
          <p className="text-xl font-semibold mb-3">Progreso: {progress}%</p>

          <ProgressCircle value={progress} size={120} stroke={12} />

          <button
            disabled
            className="w-full mt-4 rounded bg-gray-300 text-gray-600 py-2 cursor-not-allowed
                       disabled:opacity-100 disabled:cursor-not-allowed"
          >
            Examen final
          </button>

          <p className="text-sm text-gray-700 mt-3">
            Necesitas tener el 100% de progreso total para dar el examen final.
          </p>
        </aside>
      </div>
    </section>
  );
}

export default DetCurso;
