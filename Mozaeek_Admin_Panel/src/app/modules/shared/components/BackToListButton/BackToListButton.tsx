import { UnorderedListOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';

import { Translations } from '../../../../../features/localization';

type Props = {
  pageName?: string;
  url: string;
  icon?: React.ReactNode;
};
const BackToListButton: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  return (
    <Link to={props.url}>
      <Button type="default" htmlType="button" icon={<UnorderedListOutlined />}>
        {props.pageName ? t(Translations.Common.BackToListVar, { page: props.pageName }) : t(Translations.Common.BackToList)}
      </Button>
    </Link>
  );
});

export default BackToListButton;
