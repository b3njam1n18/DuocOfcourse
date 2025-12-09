import React, { useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import api from "../services/api";

export default function ResetPassword() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();

  const token = decodeURIComponent(searchParams.get("token"));

  const [password, setPassword] = useState("");
  const [confirm, setConfirm] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!password || !confirm) {
      alert("Debe ingresar la nueva contraseña en ambos campos.");
      return;
    }

    if (password !== confirm) {
      alert("Las contraseñas no coinciden.");
      return;
    }

    try {
      await api.post("/Auth/ResetPassword", {
        token,
        newPassword: password,
      });

      alert("Tu contraseña ha sido restablecida correctamente.");

      navigate("/auth/login");
    } catch (err) {
      console.error(err);
      alert(
        err.response?.data?.message ||
          "No se pudo actualizar la contraseña. Intente nuevamente."
      );
    }
  };

  return (
    <div className="bg-duocazul min-h-screen flex items-center justify-center px-4">
      <div className="bg-white p-8 rounded-lg shadow-md max-w-md w-full">

        <h2 className="text-2xl font-bold mb-6 text-center">
          Restablecer contraseña
        </h2>

        <form onSubmit={handleSubmit}>
          
          <label>Nueva contraseña</label>
          <div className="relative mb-4">
            <input
              type={showPassword ? "text" : "password"}
              className="w-full border p-2 rounded-md"
              placeholder="Ingrese nueva contraseña"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />

            <button
              type="button"
              className="absolute right-2 top-2 text-sm"
              onClick={() => setShowPassword(!showPassword)}
            >
              {showPassword ? "Ocultar" : "Mostrar"}
            </button>
          </div>

          <label>Confirmar contraseña</label>
          <input
            type={showPassword ? "text" : "password"}
            className="w-full border p-2 rounded-md mb-4"
            placeholder="Repita su contraseña"
            value={confirm}
            onChange={(e) => setConfirm(e.target.value)}
          />

          <button className="w-full bg-duocamarillo text-white p-2 rounded-md font-bold">
            Guardar contraseña
          </button>
        </form>

      </div>
    </div>
  );
}
