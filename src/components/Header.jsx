import React from "react";
import userImage from "../assets/DuocOfCourseLogo.png";

function Header() {
  return (
    <header className="sticky top-0 z-40 w-full flex items-center justify-between px-6 py-3 bg-white border-b-2 border-blackfixed top-0 left-64 w-[calc(100vw-16rem)] z-40
                        flex items-center justify-between px-6 py-3
                        bg-white border-b-2 border-black">
      <h1 className="text-xl font-bold text-gray-800">
        Bienvenido, <span className="text-black-600">Alumno</span>
      </h1>

      <img
        src={userImage}
        alt="OfCourseLogo"
        className=" h-4"
      />
    </header>
  );
}

export default Header;
