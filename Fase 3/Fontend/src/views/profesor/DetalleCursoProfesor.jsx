// src/views/profesor/DetalleCursoProfesor.jsx
import React, { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import DetCurso from "../../components/profesor/DetCurso";
import AgregarClaseModal from "../../components/profesor/AgregarClaseModal";
import {
  createModule,
  createLesson,
  getLessonsByCourse,
  getCourseById,
} from "../../services/coursesServices";

export default function DetalleCursoProfesor() {
  const { courseId } = useParams(); // /profesor/detallecurso/:courseId

  const [open, setOpen] = useState(false);
  const [moduleId, setModuleId] = useState(null);
  const [lessons, setLessons] = useState([]);
  const [course, setCourse] = useState(null);

  // 🔹 Cargar curso (título + nombre del profe)
  useEffect(() => {
    if (!courseId) return;

    const fetchCourse = async () => {
      try {
        const { data } = await getCourseById(courseId);
        setCourse(data);
      } catch (err) {
        console.error("Error cargando curso", err);
      }
    };

    fetchCourse();
  }, [courseId]);

  // 🔹 Cargar clases existentes
  useEffect(() => {
    if (!courseId) return;

    const fetchLessons = async () => {
      try {
        const { data } = await getLessonsByCourse(courseId);
        const list = data || [];
        setLessons(list);

        if (list.length > 0) {
          setModuleId(list[0].moduleId);
        }
      } catch (err) {
        console.error("Error cargando clases", err);
      }
    };

    fetchLessons();
  }, [courseId]);

// Crear nueva clase
const crearClase = async ({ nombre /*, descripcion, relacionada, file */ }) => {
  try {
    if (!nombre || !nombre.trim()) {
      alert("Falta el nombre de la clase");
      return;
    }

    let currentModuleId = moduleId;

    // Si aún no hay módulo, lo creamos
    if (!currentModuleId) {
      const { data: module } = await createModule(courseId, "Módulo 1");
      currentModuleId = module.id;
      setModuleId(currentModuleId);
    }

    // Crear la lesson en ese módulo
    const { data: lesson } = await createLesson(currentModuleId, {
      title: nombre.trim(),
      contentUrl: null,
      durationMinutes: null,
    });

    setLessons((prev) => [...prev, lesson]);
    setOpen(false);
  } catch (err) {
    console.error("Error al crear la clase", err);
    alert(err.response?.data?.message ?? "Error al crear la clase");
  }
};


  const courseTitle = course?.title ?? "Curso sin título";
  const teacherName =
    course?.teacherFullName ??
    (course?.teacherId ? `Profesor ID ${course.teacherId}` : "Profesor");

  return (
    <section>
      <h2 className="text-3xl font-bold mb-8">
        {courseTitle} - {teacherName}
      </h2>

      <div className="flex gap-6 flex-wrap">
        {/* Carta para agregar nueva clase */}
        <button
          type="button"
          onClick={() => setOpen(true)}
          className="w-80 border-2 border-dashed border-duocazul rounded-md
                     flex flex-col items-center justify-center p-6
                     text-duocazul hover:bg-duocazul/5 transition-colors"
        >
          <span className="text-3xl mb-2">+</span>
          <span className="font-semibold mb-1">Agregar clase</span>
          <span className="text-sm text-gray-500">Crear</span>
        </button>

        {/* Clases existentes */}
        {lessons.map((lesson) => (
          <DetCurso key={lesson.id} lesson={lesson} />
        ))}
      </div>

      {/* Modal para crear clase */}
      <AgregarClaseModal
        open={open}
        onClose={() => setOpen(false)}
        onSubmit={crearClase}
      />
    </section>
  );
}
