import ProgressCircle from "../components/Progreso"
import BotoneraVertical from "../components/Botonera"
import { NavLink } from "react-router-dom";

function Class() {
  const progress = 10;

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-8">
        Mecánica básica - Manuel Gallegos
      </h2>
      <div className="grid grid-cols-1 lg:grid-cols-[1fr,280px] gap-6">
        <div className="flex flex-col gap-3">
          <p>Herramientas esenciales del mecánico</p>
            <p>
            Este episodio te enseñará sobre las herramientas básicas que todo mecánico debe tener y saber utilizar. Desde las llaves y dados hasta el gato hidráulico, cubriremos las herramientas más importantes, sus funciones y cómo usarlas de forma segura y eficiente. Aprenderás también las precauciones que debes tomar al manipularlas y cómo organizarlas para un trabajo más ordenado.
            </p>
          <p className="font-bold">Contenido en video:</p>
            <dl>
            <dt className="font-bold">Introducción a las herramientas básicas:</dt>
            <li>Llaves fijas y combinadas</li>
            <li>Dados y trinquetes</li>
            <li>Destornilladores</li>
            <li>Alicates y pinzas</li>
            <dt className="font-bold">Uso del gato hidráulico:</dt>
            <li>Colocación correcta</li>
            <li>Seguridad y técnicas de elevación</li>
            <dt className="font-bold">Cómo organizar y mantener las herramientas:</dt>
            <li>Estuches y cajas de herramientas</li>
            <li>Limpieza y almacenamiento adecuado</li>
            <dt className="font-bold">Consejos para elegir herramientas de calidad:</dt>
            <li>Materiales duraderos</li>
            <li>Herramientas específicas para diferentes tipos de trabajo</li>
            </dl>
            <div className="w-full">
            <iframe width="600" height="400" src="https://www.youtube.com/embed/HFZUAXhdnHk?list=RDZt-1Tw8koDw" title="LE SSERAFIM (르세라핌) &#39;DIFFERENT&#39; OFFICIAL MV" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
            </div>
            <p>Duración: 12 minutos</p>
            <p className="font-bold">Una vez visto el video, responda las siguientes preguntas:</p>
            <p className="font-bold">1. Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, nam?</p>
            <BotoneraVertical
              opciones={["Sí", "No", "Ni sí ni no", "Ni no ni sí"]}
              permitirDesmarcar
            />
            <p className="font-bold">2. Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, nam?</p>
            <BotoneraVertical
              opciones={["Sí", "No", "Ni sí ni no", "Ni no ni sí"]}
              permitirDesmarcar
            />
            <p className="font-bold">3. Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, nam?</p>
            <BotoneraVertical
              opciones={["Sí", "No", "Ni sí ni no", "Ni no ni sí"]}
              permitirDesmarcar
            />
            <p className="font-bold">4. Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, nam?</p>
            <BotoneraVertical
              opciones={["Sí", "No", "Ni sí ni no", "Ni no ni sí"]}
              permitirDesmarcar
            />
        </div>
        <aside className="h-fit lg:sticky lg:top-20 bg-white rounded-md border p-4">
          <p className="text-xl font-semibold mb-3">Progreso: {progress}%</p>
          <ProgressCircle value={progress} size={120} stroke={12} />
          <NavLink to="/DetalleCurso"> <button className="w-full mt-4 rounded bg-duocamarillo text-duocgris font-bold py-2 hover:bg-duocazul hover:text-white"> Enviar resultados </button> </NavLink>
        </aside>
      </div>
    </section>
  );
}

export default Class;
