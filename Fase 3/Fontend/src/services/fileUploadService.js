import api from "./api";

/**
 * Sube un archivo al backend (FilesController) para que este lo guarde en GCP
 * @param {File} file - este es el archivo que se va a  subir
 * @param {string} folder - carpeta dentro del bucket 
 * @returns {Promise<string>} - URL pública del archivo en GCP
 */
export async function uploadFileToGcs(file, folder) {
  if (!file) return null;

  const formData = new FormData();
  formData.append("file", file);
  formData.append("folder", folder);

  const { data } = await api.post("/Gcp/upload", formData, {
    headers: { "Content-Type": "multipart/form-data" },
  });


  return data.url;
}
