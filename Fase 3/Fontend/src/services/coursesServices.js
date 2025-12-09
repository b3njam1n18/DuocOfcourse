// src/services/coursesServices.js
import api from "./api";

// Crear curso (para profesor)
export const crearCurso = (payload) => {
  return api.post("/Courses", payload); // tu backend usa plural
};

// Categorías (si existen)
export const getCategories = () => {
  return api.get("/Categories");
};

// Escuelas
export const getSchools = () => {
  return api.get("/Schools");
};

// 🔹 NUEVO: traer todos los cursos (para vista estudiante, admin, etc.)
export const getAllCoursesWithSchool = () => {
  // Asumimos que GET /Courses devuelve todos los cursos junto con info de escuela
  return api.get("/Courses");
};

// crea un módulo para un curso
export function createModule(courseId, title) {
  return api.post(`/Courses/${courseId}/modules`, { title });
}

// 🔧 crea una clase (lesson) dentro de un módulo
// AHORA MANDAMOS TAMBIÉN "position"
export function createLesson(
  moduleId,
  { title, contentUrl = null, durationMinutes = null, position = 1 }
) {
  return api.post(`/Courses/modules/${moduleId}/lessons`, {
    title,
    contentUrl,
    durationMinutes,
    position,
  });
}

export function getLessonsByCourse(courseId) {
  return api.get(`/Courses/${courseId}/lessons`);
}

export function getCourseById(courseId) {
  return api.get(`/Courses/${courseId}`);
}

// Cursos por profesor
export const getCoursesByTeacher = (teacherId) =>
  api.get(`/Courses/teacher/${teacherId}`);

// Inscribir alumno en un curso
export const enrollInCourse = (courseId, studentId) => {
  return api.post(`/Courses/${courseId}/enroll`, { studentId });
};

// Cursos en los que está inscrito un alumno
export const getEnrolledCoursesByStudent = (studentId) => {
  return api.get(`/Courses/student/${studentId}/enrollments`);
};
