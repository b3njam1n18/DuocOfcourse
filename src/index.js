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
import MisCursosProfesor from './views/MisCursosProfesor';


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
      { path: "/RecuperarContrasena", element: <RecuperarContrasena /> },
    ],
  },
  {
    path: "/",
    element: <AppLayout />,
    children: [
      { index: true, element: <MisCursos /> },
      { path: "MisCursosProfesor", element: <MisCursosProfesor /> },
      { path: "ExplorarCursos", element: <ExplorarCursos /> },
      { path: "Calificaciones", element: <Calificaciones /> },
      { path: "Evaluaciones", element: <Evaluaciones /> },
      { path: "Certificados", element: <Certificados /> },
      { path: "DetalleCurso", element: <DetalleCurso /> },
      { path: "Clase", element: <Clase /> },
      { path: "Evaluacion", element: <Evaluacion /> },

    ],
  },
]);

ReactDOM.createRoot(document.getElementById("root")).render(
  <RouterProvider router={router} />
);
