import { Skeleton } from 'antd';
import { TFunction } from 'i18next';

import { Translations } from '../../../../features/localization';
import { CreaDitMode } from '../../../../types';

export const getCreaditTitle = (tFunction: TFunction, mode: CreaDitMode, entityName: string, itemName: string | undefined, pending: boolean | undefined) => {
  if (pending) {
    return <Skeleton.Input size="small" active style={{ width: 200 }} />;
  }
  switch (mode) {
    case 'Create':
      return `${tFunction(Translations.Common.Create)} ${entityName}`;
    case 'Edit':
      return `${tFunction(Translations.Common.Edit)} ${entityName} '${itemName}'`;
    case 'AddSub':
      return `${tFunction(Translations.Common.CreateSubForVar)} ${entityName} '${itemName}'`;
  }
};
