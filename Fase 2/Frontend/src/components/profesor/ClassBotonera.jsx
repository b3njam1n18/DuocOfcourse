import ProgressCircle from "../Progreso"
import BotoneraVertical from "../Botonera"
import { NavLink } from "react-router-dom";

function Class() {
  return (
    <section>
            <p className="font-bold">1. Lorem ipsum dolor sit amet consectetur adipisicing elit. Dignissimos, nam?</p>
            <BotoneraVertical
              opciones={["Sí", "No", "Ni sí ni no", "Ni no ni sí"]}
              permitirDesmarcar
            />

    </section>
  );
}

export default Class;
