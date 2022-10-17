import { CheckSquareFilled, CloseSquareFilled } from '@ant-design/icons';
import { Tag } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';
import { ClientFramework } from '../../../../../types';

type Props = {
  status: boolean | undefined;
  type: 'icon' | 'tag';
  clientFramework?: ClientFramework;
};

const bootstrapLabelClasses = 'label-inline label label-lg label-light-';
const Status: React.VFC<Props> = (props) => {
  const { t } = useTranslation();
  let element;
  switch (props.type) {
    case 'icon':
      element = props.status ? <CheckSquareFilled className="font-size-lg text-green" /> : <CloseSquareFilled className="text-danger" />;
      break;
    case 'tag':
      switch (props.clientFramework) {
        case 'Bootstrap':
          element = props.status ? (
            <span className={`${bootstrapLabelClasses}success`}>{t(Translations.Common.Active)}</span>
          ) : (
            <span className={`${bootstrapLabelClasses}danger`}>{t(Translations.Common.Inactive)}</span>
          );
          break;
        case 'Antd':
          element = props.status ? <Tag color={'green'}>{t(Translations.Common.Active)}</Tag> : <Tag color={'red'}>{t(Translations.Common.Inactive)}</Tag>;
          break;
      }
      break;
  }
  return element ?? <h1>Status Not Supported!!</h1>;
};
Status.defaultProps = {
  clientFramework: 'Antd',
};

export default Status;
