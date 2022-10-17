import React from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../../features/localization';

type Props = {
  from: number;
  to: number;
  total: number;
  recordName?: string;
};
const ShowTotalPagination: React.VFC<Props> = (props) => {
  const { t } = useTranslation();

  return (
    <span>
      {t(Translations.Common.ShowingRowsFromToOfTotal, {
        from: props.from,
        to: props.to,
        total: props.total,
        name: props.recordName ? props.recordName : t(Translations.Common.RowsOnPagination),
      })}
    </span>
  );
};

export default ShowTotalPagination;
