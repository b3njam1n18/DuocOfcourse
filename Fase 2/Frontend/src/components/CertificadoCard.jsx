import React from "react";
import mecanicaImg from "../assets/mecanica.png"; // cambia por tu imagen

export default function CertificadoCard() {
  return (
    <div className="border-2 border-duocceleste bg-gray-200 p-6 md:p-8">
      <div className="grid grid-cols-1 lg:grid-cols-[1fr,420px] gap-6 items-center">
        {/* Izquierda: mensaje + botón */}
        <div>
          <h3 className="text-2xl md:text-3xl font-extrabold tracking-tight">
            FELICIDADES, ALUMNO
          </h3>
          <p className="mt-3 text-lg font-semibold">
            ¡comparte tus logros con un certificado!
          </p>

          <button
            className="mt-8 inline-flex items-center justify-center rounded bg-duocamarillo
                       text-duocgris px-6 py-3 font-semibold hover:bg-duocceleste hover:text-white transition-colors"
          >
            Descargar
          </button>
        </div>

        {/* Derecha: tarjeta con imagen y título */}
        <div className="bg-white p-5 shadow-md">
          <img
            src={mecanicaImg}
            alt="Mecánica Básica"
            className="w-full h-44 object-cover mb-4"
          />
          <h4 className="text-3xl md:text-4xl font-extrabold">
            Mecanica Basica
          </h4>
        </div>
      </div>
    </div>
  );
}
