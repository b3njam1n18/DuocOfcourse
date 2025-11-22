import Cursos from "../components/Cursos";
export default function MisCursos() {
  return (
    <section className="p-8">
      <main>
        <section className="p-8">
          <h2 className="text-3xl font-bold mb-8">Mis cursos</h2>
          <div className="flex gap-3 flex-wrap">
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
            <Cursos />
          </div>
        </section>
      </main>
    </section>
  );
}