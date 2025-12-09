import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../services/api"; // ajusta ruta si es necesario

export default function RecuperarContrasena() {
  const [email, setEmail] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!email) {
      alert("Por favor ingresa un correo.");
      return;
    }

    try {
      await api.post("/Auth/ForgotPassword", { email });

      alert("Se ha enviado un correo para restablecer tu contraseña.");

      // Redirigir al login
      navigate("/auth/login");

    } catch (err) {
      console.error(err);
      alert("No se pudo enviar el correo. Verifique el correo ingresado.");
    }
  };

  return (
    <div className="bg-duocazul min-h-screen flex items-center justify-center px-4">
      <div className="bg-white p-8 rounded-lg shadow-md max-w-md w-full">
        <h2 className="text-2xl font-bold mb-6 text-center">
          Restablecer contraseña
        </h2>

        <form onSubmit={handleSubmit}>
          <input
            type="email"
            placeholder="tucorreo@duoc.cl"
            className="w-full border p-2 rounded-md mb-4"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />

          <button
            type="submit"
            className="w-full bg-duocamarillo text-white p-2 rounded-md font-bold"
          >
            Ingresar
          </button>
        </form>

        <p
          className="text-center mt-4 cursor-pointer text-sm"
          onClick={() => navigate("/auth/login")}
        >
          Volver a inicio de sesión
        </p>
      </div>
    </div>
  );
}
