import React, { useEffect, useState } from "react";
import { getCoursesByTeacher } from "../../services/coursesServices";

const API_URL = process.env.REACT_APP_API_URL || "https://localhost:7037/api";

export default function CalificacionesProfesor() {
  const [courses, setCourses] = useState([]);   // siempre un arreglo
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        setError(null);

        const raw = localStorage.getItem("user");
        if (!raw) {
          setError("Debes iniciar sesión como profesor.");
          setCourses([]);
          return;
        }

        const user = JSON.parse(raw);
        if (user.roleId !== 3) {
          setError("Esta sección es solo para profesores.");
          setCourses([]);
          return;
        }

        const teacherId = user.userId;
        const { data } = await getCoursesByTeacher(teacherId);

        console.log("Respuesta de getCoursesByTeacher:", data);

        // 👈 aquí usamos el array correcto
        setCourses(Array.isArray(data.courses) ? data.courses : []);
      } catch (err) {
        console.error(err);
        setError(
          err.response?.data?.message ||
            err.message ||
            "No se pudieron cargar los cursos."
        );
        setCourses([]);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const handleDownloadExcel = (courseId) => {
    const raw = localStorage.getItem("user");
    const user = raw ? JSON.parse(raw) : null;
    if (!user) return;

    const teacherId = user.userId;
    const url = `${API_URL}/teachers/${teacherId}/courses/${courseId}/grades/excel`;
    window.open(url, "_blank");
  };

  if (loading) {
    return <p className="text-sm text-gray-600">Cargando cursos...</p>;
  }

  if (error) {
    return <p className="text-sm text-red-500">{error}</p>;
  }

  if (!courses.length) {
    return (
      <p className="text-sm text-gray-600">
        Aún no tienes cursos asociados.
      </p>
    );
  }

  return (
    <section>
      <h2 className="text-3xl font-bold mb-4">Calificaciones de mis cursos</h2>
      <div className="flex flex-wrap gap-4">
        {courses.map((c) => (
          <div
            key={c.id}
            className="w-80 bg-white border rounded-lg shadow p-4 flex flex-col justify-between"
          >
            <div>
              <h3 className="text-lg font-semibold mb-1">
                {c.title || c.name || "Curso sin título"}
              </h3>
              <p className="text-xs text-gray-600">ID: {c.id}</p>
            </div>

            <button
              onClick={() => handleDownloadExcel(c.id)}
              className="mt-4 text-sm font-semibold rounded bg-duocazul text-white px-4 py-2 hover:bg-duocceleste"
            >
              Exportar notas a Excel
            </button>
          </div>
        ))}
      </div>
    </section>
  );
}
