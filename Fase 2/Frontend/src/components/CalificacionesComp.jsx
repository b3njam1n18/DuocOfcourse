import React from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { NavLink } from "react-router-dom";

function CalificacionesComp() {
  const baseBtn = "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors"
  return (
    <section>
      <h2 className="text-3xl font-bold mb-8">Ver calificaciones</h2>
      <h2 className="text-3xl font-bold mb-8">Escuela de ingenieria y recursos Naturales</h2>
      <div className="flex gap-3 flex-wrap">
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Mecánica"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">MECÁNICA BÁSICA</h3>
            <p className="text-xs mb-0.5">Nota Final: 6.5</p>
            <p className="text-xs mb-3">Estado: APROBADO</p>
          </div>
        </div>

        
      </div>
    </section>
  );
}

export default CalificacionesComp;