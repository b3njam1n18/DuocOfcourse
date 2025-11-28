import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import candado from "../assets/candado.png";

export default function Login() {
  const nav = useNavigate();
  const [form, setForm] = useState({ email: "", password: "" });
  const [loading, setLoading] = useState(false);
  const btnBase = "w-full py-2 rounded-md font-semibold transition-colors";
  return (
    <div className="w-full max-w-md items-center bg-white rounded-xl border shadow p-6">
          <img src={candado} alt="Imagen superior" className="h-40 mx-auto block mb-4" />
      <h1 className="text-2xl font-bold mb-1">Restablecer contraseña</h1>
      <form className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="email">Ingrese el correo electrónico asociado</label>
          <input
            id="email"
            name="email"
            type="email"
            value={form.email}
            
            required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="tucorreo@duoc.cl"
          />
        </div>

        <Link to="/Login" className="text-duocceleste hover:underline"><button
          type="submit"
          className={[
            btnBase,
            loading
              ? "bg-gray-300 text-gray-500 cursor-not-allowed"
              : "bg-duocamarillo text-duocgris hover:bg-duocceleste hover:text-white",
          ].join(" ")}
        >
          {loading ? "Ingresando..." : "Ingresar"}
        </button></Link>
        
      </form>

      <p className="text-center text-sm text-gray-600 mt-6">
        
        <Link to="/login" className="text-duocceleste hover:underline">Volver a inicio de sesión</Link>
      </p>
    </div>
  );
}
