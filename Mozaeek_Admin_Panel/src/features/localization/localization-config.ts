import i18next, { InitOptions } from 'i18next';
import moment from 'moment-jalaali';
import { initReactI18next } from 'react-i18next';

import en from '../../app/translations/en.json';
import fa from '../../app/translations/fa.json';
import { LocalizationHelpers, getStorage, setStorage, valueExistInEnum } from '../../utils/helpers';
import { LocalStorageExpiration, LocalStorageKey } from '../constants';
import { CultureName, LanguageCode, SupportedCultureNames, SupportedLanguagesCodes } from './cultures';

export type TranslationResource = typeof en;
export type ConvertedToObjectType<T> = {
  [P in keyof T]: T[P] extends string ? string : ConvertedToObjectType<T[P]>;
};

export const Translations: ConvertedToObjectType<TranslationResource> = {} as any;

let cultureName = getStorage(LocalStorageKey.Culture);
if (!cultureName || !valueExistInEnum(SupportedCultureNames, cultureName)) {
  cultureName = LocalizationHelpers.defaultCulture.Name;
  setStorage(LocalStorageKey.Culture, cultureName, LocalStorageExpiration.Infinite);
}
//LocalizationHelpers.setCulture(cultureName);
LocalizationHelpers.setCulture(CultureName.FaIr);
const language = LocalizationHelpers.getCultureFromCultureName(cultureName as CultureName)!.LanguageCode;
moment.loadPersian({ dialect: 'persian-modern' });

const i18nextOptions: InitOptions = {
  debug: false,
  resources: {
    en: { translation: en },
    fa: { translation: fa },
  },
  lng: language,
  interpolation: {
    escapeValue: false, // React escapes by default
  },
  supportedLngs: SupportedLanguagesCodes as LanguageCode[],
  fallbackLng: LocalizationHelpers.defaultCulture!.LanguageCode,
};

export const initLocalization = async () => {
  initLocalizationKeys(en, Translations);

  await i18next.use(initReactI18next).init(i18nextOptions);
};

const initLocalizationKeys = (obj: any, dict: { [key: string]: string | object }, current?: string) => {
  Object.keys(obj).forEach((key) => {
    const currentLookupKey = current ? `${current}.${key}` : key;
    if (typeof obj[key] === 'object') {
      dict[key] = {};

      //@ts-ignore
      initLocalizationKeys(obj[key], dict[key], currentLookupKey);
    } else {
      dict[key] = currentLookupKey;
    }
  });
};
