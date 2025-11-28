import CalificacionesComp from "../../components/profesor/CalificacionesComp";
export default function CalificacionesProfesor() {
  return (
    <section className="p-8">
      
      <h2 className="text-3xl font-bold mb-8">Calificaciones</h2>
      <div className="flex gap-3 flex-wrap">
        <CalificacionesComp />
      </div>
    </section>
  );
}