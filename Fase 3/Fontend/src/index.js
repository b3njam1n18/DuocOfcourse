import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';

import { createBrowserRouter, RouterProvider, Navigate } from "react-router-dom";

import AppLayout from "./App";
import AuthLayout from "./AuthLayout";

import MisCursos from "./views/MisCursos";
import ExplorarCursos from "./views/ExplorarCursos";
import Calificaciones from "./views/Calificaciones";
import Evaluaciones from "./views/Evaluaciones";
import Evaluacion from "./views/Evaluacion";
import Certificados from "./views/Certificados";
import DetalleCurso from "./views/DetalleCurso";
import Clase from "./views/Clase";
import RecuperarContrasena from "./views/RecuperarContrasena";
import Login from './components/Login';
import AuthRegister from './views/AuthRegister';
import ResetPassword from './views/ResetPassword'
import MisCursosProfesor from './views/profesor/MisCursosProfesor';
import ProfesorLayout from "./layouts/ProfesorLayout";
import DetalleCursoProfesor from "./views/profesor/DetalleCursoProfesor";
import ClaseProfesor from "./views/profesor/ClaseProfesor";
import EvaluacionesProfesor from "./views/profesor/EvaluacionesProfesor";
import EditarEvaluacionProfesor from "./views/profesor/EditarEvaluacionProfesor";
import CalificacionesProfesor from "./views/profesor/CalificacionesProfesor";
import ErrorPage from "./components/errorpage";


import PrivateRoute from "./components/PrivateRoute";


const router = createBrowserRouter([
  //  redirige al login
  {
    path: "/",
    element: <Navigate to="/auth/login" replace />,
  },

  //  LAYOUT DE AUTH
  {
    path: "/auth",
    element: <AuthLayout />,
    errorElement: <ErrorPage />,
    children: [
      { path: "login", element: <Login /> },
      { path: "register", element: <AuthRegister /> },
      { path: "recuperar", element: <RecuperarContrasena /> },
    ],
  },

  //  LAYOUT ALUMNO
  {
    path: "/app",
    element: (
      <PrivateRoute>
        <AppLayout />
      </PrivateRoute>
    ),
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <MisCursos /> },
      { path: "explorar", element: <ExplorarCursos /> },
      { path: "calificaciones", element: <Calificaciones /> },
      { path: "evaluaciones", element: <Evaluaciones /> },
      { path: "certificados", element: <Certificados /> },
      { path: "detallecurso/:courseId", element: <DetalleCurso /> },
      { path: "clase/:courseId/:lessonId", element: <Clase />},
      { path: "evaluacion/:courseId/:evaluationId", element: <Evaluacion /> },
    ],
  },


  {
    path: "/profesor",
    element: (
      <PrivateRoute>
        <ProfesorLayout />
      </PrivateRoute>
    ),
    errorElement: <ErrorPage />,
    children: [
      { index: true, element: <MisCursosProfesor /> },
      { path: "detallecurso/:courseId", element: <DetalleCursoProfesor /> },
      { path: "editarclase/:courseId/:lessonId", element: <ClaseProfesor /> },
      { path: "evaluaciones", element: <EvaluacionesProfesor /> },
      { path: "editarevaluacion", element: <EditarEvaluacionProfesor /> },
      { path: "calificaciones", element: <CalificacionesProfesor /> },
    ],
  },

  //layout reset-pass
  {
    path: "/reset-password",
    element: <ResetPassword />,
  }
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <RouterProvider router={router} />
);
