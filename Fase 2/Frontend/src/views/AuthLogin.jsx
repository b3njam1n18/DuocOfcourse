import Login from "../components/Login";

import img1 from "../assets/DuocUCLogo.png";
import img2 from "../assets/OfCourseAmarillo.png";

export default function AuthLogin() {
  return (
    <section className="p-8">
        <div className="relative max-w-2xl mx-auto">
        {/* Imágenes absolutas */}
        <div className="absolute inset-x-0 top-0 z-10 flex flex-col items-center gap-2">
          <img src={img1} alt="Imagen superior" className=" h-16 " />
          <img src={img2} alt="Imagen inferior" className="h-10" />
        </div>

        {/* Espacio para que NO se superponga  */}
        <div aria-hidden className="h-32" />
       
        <Login/>
        
      </div>
    </section>
  );
}