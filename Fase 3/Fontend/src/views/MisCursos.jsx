// src/views/MisCursos.jsx (o donde lo tengas)
import { useEffect, useState } from "react";
import Cursos from "../components/Cursos";
import {
  getAllCoursesWithSchool,
  getEnrolledCoursesByStudent,
} from "../services/coursesServices";

export default function MisCursos() {
  const [currentUser, setCurrentUser] = useState(null);
  const [myCourses, setMyCourses] = useState([]);
  const [loading, setLoading] = useState(true);

  // Leer usuario logueado
  useEffect(() => {
    try {
      const raw = localStorage.getItem("user");
      if (raw) {
        const parsed = JSON.parse(raw);
        setCurrentUser(parsed);
      }
    } catch (err) {
      console.error("Error leyendo usuario desde localStorage:", err);
    }
  }, []);

  // Cargar cursos inscritos
  useEffect(() => {
    async function fetchMyCourses() {
      if (!currentUser || !currentUser.userId || currentUser.roleId !== 2) {
        setLoading(false);
        return;
      }

      try {
        setLoading(true);

        // 1) Traer ids de cursos inscritos
        const enrollRes = await getEnrolledCoursesByStudent(currentUser.userId);
        const enrolledIds = enrollRes.data || [];

        if (enrolledIds.length === 0) {
          setMyCourses([]);
          return;
        }

        // 2) Traer todos los cursos
        const coursesRes = await getAllCoursesWithSchool();
        const allCourses = Array.isArray(coursesRes.data)
          ? coursesRes.data
          : coursesRes.data?.courses || [];

        // 3) Filtrar solo los que el alumno tiene inscritos
        const mine = allCourses.filter((c) => enrolledIds.includes(c.id));
        setMyCourses(mine);
      } catch (err) {
        console.error("Error cargando mis cursos:", err);
      } finally {
        setLoading(false);
      }
    }

    fetchMyCourses();
  }, [currentUser]);

  if (!currentUser) {
    return (
      <section>
        <main>
          <h2 className="text-3xl font-bold mb-4">Mis cursos</h2>
          <p className="text-sm text-gray-600">
            Debes iniciar sesión como estudiante para ver tus cursos.
          </p>
        </main>
      </section>
    );
  }

  if (loading) {
    return (
      <section>
        <main>
          <h2 className="text-3xl font-bold mb-4">Mis cursos</h2>
          <p className="text-sm text-gray-600">Cargando tus cursos...</p>
        </main>
      </section>
    );
  }

  return (
    <section>
      <main>
        <h2 className="text-3xl font-bold mb-8">Mis cursos</h2>

        {myCourses.length === 0 ? (
          <p className="text-sm text-gray-600">
            Aún no estás inscrito en ningún curso.
          </p>
        ) : (
          <div className="flex gap-3 flex-wrap">
            {myCourses.map((course) => (
              <Cursos key={course.id} course={course} />
            ))}
          </div>
        )}
      </main>
    </section>
  );
}
