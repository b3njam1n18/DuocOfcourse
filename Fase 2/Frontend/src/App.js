import { Outlet } from "react-router-dom";
import React from 'react';
import Navbar from './components/Navbar';
import Header from "./components/Header";
function App() {
  return (
    <div className="flex">
      <Navbar />
      <div className="ml-64 min-h-screen">
        <Header />
        <main>
          <Outlet />
        </main>
      </div>
    </div>
  );
}

export default App;
