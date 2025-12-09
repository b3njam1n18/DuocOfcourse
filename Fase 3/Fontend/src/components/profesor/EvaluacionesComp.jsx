import React from "react";
import mecanicaImg from "../../assets/mecanica.png";
import { NavLink } from "react-router-dom";

console.log("URL REAL:", window.location.href);

function EvaluacionesComp({ evaluation, courseId }) {
  const baseBtn =
    "bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors";

  const fechaLimite = evaluation?.dueAt
    ? new Date(evaluation.dueAt).toLocaleDateString("es-CL")
    : "Sin fecha límite";

  const intentosTexto =
    evaluation?.attemptLimit ??
    evaluation?.maxAttempts ??
    "No definido";

  return (
    <section>
      <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
        <img
          src={mecanicaImg}
          alt="Curso"
          className="rounded-md mb-3 h-36 w-full object-cover"
        />
        <div>
          <h3 className="text-base font-semibold mb-1">
            {evaluation?.title || "Evaluación sin título"}
          </h3>
          <p className="text-xs mb-0.5">Fecha límite: {fechaLimite}</p>
          <p className="text-xs mb-3">
            Cantidad de intentos: {intentosTexto}
          </p>
        </div>

        <div className="mt-4 flex items-center justify-between">
          <button
            type="button"
            onClick={() => console.log("eliminar", evaluation?.id)}
            className="inline-flex items-center justify-center w-10 h-10 rounded-md bg-duocamarillo text-duocgris hover:bg-duocceleste hover:text-white"
          >
            🗑
          </button>

          <NavLink
            to={`/profesor/EditarEvaluacion?evaluationId=${evaluation?.id}&courseId=${courseId}`}
            className={baseBtn}
          >
            EDITAR
          </NavLink>
        </div>
      </div>
    </section>
  );
}

export default EvaluacionesComp;
