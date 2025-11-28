import { useMemo, useState } from "react";
import { useNavigate, Link } from "react-router-dom";

export default function Register() {
  const nav = useNavigate();

  // catálogos (ajústalos a lo tuyo / API)
  const escuelas = ["Escuela Ingeniería", "Escuela Construcción", "Escuela Salud"];
  const carrerasPorEscuela = {
    "Escuela Ingeniería": ["Informática", "Industrial", "Mecánica"],
    "Escuela Construcción": ["Edificación", "Prevención de Riesgos"],
    "Escuela Salud": ["Enfermería", "Kinesiología"],
  };

  const [form, setForm] = useState({
    nombre: "",
    email: "",
    escuela: "",
    carrera: "",
    password: "",
  });
  const [showPwd, setShowPwd] = useState(false);
  const [loading, setLoading] = useState(false);

  const carreras = useMemo(
    () => (form.escuela ? carrerasPorEscuela[form.escuela] || [] : []),
    [form.escuela]
  );

  const onChange = (e) => {
    const { name, value } = e.target;
    setForm((f) => ({
      ...f,
      [name]: value,
      ...(name === "escuela" ? { carrera: "" } : null), // reset carrera si cambia escuela
    }));
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      // TODO: POST a tu backend con form
      // await api.register(form)
      nav("/cursos"); // redirige después de registrar
    } catch (err) {
      alert("No se pudo registrar");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="w-full max-w-md bg-white rounded-xl border shadow p-6">
      <h1 className="text-2xl font-bold mb-1">Crear cuenta</h1>
      <p className="text-sm text-gray-600 mb-6">Regístrate en DuocOfCourse</p>

      <form onSubmit={onSubmit} className="space-y-4">
        {/* Nombre */}
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="nombre">Nombre:</label>
          <input
            id="nombre" name="nombre" value={form.nombre} onChange={onChange} required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="Tu nombre completo"
          />
        </div>

        {/* Correo */}
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="email">Correo:</label>
          <input
            id="email" name="email" type="email" value={form.email} onChange={onChange} required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="tucorreo@duoc.cl"
          />
        </div>

        {/* Escuela */}
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="escuela">Seleccione escuela</label>
          <select
            id="escuela" name="escuela" value={form.escuela} onChange={onChange} required
            className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
          >
            <option value="" disabled>— Seleccione —</option>
            {escuelas.map((e) => <option key={e} value={e}>{e}</option>)}
          </select>
        </div>

        {/* Carrera dependiente */}
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="carrera">Seleccione carrera</label>
          <select
            id="carrera" name="carrera" value={form.carrera} onChange={onChange}
            required disabled={!form.escuela}
            className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo disabled:bg-gray-100"
          >
            <option value="" disabled>— Seleccione —</option>
            {carreras.map((c) => <option key={c} value={c}>{c}</option>)}
          </select>
        </div>

        {/* Contraseña + mostrar */}
        <div>
          <label className="block text-sm font-medium mb-1" htmlFor="password">Contraseña:</label>
          <input
            id="password" name="password" type={showPwd ? "text" : "password"}
            value={form.password} onChange={onChange} required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="****************"
          />
          <label className="mt-2 inline-flex items-center gap-2 text-sm">
            <input type="checkbox" checked={showPwd} onChange={(e)=>setShowPwd(e.target.checked)} />
            Mostrar contraseña
          </label>
        </div>

        {/* Botón registrarse (estilo botón grande azul redondeado) */}
        <div className="pt-2 flex justify-end">
          <button
            type="submit"
            disabled={loading}
            className={[
              "rounded-full px-8 py-3 text-white text-lg",
              loading
                ? "bg-gray-300 cursor-not-allowed"
                : "bg-duocceleste hover:opacity-90",
            ].join(" ")}
          >
            {loading ? "Registrando..." : "Registrarse"}
          </button>
        </div>
      </form>

      <p className="text-center text-sm text-gray-600 mt-6">
        ¿Ya tienes cuenta?{" "}
        <Link to="/Login" className="text-duocceleste hover:underline">Inicia sesión</Link>
      </p>
    </div>
  );
}
