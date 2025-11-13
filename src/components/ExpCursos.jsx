import React from "react";
import mecanicaImg from "../assets/mecanica.png"; // reemplazá con tus imágenes locales
import construccionImg from "../assets/construccion.png";

function ExpCursos() {
  return (
    <section className="p-8 flex gap-3 flex-wrap">
      <h2 className="text-3xl font-bold mb-8 ">Escuela de Construcción</h2>
      {/* Contenedor de cartas */}
      <div className="flex gap-3 flex-wrap">
        {/* Carta 1 */}
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
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>

        {/* Carta 2 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={construccionImg}
            alt="Curso de Construcción"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">
              TÉCNICAS PARA LA CONSTRUCCIÓN
            </h3>
            <p className="text-xs mb-0.5">Profesor: Ezequiel Morales</p>
            <p className="text-xs mb-3">Cantidad de episodios: 12</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>

        {/* Carta 3 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>
        {/* Carta 4 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>
      </div>
      <h2 className="text-3xl font-bold mb-8">Escuela de Mecánica</h2>

      {/* Contenedor de cartas */}
      <div className="flex gap-3 flex-wrap">
        {/* Carta 1 */}
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
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>

        {/* Carta 2 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={construccionImg}
            alt="Curso de Construcción"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">
              TÉCNICAS PARA LA CONSTRUCCIÓN
            </h3>
            <p className="text-xs mb-0.5">Profesor: Ezequiel Morales</p>
            <p className="text-xs mb-3">Cantidad de episodios: 12</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>

        {/* Carta 3 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>
        {/* Carta 4 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button className="bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded hover:bg-duocceleste hover:text-white transition-colors">
              Inscribir
            </button>
          </div>
        </div>
      </div>
      <h2 className="text-3xl font-bold mb-8">Cursos ya inscritos</h2>

      {/* Contenedor de cartas */}
      <div className="flex gap-3 flex-wrap">
        {/* Carta 1 */}
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
            <button disabled className="disabled  shadow-md text-duocgris disabled:opacity-50 disabled:cursor-not-allowed bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded">
              Ya inscrito
            </button>
          </div>
        </div>

        {/* Carta 2 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={construccionImg}
            alt="Curso de Construcción"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">
              TÉCNICAS PARA LA CONSTRUCCIÓN
            </h3>
            <p className="text-xs mb-0.5">Profesor: Ezequiel Morales</p>
            <p className="text-xs mb-3">Cantidad de episodios: 12</p>
          </div>
          <div className="flex justify-end">
            <button disabled className="disabled  shadow-md text-duocgris disabled:opacity-50 disabled:cursor-not-allowed bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded">
              Ya inscrito
            </button>
          </div>
        </div>

        {/* Carta 3 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button disabled className="disabled  shadow-md text-duocgris disabled:opacity-50 disabled:cursor-not-allowed bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded">
              Ya inscrito
            </button>
          </div>
        </div>
        {/* Carta 4 */}
        <div className="bg-duocazul text-white p-3 rounded-md shadow-md hover:shadow-xl hover:-translate-y-1 transition-all duration-300 w-80">
          <img
            src={mecanicaImg}
            alt="Curso de Electricidad"
            className="rounded-md mb-3 h-36 w-full object-cover"
          />
          <div>
            <h3 className="text-base font-semibold mb-1">ELECTRICIDAD DOMICILIARIA</h3>
            <p className="text-xs mb-0.5">Profesor: Ricardo Fernández</p>
            <p className="text-xs mb-3">Cantidad de episodios: 8</p>
          </div>
          <div className="flex justify-end">
            <button disabled className="disabled  shadow-md text-duocgris disabled:opacity-50 disabled:cursor-not-allowed bg-duocamarillo text-duocgris font-semibold text-sm px-3 py-1.5 rounded">
              Ya inscrito
            </button>
          </div>
        </div>
      </div>

    </section>

  );
}

export default ExpCursos;