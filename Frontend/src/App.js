import React, { useState } from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './components/Navbar';
import Header from './components/Header';


function App() {
  const [menuOpen, setMenuOpen] = useState(false);

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Sidebar: desktop fijo + mobile drawer */}
      <Navbar open={menuOpen} onClose={() => setMenuOpen(false)} />

      {/* Contenido principal: en desktop deja espacio para el sidebar */}
      <div className="lg:pl-64">
        <Header onOpenMenu={() => setMenuOpen(true)} />
        <main className="p-4">
          <Outlet />
        </main>
      </div>
    </div>
    
  );
}
//PRUEBA NO CONSIDERAR
  // const AppLayout = () => {
  //   return (
  //     <div className="min-h-screen flex flex-col">
  //       <Navbar />
  //       <BackendStatus /> {}
  //       <main className="flex-1">
  //         <Outlet />
  //       </main>
  //     </div>
  //   );
  // };

export default App;
