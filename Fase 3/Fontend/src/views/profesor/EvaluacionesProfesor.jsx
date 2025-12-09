import React, { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

import AddCardButton from "../../components/ui/AddCardButton";
import EvaluacionesComp from "../../components/profesor/EvaluacionesComp";
import CrearEvaluacionModal from "../../components/profesor/CrearEvaluacionModal";

import {
  createEvaluation,
  getEvaluationsByCourse,
} from "../../services/evaluationsService";
import { getCoursesByTeacher } from "../../services/coursesServices";

export default function EvaluacionesProfesor() {
  const [open, setOpen] = useState(false);

  const [evaluaciones, setEvaluaciones] = useState([]);
  const [cursos, setCursos] = useState([]);

  const [searchParams] = useSearchParams();
  const courseIdFromUrl = Number(searchParams.get("courseId")) || null;

  // ===== ID DEL PROFESOR =====
  const rawUser = localStorage.getItem("user");
  console.log("LOCALSTORAGE USER =", rawUser);

  const parsedUser = rawUser ? JSON.parse(rawUser) : null;
  console.log("parsedUser:", parsedUser);

  const teacherId = parsedUser?.userId;
  console.log("teacherId:", teacherId);

  // 🔹 Curso seleccionado en la vista
  const [selectedCourseId, setSelectedCourseId] = useState(courseIdFromUrl);

  // ===== CURSOS DEL PROFESOR =====
  useEffect(() => {
    if (!teacherId) return;

    async function fetchCursos() {
      try {
        const res = await getCoursesByTeacher(teacherId);
        console.log("Cursos del profesor:", res.data?.courses);
        const lista = res.data?.courses || [];
        setCursos(lista);

        // Si no hay courseId en la URL, tomamos el primero por defecto
        if (!courseIdFromUrl && lista.length > 0 && !selectedCourseId) {
          setSelectedCourseId(lista[0].id);
        }
      } catch (err) {
        console.error("Error cargando cursos:", err);
      }
    }

    fetchCursos();
  }, [teacherId]); // eslint-disable-line react-hooks/exhaustive-deps

  // ===== CARGAR EVALUACIONES FINALES DEL CURSO SELECCIONADO =====
  useEffect(() => {
    if (!selectedCourseId) {
      console.warn("EvaluacionesProfesor: no hay curso seleccionado");
      setEvaluaciones([]);
      return;
    }

    const cargarEvaluaciones = async () => {
      try {
        const res = await getEvaluationsByCourse(selectedCourseId);

        const finales = (res.data || []).filter(
          (ev) => ev.isFinalExam === true
        );

        setEvaluaciones(finales);
      } catch (e) {
        console.error("Error cargando evaluaciones:", e);
      }
    };

    cargarEvaluaciones();
  }, [selectedCourseId]);

  // ===== CREAR EVALUACIÓN FINAL =====
  const handleCrearEvaluacion = async (dataPlano) => {
    if (!dataPlano.courseId) {
      alert("Debes seleccionar un curso");
      return;
    }

    const courseIdNum = Number(dataPlano.courseId);

    const payload = {
      title: dataPlano.title,
      description: dataPlano.instrucciones || null,
      dueAt: null,
      type: "FINAL_EXAM",
      passThreshold: 0.6,
      isFinalExam: true,
    };

    try {
      const res = await createEvaluation(courseIdNum, payload);

      const nueva = res.data;

      if (courseIdNum === selectedCourseId) {
        setEvaluaciones((prev) => [...prev, nueva]);
      }

      setOpen(false);
    } catch (err) {
      console.error("Error creando evaluación:", err);
    }
  };

  return (
    <section className="p-8">
      <h2 className="text-3xl font-bold mb-6">Evaluaciones finales</h2>

      {/* Selector de curso */}
      <div className="mb-6">
        <label className="block mb-1 font-medium">Selecciona un curso</label>
        <select
          className="w-full max-w-sm border rounded-md p-2"
          value={selectedCourseId || ""}
          onChange={(e) => {
            const val = e.target.value;
            setSelectedCourseId(val ? Number(val) : null);
          }}
        >
          <option value="">-- Selecciona un curso --</option>
          {cursos.map((c) => (
            <option key={c.id} value={c.id}>
              {c.title}
            </option>
          ))}
        </select>
      </div>

      <div className="flex flex-wrap gap-4 items-stretch">
        <AddCardButton
          title="Agregar evaluación"
          cta="Crear"
          onClick={() => setOpen(true)}
        />

        {evaluaciones.length === 0 ? (
          <p className="text-sm text-gray-600">
            {selectedCourseId
              ? "Este curso aún no tiene evaluaciones finales creadas."
              : "Selecciona un curso para ver sus evaluaciones finales."}
          </p>
        ) : (
          evaluaciones.map((ev) => (
            <EvaluacionesComp
              key={ev.id}
              evaluation={ev}
              courseId={selectedCourseId} // 👈 usamos el curso seleccionado
            />
          ))
        )}
      </div>

      {/* Modal de creación */}
      {open && Array.isArray(cursos) && (
        <CrearEvaluacionModal
          cursos={cursos}
          open={open}
          onClose={() => setOpen(false)}
          onSubmit={handleCrearEvaluacion}
        />
      )}
    </section>
  );
}
