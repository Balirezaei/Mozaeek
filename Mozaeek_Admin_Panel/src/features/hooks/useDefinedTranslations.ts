import { useTranslation } from 'react-i18next';

import { Translations } from '../localization';

const useDefinedTranslations = () => {
  const { t } = useTranslation();

  const gender = (value: boolean) => {
    return value ? t(Translations.Common.Male) : t(Translations.Common.Female);
  };

  return { gender };
};

export default useDefinedTranslations;
