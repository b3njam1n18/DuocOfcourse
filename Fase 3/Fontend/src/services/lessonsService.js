// src/services/lessonsService.js
import api from "./api";

export const getLesson = (courseId, lessonId) =>
  api.get(`/courses/${courseId}/lessons/${lessonId}`);

export const uploadLessonVideo = (courseId, lessonId, formData) =>
  api.post(`/courses/${courseId}/lessons/${lessonId}/video`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
