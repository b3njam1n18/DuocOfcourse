import React from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { NavLink } from "react-router-dom";

function EvaluacionesComp() {
  const baseBtn = "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors"
  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-8">Realizar evaluaciones</h2>
      <div className="flex gap-3 flex-wrap">
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Mecánica"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">MECÁNICA BÁSICA</h3>
            <p className="text-xs mb-0.5">Fecha límite: 31/12/2025</p>
            <p className="text-xs mb-3">Cantidad de intentos: 3</p>
          </div>
          <div className="flex justify-end">
            <div className="flex justify-end">
            <NavLink to="/Evaluacion" className={baseBtn}> ACCEDER </NavLink>
            </div>
          </div>
        </div>

        
      </div>
    </section>
  );
}

export default EvaluacionesComp;