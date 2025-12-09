import api from "./api";

export const getSchools = () => api.get("/school");

export const getCareersBySchool = (schoolId) =>
  api.get(`/careers/by-school/${schoolId}`);
