import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { login as loginApi } from "../services/api";

export default function Login() {
  const nav = useNavigate();
  const [form, setForm] = useState({ email: "", password: "" });
  const [loading, setLoading] = useState(false);

  const onChange = (e) =>
    setForm({ ...form, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const { data } = await loginApi(form.email, form.password);
      console.log("LOGIN OK, respuesta API:", data);

      // 👇 aquí armamos el nombre completo (según lo que devuelva tu API)
      const fullName =
        data.fullName || // si el backend ya manda fullName
        [data.firstName, data.middleName, data.lastName, data.secondLastName]
          .filter(Boolean)
          .join(" ") || // si manda nombres separados
        data.email; // fallback: el correo

      // Guardar token
      localStorage.setItem("token", data.token);

      // Guardar usuario con nombre
      localStorage.setItem(
        "user",
        JSON.stringify({
          userId: data.userId,
          email: data.email,
          roleId: data.roleId,
          fullName: fullName,
        })
      );

      console.log("Token guardado:", localStorage.getItem("token"));

      // Redirecciones por rol
      if (data.roleId === 2) {
        console.log("Rol alumno → /app");
        nav("/app");
      } else if (data.roleId === 3) {
        console.log("Rol profe → /profesor");
        nav("/profesor");
      } else {
        console.log("Rol desconocido, data.roleId =", data.roleId);
      }
    } catch (err) {
      console.error("Error en login:", err);
      alert("Credenciales inválidas");
    } finally {
      setLoading(false);
    }
  };

  const btnBase = "w-full py-2 rounded-md font-semibold transition-colors";

  return (
    <div className="w-full max-w-md bg-white rounded-xl border shadow p-6">
      <h1 className="text-2xl font-bold mb-1">Iniciar sesión</h1>
      <p className="text-sm text-gray-600 mb-6">
        Accede a tu cuenta de DuocOfCourse
      </p>

      <form onSubmit={onSubmit} className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="email">
            Correo
          </label>
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
          <label
            className="block text-sm font-medium mb-1"
            htmlFor="password"
          >
            Contraseña
          </label>
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
          <Link
            to="/auth/recuperar"
            className="text-duocceleste hover:underline"
          >
            ¿Olvidaste tu contraseña?
          </Link>
        </div>

        <button
          type="submit"
          disabled={loading}
          className={[
            btnBase,
            loading
              ? "bg-gray-300 text-gray-500 cursor-not-allowed"
              : "bg-duocamarillo text-duocgris hover:bg-duocceleste hover:text-white",
          ].join(" ")}
        >
          {loading ? "Ingresando..." : "Ingresar"}
        </button>
      </form>

      <p className="text-center text-sm text-gray-600 mt-6">
        ¿No tienes cuenta?{" "}
        <Link
          to="/auth/register"
          className="text-duocceleste hover:underline"
        >
          Crear cuenta
        </Link>
      </p>
    </div>
  );
}
