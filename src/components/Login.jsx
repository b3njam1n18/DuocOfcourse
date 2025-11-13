import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";


export default function Login() {
  const nav = useNavigate();
  const [form, setForm] = useState({ email: "", password: "" });
  const [loading, setLoading] = useState(false);

  const onChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      // TODO: autentica aquí (API / Firebase / etc.)
      // await api.login(form.email, form.password)
      nav("/cursos"); // redirige al home de la app
    } catch (err) {
      alert("Credenciales inválidas");
    } finally {
      setLoading(false);
    }
  };

  const btnBase = "w-full py-2 rounded-md font-semibold transition-colors";
  return (
    <div className="w-full max-w-md bg-white rounded-xl border shadow p-6">
      <h1 className="text-2xl font-bold mb-1">Iniciar sesión</h1>
      <p className="text-sm text-gray-600 mb-6">Accede a tu cuenta de DuocOfCourse</p>

      <form onSubmit={onSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="email">Correo</label>
          <input
            id="email"
            name="email"
            type="email"
            value={form.email}
            onChange={onChange}
            required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="tucorreo@duoc.cl"
          />
        </div>

        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="password">Contraseña</label>
          <input
            id="password"
            name="password"
            type="password"
            value={form.password}
            onChange={onChange}
            required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="********"
          />
        </div>

        <div className="flex items-center justify-between text-sm">
          <Link to="/RecuperarContrasena" className="text-duocceleste hover:underline">¿Olvidaste tu contraseña?</Link>
        </div>
        <Link to="/" className="text-duocceleste hover:underline"><button
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
        ¿No tienes cuenta?{" "}
        <Link to="#" className="text-duocceleste hover:underline">Crear cuenta</Link>
      </p>
    </div>
  );
}
