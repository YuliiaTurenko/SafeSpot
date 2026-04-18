import i18n from "i18next";

export default function LanguageButton() {
  const changeLang = (lng: string) => {
    i18n.changeLanguage(lng);
    localStorage.setItem("lang", lng);
  };

  const currentLang = i18n.language;

  const btnClass = (lang: string) => `
    transition-all duration-200 ease-in-out
    ${currentLang === lang 
      ? "text-white font-bold border-b-2 border-white" 
      : "text-[#CAD2C5] hover:text-white opacity-70 hover:opacity-100"}
  `;

  return (
    <div className="flex justify-end mb-4 p-2">
      <div className="flex items-center gap-3">
        <button 
          onClick={() => changeLang("en")} 
          className={btnClass("en")}
        >
          EN
        </button>
        
        <span className="text-[#CAD2C5] opacity-30">|</span>
        
        <button 
          onClick={() => changeLang("uk")} 
          className={btnClass("uk")}
        >
          UA
        </button>
      </div>
    </div>
  );
}
