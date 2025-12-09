// src/services/certificatesService.js
import api from "./api";

// Descarga el certificado PDF de un curso para un estudiante
export const downloadCourseCertificate = (courseId, studentId) => {
  return api.get(`/courses/${courseId}/certificate`, {
    params: { studentId },
    responseType: "blob", // 👈 importante para archivos binarios
  });
};