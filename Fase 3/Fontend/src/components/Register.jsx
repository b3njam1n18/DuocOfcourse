// src/pages/Register.jsx (o donde lo tengas)
import { useEffect, useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { getCategories } from "../services/coursesServices";
import api from "../services/api";

export default function Register() {
  const nav = useNavigate();

  const [categories, setCategories] = useState([]); // course_categories de la API
  const [loadingCategories, setLoadingCategories] = useState(true);

  const [form, setForm] = useState({
    firstName: "",
    middleName: "",
    lastName: "",
    secondLastName: "",
    email: "",
    courseCategoryId: "",
    password: "",
  });

  const [showPwd, setShowPwd] = useState(false);
  const [loading, setLoading] = useState(false);
  const [errorMsg, setErrorMsg] = useState(null);

  // Cargar carreras (course_categories) al montar
  useEffect(() => {
    const loadCategories = async () => {
      try {
        setLoadingCategories(true);
        const { data } = await getCategories(); // GET /Categories
        setCategories(data || []);
      } catch (err) {
        console.error(err);
        setErrorMsg(
          err.response?.data?.message ||
            "No se pudieron cargar las carreras."
        );
      } finally {
        setLoadingCategories(false);
      }
    };

    loadCategories();
  }, []);

  const onChange = (e) => {
    const { name, value } = e.target;
    setForm((f) => ({
      ...f,
      [name]: value,
    }));
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setErrorMsg(null);

    try {
      // armamos el payload que espera el backend
      const payload = {
        firstName: form.firstName,
        middleName: form.middleName || null,
        lastName: form.lastName || null,
        secondLastName: form.secondLastName || null,
        email: form.email,
        password: form.password,
        roleId: 2, // 2 = alumno (ajusta si tu backend usa otro valor)
        courseCategoryId: Number(form.courseCategoryId),
      };

      if (!payload.courseCategoryId) {
        throw new Error("Debes seleccionar una carrera.");
      }

      // AJUSTA la URL si tu endpoint de registro es otro:
      // por ejemplo /Auth/register o /Users
      await api.post("/Auth/register", payload);

      nav("/cursos"); // redirige después de registrar
    } catch (err) {
      console.error(err);
      setErrorMsg(
        err.response?.data?.message ||
          err.message ||
          "No se pudo registrar el usuario."
      );
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="w-full max-w-md bg-white rounded-xl border shadow p-6">
      <h1 className="text-2xl font-bold mb-1">Crear cuenta</h1>
      <p className="text-sm text-gray-600 mb-6">Regístrate en DuocOfCourse</p>

      {errorMsg && (
        <p className="mb-4 text-sm text-red-500">
          {errorMsg}
        </p>
      )}

      <form onSubmit={onSubmit} className="space-y-4">
        {/* Nombres */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
          <div>
            <label
              className="block text-sm font-medium mb-1"
              htmlFor="firstName"
            >
              Primer nombre
            </label>
            <input
              id="firstName"
              name="firstName"
              value={form.firstName}
              onChange={onChange}
              required
              className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            />
          </div>

          <div>
            <label
              className="block text-sm font-medium mb-1"
              htmlFor="middleName"
            >
              Segundo nombre
            </label>
            <input
              id="middleName"
              name="middleName"
              value={form.middleName}
              onChange={onChange}
              className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            />
          </div>
        </div>

        {/* Apellidos */}
        <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
          <div>
            <label
              className="block text-sm font-medium mb-1"
              htmlFor="lastName"
            >
              Primer apellido
            </label>
            <input
              id="lastName"
              name="lastName"
              value={form.lastName}
              onChange={onChange}
              required
              className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            />
          </div>

          <div>
            <label
              className="block text-sm font-medium mb-1"
              htmlFor="secondLastName"
            >
              Segundo apellido
            </label>
            <input
              id="secondLastName"
              name="secondLastName"
              value={form.secondLastName}
              onChange={onChange}
              className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            />
          </div>
        </div>

        {/* Correo */}
        <div>
          <label
            className="block text-sm font-medium mb-1"
            htmlFor="email"
          >
            Correo institucional
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

        {/* Carrera (course_category) */}
        <div>
          <label
            className="block text-sm font-medium mb-1"
            htmlFor="courseCategoryId"
          >
            Carrera
          </label>
          <select
            id="courseCategoryId"
            name="courseCategoryId"
            value={form.courseCategoryId}
            onChange={onChange}
            required
            disabled={loadingCategories}
            className="w-full rounded-md border bg-white px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo disabled:bg-gray-100"
          >
            <option value="">
              {loadingCategories ? "Cargando carreras..." : "— Seleccione —"}
            </option>
            {categories.map((cat) => (
              <option key={cat.id} value={cat.id}>
                {cat.name}
              </option>
            ))}
          </select>
        </div>

        {/* Contraseña + mostrar */}
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
            type={showPwd ? "text" : "password"}
            value={form.password}
            onChange={onChange}
            required
            className="w-full rounded-md border px-3 py-2 outline-none focus:ring-2 focus:ring-duocamarillo"
            placeholder="****************"
          />
          <label className="mt-2 inline-flex items-center gap-2 text-sm">
            <input
              type="checkbox"
              checked={showPwd}
              onChange={(e) => setShowPwd(e.target.checked)}
            />
            Mostrar contraseña
          </label>
        </div>

        {/* Botón registrarse */}
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
        <Link
          to="/Login"
          className="text-duocceleste hover:underline"
        >
          Inicia sesión
        </Link>
      </p>
    </div>
  );
}
