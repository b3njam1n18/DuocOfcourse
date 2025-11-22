import React from "react";
import mecanicaImg from "../assets/mecanica.png";
import construccionImg from "../assets/construccion.png";
import { NavLink } from "react-router-dom";

function Cursos() {
  const baseBtn = "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors"
  return (
      <div className="flex gap-3 flex-wrap">
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Mecánica"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">MECÁNICA BÁSICA</h3>
            <p className="text-xs mb-0.5">Profesor: Manuel Gallegos</p>
            <p className="text-xs mb-3">Cantidad de episodios: 10</p>
          </div>
          <div className="flex justify-end">
            <div className="flex justify-end">
            <NavLink to="/DetalleCurso" className={baseBtn}> ACCEDER </NavLink>
            </div>
          </div>
        </div>
      </div>
    
  );
}

export default Cursos;