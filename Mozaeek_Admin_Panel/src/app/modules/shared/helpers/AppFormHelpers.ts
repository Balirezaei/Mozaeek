import { TFunction } from 'i18next';

import { Translations } from '../../../../features/localization';

export const getCreaditSubmitText = (tFunction: TFunction, create: boolean) => {
  if (create) {
    return tFunction(Translations.Common.Creation);
  } else {
    return tFunction(Translations.Common.Edition);
  }
};
