// src/pages/AuthRegister.jsx
import { useEffect, useState } from "react";
import api from "../services/api";
import { useNavigate } from "react-router-dom";

export default function AuthRegister() {
  const nav = useNavigate();

  const [schools, setSchools] = useState([]);
  const [careers, setCareers] = useState([]);

  const [form, setForm] = useState({
    firstName: "",
    middleName: "",
    lastName: "",
    secondLastName: "",
    email: "",
    schoolId: "",
    careerId: "",
    password: "",
  });

  const [showPass, setShowPass] = useState(false);
  const [errorMsg, setErrorMsg] = useState(null);

  // Cargar ESCUELAS (+ courseCategories)
  useEffect(() => {
    api
      .get("/School")              // tu endpoint actual
      .then((res) => setSchools(res.data || []))
      .catch((err) => {
        console.error("Error cargando escuelas:", err);
        setErrorMsg("No se pudieron cargar las escuelas.");
      });
  }, []);

  // Cambios genéricos en inputs
  const handleChange = (e) => {
    const { name, value } = e.target;
    setForm((f) => ({ ...f, [name]: value }));
  };

  // Cuando cambia la ESCUELA
  const handleSchoolChange = (e) => {
    const schoolId = Number(e.target.value);
    setForm({ ...form, schoolId, careerId: "" });

    const school = schools.find((s) => s.id === schoolId);
    setCareers(school ? school.courseCategories : []);
  };

  // Registrar usuario
  const handleSubmit = async (e) => {
  e.preventDefault();
  try {
    await api.post("/Users/registro", {
      Roleid: 2,
      FirstName: form.firstName,
      MiddleName: form.middleName,
      LastName: form.lastName,
      SecondLastName: form.secondLastName,
      Email: form.email,
      Password: form.password,
      SchoolId: Number(form.schoolId) || 0, // se puede ignorar en backend
      CareerId: Number(form.careerId),
    });

    alert("Cuenta creada exitosamente.");
    nav("/auth/login");
  } catch (error) {
    console.error(error);
    alert(error.response?.data ?? "Error al registrar");
  }
};


  return (
    <div className="min-h-screen w-full flex flex-col items-center bg-duocazul py-10">
      <form
        onSubmit={handleSubmit}
        className="bg-white shadow-lg rounded-xl p-10 w-[420px]"
      >
        <h2 className="text-3xl font-bold mb-2">Crear cuenta</h2>
        <p className="text-gray-600 mb-6">Regístrate en DuocOfCourse</p>

        {errorMsg && (
          <p className="mb-3 text-sm text-red-500">{errorMsg}</p>
        )}

        {/* Nombres */}
        <label className="font-semibold block">Primer nombre:</label>
        <input
          name="firstName"
          type="text"
          className="duoc-input"
          value={form.firstName}
          onChange={handleChange}
          required
        />

        <label className="font-semibold mt-3 block">Segundo nombre:</label>
        <input
          name="middleName"
          type="text"
          className="duoc-input"
          value={form.middleName}
          onChange={handleChange}
        />

        <label className="font-semibold mt-3 block">Primer apellido:</label>
        <input
          name="lastName"
          type="text"
          className="duoc-input"
          value={form.lastName}
          onChange={handleChange}
          required
        />

        <label className="font-semibold mt-3 block">Segundo apellido:</label>
        <input
          name="secondLastName"
          type="text"
          className="duoc-input"
          value={form.secondLastName}
          onChange={handleChange}
        />

        {/* Email */}
        <label className="font-semibold mt-4 block">Correo:</label>
        <input
          name="email"
          type="email"
          className="duoc-input"
          placeholder="tucorreo@duoc.cl"
          value={form.email}
          onChange={handleChange}
          required
        />

        {/* Escuela */}
        <label className="font-semibold mt-4 block">Seleccione escuela</label>
        <select
          className="duoc-input"
          value={form.schoolId}
          onChange={handleSchoolChange}
          required
        >
          <option value="">— Seleccione —</option>
          {schools.map((s) => (
            <option key={s.id} value={s.id}>
              {s.name}
            </option>
          ))}
        </select>

        {/* Carrera */}
        <label className="font-semibold mt-4 block">Seleccione carrera</label>
        <select
          className="duoc-input"
          name="careerId"
          value={form.careerId}
          onChange={handleChange}
          required
          disabled={!form.schoolId}
        >
          <option value="">— Seleccione —</option>
          {careers.map((c) => (
            <option key={c.id} value={c.id}>
              {c.name}
            </option>
          ))}
        </select>

        {/* Contraseña */}
        <label className="font-semibold mt-4 block">Contraseña:</label>
        <input
          type={showPass ? "text" : "password"}
          className="duoc-input"
          value={form.password}
          onChange={(e) =>
            setForm((f) => ({ ...f, password: e.target.value }))
          }
          required
        />

        <div className="flex items-center mt-2">
          <input
            type="checkbox"
            onChange={() => setShowPass((v) => !v)}
          />
          <span className="ml-2 text-sm">Mostrar contraseña</span>
        </div>

        {/* Botón */}
        <button
          type="submit"
          className="w-full bg-duocamarillo text-duocgris font-semibold py-2 rounded-md mt-6 hover:bg-duocceleste hover:text-white transition"
        >
          Registrarse
        </button>
      </form>
    </div>
  );
}
