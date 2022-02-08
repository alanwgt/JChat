import i18n from 'i18next';
import LanguageDetector from 'i18next-browser-languagedetector';
import HttpBackend from 'i18next-http-backend';
import { initReactI18next } from 'react-i18next';

i18n
  .use(HttpBackend)
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    returnEmptyString: false,
    lng: 'en',
    fallbackLng: 'en',
    ns: 'translation',
    debug: process.env.NODE_ENV !== 'production',
    interpolation: {
      escapeValue: false,
    },
    backend: {
      backends: [HttpBackend],
    },
  });

export default i18n;
