import logoDuoc from "../assets/DuocUCLogo.png";
import { NavLink } from "react-router-dom";
function Navbar() {
  const baseBtn = "block w-full text-center rounded-md px-4 py-4 shadow-md transition-colors";
  const activeBtn = "bg-duocamarillo text-duocgris";
  const inactiveBtn = "bg-duocgris hover:bg-duocamarillo hover:text-duocgris";

  return (

     <aside className="fixed left-0 top-0 h-screen w-64 bg-duocazul text-white p-4 z-50">
      
    <div className="flex flex-col h-full">
      <div className="flex justify-center mb-6">
        <img 
          src={logoDuoc}
          alt="Logo" 
          className="full" 
        />
      </div>

      <div className="flex flex-col space-y-4">
       <NavLink
          to="/profesor"
          className={({ isActive }) => `${baseBtn} ${isActive ? activeBtn : inactiveBtn}`}
        >
          Mis cursos
        </NavLink>

       <NavLink
          to="/profesor/Evaluaciones"
          className={({ isActive }) => `${baseBtn} ${isActive ? activeBtn : inactiveBtn}`}
        >
          Evaluaciones
        </NavLink>

       <NavLink
          to="/profesor/Calificaciones"
          className={({ isActive }) => `${baseBtn} ${isActive ? activeBtn : inactiveBtn}`}
        >
          Calificaciones
        </NavLink>
    
      </div>
      
      <div className="mt-auto pt-4 border-t border-white/10">
      <NavLink
          to="/Login"
        >
          
          <button className="w-full rounded-md bg-duocamarillo text-duocgris font-semibold px-3 py-4 hover:bg-duocceleste hover:text-white transition-colors">
            Cerrar sesión
          </button>
        </NavLink>
        </div>
    </div>
    </aside>
  );
}

export default Navbar;
