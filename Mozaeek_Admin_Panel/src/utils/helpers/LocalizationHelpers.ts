import { LocalStorageExpiration, LocalStorageKey } from '../../features/constants';
import { CultureName, SupportedCulture, SupportedCultures } from '../../features/localization/cultures';
import { setStorage } from './LocalStorageHelpers';

export class LocalizationHelpers {
  static defaultCulture = SupportedCultures.find((c) => c.Name === CultureName.FaIr)!;

  private static currentCulture: SupportedCulture = LocalizationHelpers.defaultCulture;

  static setCulture = (cultureName: CultureName | string) => {
    const newCulture: SupportedCulture = SupportedCultures.find((c) => c.Name === cultureName) ?? LocalizationHelpers.defaultCulture;
    setStorage(LocalStorageKey.Culture, newCulture.Name, LocalStorageExpiration.Infinite);
    LocalizationHelpers.currentCulture = SupportedCultures.find((f) => f.Name === newCulture.Name)!;
    // const interval = window.setInterval(async () => {
    //   if (i18next.isInitialized) {
    //     await i18next.changeLanguage(newCulture.LanguageCode);
    //     window.clearInterval(interval);
    //   }
    // }, 100);
  };

  static setCultureToDefault = async () => {
    await LocalizationHelpers.setCulture(LocalizationHelpers.defaultCulture.Name);
  };

  static getCurrentCulture = () => {
    return LocalizationHelpers.currentCulture;
  };

  static getHtmlDirection = () => {
    return LocalizationHelpers.getCurrentCulture().HtmlDirection;
  };

  static getCultureFromCultureName = (cultureName: CultureName) => {
    return SupportedCultures.find((f) => f.Name === cultureName);
  };

  static getOrdinalKey = (number: number) => {
    switch (number) {
      case 1:
        return 'Ordinal.First';
      case 2:
        return 'Ordinal.Second';
      case 3:
        return 'Ordinal.Third';
      case 4:
        return 'Ordinal.Fourth';
      case 5:
        return 'Ordinal.Fifth';
      case 6:
        return 'Ordinal.Sixth';
      case 7:
        return 'Ordinal.Seventh';
      case 8:
        return 'Ordinal.Eighth';
      case 9:
        return 'Ordinal.Ninth';
      case 10:
        return 'Ordinal.Tenth';
    }
  };

  static getLanguageFromServer = async () => {
    // //ToDo : Get BaseUrl from environment
    // //ToDo : Real url
    // //ToDo : use Axios
    return await fetch('http://localhost:5000/test/geten').then(async (response) => {
      if (response.ok) {
        return await response.text();
      } else {
        return null;
      }
    });
  };
}
