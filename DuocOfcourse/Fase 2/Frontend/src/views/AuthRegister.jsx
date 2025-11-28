import Register from "../components/Register";
import img1 from "../assets/DuocUCLogo.png";
import img2 from "../assets/OfCourseAmarillo.png";

export default function AuthRegister() {
  return (
    <section className="p-8">
      <div className="relative max-w-2xl mx-auto">
        <div className="absolute inset-x-0 top-0 z-10 flex flex-col items-center gap-2">
          <img src={img1} alt="DuocUC" className="h-16" />
          <img src={img2} alt="OfCourse" className="h-10" />
        </div>
        <div aria-hidden className="h-32" />
        <Register />
      </div>
    </section>
  );
}
