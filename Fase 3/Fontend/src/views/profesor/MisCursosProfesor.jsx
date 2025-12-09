import React, { useState, useEffect } from "react";
import Cursos from "../../components/profesor/CursosProfesor";
import NuevoCursoModal from "../../components/profesor/NuevoCursoModal";
import AddCardButton from "../../components/ui/AddCardButton";
import api from "../../services/api";

export default function MisCursosProfesor() {
  const [open, setOpen] = useState(false);
  const [categorias, setCategorias] = useState([]);
  const [escuelas, setEscuelas] = useState([]);
  const [cursos, setCursos] = useState([]);

  // Cargar escuelas (y categorías) al entrar a la vista
  useEffect(() => {
    const fetchFilters = async () => {
      try {
        const res = await api.get("/School"); // endpoint correcto
        console.log("School API:", res.data);

        const schools = res.data || [];

        // Escuelas: id + name
        setEscuelas(
          schools.map((s) => ({
            id: s.id,
            name: s.name,
          }))
        );

        // Categorías: aplanar courseCategories de todas las schools
        const allCategories = [];
        schools.forEach((s) => {
          (s.courseCategories || []).forEach((c) => {
            allCategories.push({
              id: c.id,
              name: c.name,
            });
          });
        });

        setCategorias(allCategories);
      } catch (err) {
        console.error("Error cargando categorías/escuelas", err);
      }
    };

    fetchFilters();
  }, []);

  // Cargar cursos del profesor logueado
  useEffect(() => {
    const fetchCursos = async () => {
      try {
        const user = JSON.parse(localStorage.getItem("user"));
        const teacherId = user?.userId;
        if (!teacherId) return;

        const res = await api.get(`/Courses/teacher/${teacherId}`);
        // La API devuelve { teacherId, courses: [...] }
        setCursos(res.data?.courses || []);
      } catch (err) {
        console.error("Error cargando cursos del profesor", err);
      }
    };

    fetchCursos();
  }, []);


  const crearCurso = async ({ title, description, categoryId, schoolId, file }) => {
    try {
      const user = JSON.parse(localStorage.getItem("user"));
      const teacherId = user?.userId;

      if (!teacherId) {
        alert("No se encontró el usuario profesor en la sesión.");
        return;
      }

      // 👇 Por ahora no se sube nada, solo dejamos null
      let coverImagePath = null;

      const payload = {
        title,
        description,
        teacherId,
        categoryId,
        schoolId,
        coverImagePath,
      };

      const resCourse = await api.post("/Courses", payload);
      console.log("Curso creado:", resCourse.data);

      const creado = resCourse.data;
      setCursos((prev) => [
        ...prev,
        {
          id: creado.id,
          title: creado.title,
          isPublished: false,
          createdAt: creado.createdAt,
        },
      ]);

      alert("Curso creado correctamente ✅");
    } catch (err) {
      console.error("Error al crear curso", err);
      console.log("Respuesta backend:", err.response?.data);
      alert(err.response?.data?.message || "Ocurrió un error al crear el curso");
    }
  };



  // Eliminar curso
  const eliminarCurso = async (courseId) => {
    const confirmar = window.confirm("¿Seguro que quieres eliminar este curso?");
    if (!confirmar) return;

    try {
      await api.delete(`/Courses/${courseId}`);
      // sacar el curso del estado
      setCursos((prev) => prev.filter((c) => c.id !== courseId));
      alert("Curso eliminado correctamente 🗑️");
    } catch (err) {
      console.error("Error al eliminar curso", err);
      alert(
        err.response?.data?.message ||
        "No se pudo eliminar el curso. Revisa si tiene estudiantes inscritos."
      );
    }
  };

  return (
    <main>
      <section className="p-8">
        {/* Título */}
        <h2 className="text-3xl font-bold mb-8">Mis cursos</h2>

        {/* Grilla de cartas + botón-carta para crear */}
        <div className="flex flex-wrap gap-4 items-stretch">
          <AddCardButton
            title="Agregar curso"
            cta="Crear"
            onClick={() => setOpen(true)}
          />

          {/* Tus cursos */}
          <Cursos cursos={cursos} onDelete={eliminarCurso} />
        </div>

        {/* Modal de creación */}
        <NuevoCursoModal
          open={open}
          onClose={() => setOpen(false)}
          onSubmit={crearCurso}
          categorias={categorias}
          escuelas={escuelas}
        />
      </section>
    </main>
  );
}
