import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import AppLayout from "./App";
import AuthLayout from "./AuthLayout";
import { createBrowserRouter, RouterProvider, BrowserRouter  } from "react-router-dom";

import MisCursos from "./views/MisCursos";
import ExplorarCursos from "./views/ExplorarCursos";
import Calificaciones from "./views/Calificaciones";
import Evaluaciones from "./views/Evaluaciones";
import Evaluacion from "./views/Evaluacion";
import Certificados from "./views/Certificados";
import DetalleCurso from "./views/DetalleCurso";
import Clase from "./views/Clase";
import RecuperarContrasena from "./views/RecuperarContrasena";
import AuthLogin from './views/AuthLogin';

import MisCursosProfesor from './views/profesor/MisCursosProfesor';
import ProfesorLayout from "./layouts/ProfesorLayout";
import DetalleCursoProfesor from "./views/profesor/DetalleCursoProfesor";
import ClaseProfesor from "./views/profesor/ClaseProfesor";
import EvaluacionesProfesor from "./views/profesor/EvaluacionesProfesor";
import EditarEvaluacionProfesor from "./views/profesor/EditarEvaluacionProfesor";
import CalificacionesProfesor from "./views/profesor/CalificacionesProfesor";
import AuthRegister from './views/AuthRegister';




const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </React.StrictMode>
);

const router = createBrowserRouter([
    {
    element: <AuthLayout />,
    children: [
      { path: "/Login", element: <AuthLogin /> },
      { path: "/Register", element: <AuthRegister /> },
      { path: "/RecuperarContrasena", element: <RecuperarContrasena /> },
    ],
  },
  {
    path: "/",
    element: <AppLayout />,
    children: [
      { index: true, element: <MisCursos /> },
      { path: "ExplorarCursos", element: <ExplorarCursos /> },
      { path: "Calificaciones", element: <Calificaciones /> },
      { path: "Evaluaciones", element: <Evaluaciones /> },
      { path: "Certificados", element: <Certificados /> },
      { path: "DetalleCurso", element: <DetalleCurso /> },
      { path: "Clase", element: <Clase /> },
      { path: "Evaluacion", element: <Evaluacion /> },

    ],
  },

  { path: "/profesor", element: <ProfesorLayout />, children: [
      { index: true, element: <MisCursosProfesor /> },
      { path: "DetalleCurso", element: <DetalleCursoProfesor /> },
      { path: "EditarClase", element: <ClaseProfesor /> },
      { path: "Evaluaciones", element: <EvaluacionesProfesor /> },
      { path: "EditarEvaluacion", element: <EditarEvaluacionProfesor /> },
      { path: "Calificaciones", element: <CalificacionesProfesor /> },
  ]},
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <RouterProvider router={router} />
);
