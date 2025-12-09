export default function ErrorPage() {
  return (
    <div className="flex flex-col items-center justify-center h-screen text-center p-6">
      <h1 className="text-4xl font-bold text-red-600 mb-4">Oops!</h1>
      <p className="text-gray-600 mb-4">
        Algo salió mal o la página no existe.
      </p>
      <a href="/login" className="text-blue-500 underline">
        Volver al inicio
      </a>
    </div>
  );
}
