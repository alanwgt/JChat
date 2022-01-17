import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

import HttpBackend from 'i18next-http-backend';
import LanguageDetector from 'i18next-browser-languagedetector';

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
